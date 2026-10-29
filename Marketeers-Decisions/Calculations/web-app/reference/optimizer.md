# Optimizer fields

Tracks: `backend/api/schemas.py` (`RunOptimizerRequest`, `CorridorPreviewRequest`, `SkuBoundInput`), `backend/api/routes/optimizer.py`.

What the search does: [Price search](../explanation/optimizer.md). What the corridor chart means: [Price corridor](../explanation/price-corridor.md).

## `GET /bounds`

Each row: `sku`, `base_price`, `lo`, `hi`, `cost`.

`lo` and `hi` are calibrated price levels. `cost` is the stored unit cost, or JSON `null` when the study has none.

`SkuBoundInput.cost` on `POST /run` defaults to `0` only when the client omits the field. The Price Optimization page sends `cost ?? 0`.

## `POST /run`

| Field | Role |
|-------|------|
| `group_type`, `brand` | Group being optimized |
| `bounds[]` | `sku`, `base_price`, `lo`, `hi`, `cost`, `included` |
| `objective` | `revenue`, `value_share`, `volume_share`, or `margin` |
| `scope` | `brand`, or any other value for included decision SKUs only |
| `max_iters` | Passes. Default `15`. |
| `multi_start` | `full` (default) or `current` |
| `tol_str` | Acceptance tolerance. Default `1e-5`. |
| `filters` | Segment filters, AND. `assignment_key` plus `member` is the one-filter form. |
| `base_scenario` | Starting prices. Omit for the study base. |
| `competitor_moves` | Competitor price shocks applied before search. Accepted by the API. The Price Optimization page does not send them (`COMPETITIVE_MOVES_ENABLED` is false). |
| `reference_sku`, `anchor_price` | Corridor. Anchor is a fixed price. |
| `anchor_label` | Display only |
| `anchor_size` | Required for `corridor_basis=unit` |
| `corridor_basis` | `pack` (default) or `unit` |
| `corridor_min`, `corridor_max` | Allowed reference/anchor ratios |
| `enforce_ppa_ladder` | Default `false` |
| `ppa_size_chains`, `ppa_tier_links`, `ppa_min_saving`, `ppa_max_saving` | Used when the ladder is on |
| `min_margin_pct` | Percent points. `34` means 34%. |
| `max_volume_drop_pct` | Percent drop of scope volume share versus its baseline. `10` means 10% of starting volume, not 10 percentage points of share. |

`margin`, or any run that sets `min_margin_pct`, fails when a required SKU has no stored unit cost.

Job `status` is `running`, `done`, `cancelled`, or `error`. The result's `solution_status` is `optimal_found` or `infeasible`.

## `POST /corridor-preview`

| Field | Role |
|-------|------|
| `group_type`, `brand`, `reference_sku` | What is swept |
| `anchor_price` | Denominator of the index. Required. |
| `anchor_label`, `anchor_sku` | Echoed. The sweep does not look up `anchor_sku`. |
| `index_min`, `index_max`, `index_step` | Optional window. Omit them to span calibrated lo/hi. |
| `n_points` | Default `10` |
| `min_margin_pct`, `max_volume_drop_pct` | Percent points, same as the run |
| `filters`, `assignment_key`, `member`, `base_scenario`, `competitor_moves` | Same roles as on a run |

## Result fields worth naming

`prices`, `objective_value`, `baseline_value`, `uplift_pct`, `n_evals`, `n_passes`, `menus`, `constraint_conflicts`, `infeasible_trials`, `binding_constraints`, `limited_by`, `corridor_ratio`, `ppa_ladder_rows`, `brand_volume_drop_pct`, `market_rows`.

`raw_prices` equals `prices`. There is no post-search shelf snap.

`n_passes` is `max(0, len(obj_history) − 1)`, and `obj_history` stores one value per start, not per pass.
