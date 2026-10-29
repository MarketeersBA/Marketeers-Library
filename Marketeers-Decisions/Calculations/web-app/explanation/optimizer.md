# Price search

Tracks: `backend/model/optimizer.py` (`_build_menus`, `optimize_coordinate_ascent`), `backend/model/fast_evaluator.py`.

Field names and units are in [Optimizer fields](../reference/optimizer.md). Margin and volume drop are defined in [Measures](measures.md). The demand model is the [mathematical model](mathematical-model.md).

## Business question

Which pack prices should this brand's decision SKUs take to improve one commercial result, without leaving the price box, the competitive corridor, the margin floor, the volume-drop cap, or (when it is turned on) the pack ladder?

The search returns the best point it found on its menu. It does not certify a global optimum on every real price.

## Statistic

One objective, summed over the scope (the whole brand, or only the included decision SKUs):

| Objective | Score |
|-----------|--------|
| `revenue` | Sum of SKU revenue |
| `value_share` | Sum of value share |
| `volume_share` | Sum of volume share |
| `margin` | Sum of revenue × (p − c) / p |

Each candidate price vector is scored by the simulator: price is interpolated into respondent utilities, multinomial logit produces preference shares, distribution and size produce volume shares, then metrics produce revenue and value. There is no closed-form demand curve. `FastEvaluator.evaluate_at_prices` runs that path. It does not use a precomputed grid of level indices (`build_price_tables` is off).

## Procedure

`optimize_coordinate_ascent`, started by `POST /api/{slug}/optimizer/run`.

### Menu

For each decision SKU the solver builds at most 15 prices and only searches that list.

Start from the request `[lo, hi]` (calibrated price levels).

- If a margin floor is set and the SKU has a positive cost, raise `lo` to `cost / (1 − margin)`.
- If this SKU is the reference SKU and an anchor price is set, tighten `lo` and `hi` so the pack (or unit) ratio versus the anchor stays inside `[corridor_min, corridor_max]`. The anchor is a fixed competitor price, not a decision variable.
- Round `lo` up to the next cent and `hi` down to the previous cent.
- Take a `linspace` of that interval (15 points) plus the two endpoints.
- Drop points that fail the margin floor.

An empty menu for any decision SKU ends the run as infeasible. No search starts. The menu does not apply a movement cap, and it does not snap to shelf endings such as `.49` or `.99`.

### Search

Blocks come from `_get_blocks`. With the pack ladder off, every block is one SKU, in bounds order. That is the Price Optimization page. With the ladder on, size chains and tier-link reference SKUs are searched together. See [Pack architecture](pack-architecture.md).

Starts, with duplicates removed:

| Start | Prices |
|-------|--------|
| `current` | Menu price nearest each base price |
| `min`, `max`, `midpoint` | Ends and middle of each menu |
| `random` | One draw per SKU from `random.Random(42)` |

`multi_start=current` keeps only the current start. `full` (the HTTP default) keeps the others too.

Each start sweeps its blocks up to `max_iters` passes (default 15). A pass keeps the best finite score in the block. The start stops when a full sweep does not improve by more than `tol` (default `1e-5`). The winning start is the one with the best finite score.

A trial that fails corridor, ladder, or margin scores as infeasible and increments `infeasible_trials`. A trial whose scope volume drop exceeds `max_volume_drop_pct` does too. Long ladder blocks are subsampled before they are enumerated, and a combination that breaks the ladder is not evaluated.

The baseline objective is scored at the true base prices, so uplift is against the start market. If those prices fail the checks, the reported baseline is still a finite objective at those prices so uplift stays interpretable.

There is no shelf-snap after the search. `raw_prices` is the same map as `prices`.

A one-SKU problem that found a feasible point also stores a 25-point chart across that SKU's original `[lo, hi]`. That chart is not the search menu.

### Jobs

`POST /run` returns a job id. The work runs on a thread pool (two workers). The client polls. Progress is written to the database about once a second, and again when the job is `done`, `cancelled`, or `error`.

## What the result does not mean

With the ladder off, convergence means no single SKU's menu price improves the objective while the others stay fixed. A joint move of two SKUs can still score higher. The search does not prove optimality off the menu.

With the ladder on, a long chain is a joint search of a shortened menu, not every combination of the full menus.

`solution_status` is `optimal_found` or `infeasible`. "Optimal" here means the search reported a feasible winner. It does not mean a proof of global optimality. Job failure is a separate `status` of `error`.

The objective is piecewise linear in price, with a kink at every calibrated level, so a gradient method has no derivative there. The volume-drop cap is known only after a simulation. The implementation enumerates a short menu for that reason. It does not call a continuous solver, a mixed-integer solver, or a population search.

Empty menus name the SKU and the empty window. They do not tell the user which slider to move.
