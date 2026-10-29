# Analytics

Tracks: `backend/model/analytics/som_sop.py`, `cross_brand.py`, `incremental.py`, `validation.py`, and `backend/api/routes/analytics.py`, `backend/api/routes/validation.py`.

Each procedure reruns the [mathematical model](mathematical-model.md). None of them fits a separate demand curve. Assortment's use of introduction is in [Assortment](assortment.md).

The HTTP types on `GET /api/studies/{slug}/analytics/{type}` are `som-sop`, `cross-brand`, `introduction`, and `removal`.

## Share of market vs share of preference

**Question.** Where is volume share out of line with how widely choosers can reach the SKU?

**Statistic.** For each included product, after one `find_predicted_shares`:

```text
SoM = volume share / 100
SoP = penetration / 100
```

Penetration is preference × WD, scaled so it lines up with volume share. See [Measures](measures.md).

**Procedure.** One simulation at the current scenario. No price loop.

**Limit.** SoP is not a brand's value share and not a survey preference share. The scatter is volume share against penetration.

## Cross-brand

**Question.** If one driver SKU's price is shocked, how do other SKUs' volume shares move?

**Statistic.** New volume share of each included product at each price multiplier, against the unshocked shares.

**Procedure.** Default multipliers are 0.9, 1.0, and 1.1 on the driver price. Level `sku` shocks one product. Level `group` shocks the products in a group member. Each multiplier is a fresh simulation.

**Limit.** This is not an elasticity curve and not the optimizer. The shocks are the multipliers you pass (or the default three). Other prices stay put.

## Introduction and removal

**Question.** What does the lineup gain or lose as SKUs are added or dropped, one at a time?

**Statistic.** One of revenue, volume share, or value share for the group, depending on `incremental_type`.

**Procedure.** Two modes.

- **Exclusive.** Each candidate is toggled alone against the current lineup. The others stay as they are.
- **Cumulative, value share, `intro_order=sorted`.** SKUs are ordered by their value share in the loaded scenario, highest first. The response accumulates those fixed shares. It does not re-simulate the lineup after each add. This is the Assortment call.
- **Cumulative, other metrics, `intro_order=sorted`.** Candidates are ranked by a standalone simulation against the starting lineup, then introduced in that fixed order.
- **Cumulative, `intro_order=greedy`.** At each step the candidate with the highest metric delta is picked, and the lineup baseline updates.

Scope is a group type plus member, or company products when the group type is `Company Products`. Only products in that scope are included or excluded. The rest of the market stays in the simulation. Omit `current_lineup` to start from whoever is included now. An empty lineup starts from nobody in the scope.

Introduction and removal are the same engine with the direction reversed (`Intro` vs `Removal`).

**Limit.** The value-share sorted path is a ranking of current value shares, not the gain from adding the SKU to a growing lineup. Greedy order is a path, not a proof that the subset is the best assortment of that size. Exclusive results are not additive. Keep / Watch / Push WD are not in this response.

## Held-out validation

**Question.** After a later period's prices and distribution are applied, does the model still match audit shares?

**Statistic.** Predicted company volume versus audit, after the simulated shares are rescaled so their total matches the audit total in the wave file. Fit summaries (mean absolute gap, WMAPE, max gap) come from `validation_scenario_score`.

**Procedure.** Data Hub upload, not the analytics type list. Routes are under `/api/studies/{slug}/validation`. A wave stores per-SKU inputs and benchmark shares. `study.json` and the HBU file are not rewritten. Scoring clones the study, applies the wave, predicts, then rebases to the wave's audit total.

**Limit.** The rebased predicted share is not the Runner's `N.Share`. Runner reports the unrebased scenario share. The same price change can show a different company share on Data Hub and on Runner, and both figures follow from that difference.
