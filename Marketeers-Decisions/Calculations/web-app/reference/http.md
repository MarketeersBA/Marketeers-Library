# HTTP

Tracks: `backend/api/main.py` and `backend/api/routes/`. OpenAPI is `/docs` on the running API. Shapes are the Pydantic models in `backend/api/schemas.py`. TypeScript clients under `frontend/src/api/` are handwritten.

Prefix notes: `{slug}` is the study slug. Sessions use `sessionId` from `POST /api/sessions`.

## Health

| Method | Path | Role |
|--------|------|------|
| GET | `/api/health` | `{"status":"ok"}` |

## Auth

| Method | Path | Role |
|--------|------|------|
| GET | `/api/auth/config` | `{mode: dev}` or Cognito pool fields. Public. |
| POST | `/api/auth/mock/signup` | Mock mode only |
| POST | `/api/auth/mock/confirm` | Mock mode only |
| POST | `/api/auth/mock/login` | Mock mode only |

`dev` auth does not use these posts. It treats every request as client `local-dev`.

## Studies and wizard

| Method | Path | Role |
|--------|------|------|
| GET | `/api/studies` | Catalog for the current client |
| POST | `/api/studies` | Create |
| PATCH | `/api/studies/{slug}` | Update |
| DELETE | `/api/studies/{slug}` | Delete |
| GET, PATCH | `/api/studies/{slug}/meta` | Display metadata |
| GET | `/api/studies/{slug}/data-sources` | Which files the study has |
| POST | `/api/studies/{slug}/data-sources/design` | Replace design workbook |
| POST | `/api/studies/{slug}/data-sources/hbu` | Replace HBU |
| POST | `/api/studies/{slug}/data-sources/unit-costs` | Upload unit costs |
| POST | `/api/wizard/design` | Parse a design file before create |
| POST | `/api/wizard/hbu` | Parse an HBU file before create |

File layout and workbook sheets: [Studies and files](studies-and-files.md).

## Sessions

| Method | Path | Role |
|--------|------|------|
| POST | `/api/sessions` | Load a study. Body includes `slug`. |
| DELETE | `/api/sessions/{session_id}` | Drop the in-memory session |
| POST | `/api/sessions/{session_id}/recalculate` | Apply grid edits and rerun shares |
| POST | `/api/sessions/{session_id}/reset` | Back to base |
| GET, POST | `/api/sessions/{session_id}/segmentation` | Read or set AND filters |
| GET | `/api/sessions/{session_id}/groups` | Group rollups |
| GET | `/api/sessions/{session_id}/bounds` | Calibrated attribute bounds |
| POST | `/api/sessions/{session_id}/apply-scenario` | Load a saved scenario |
| POST | `/api/sessions/{session_id}/competitor-reaction` | Apply competitor price shocks |
| GET | `/api/sessions/{session_id}/export` | Excel export of the grid |

Sessions live in the API process. They are not a shared multi-user room. Study files are tenant-scoped.

## Scenarios

Under `/api/studies/{slug}`:

| Method | Path | Role |
|--------|------|------|
| GET | `/scenarios` | List |
| POST | `/scenarios` | Save |
| POST | `/scenarios/seed-base` | Seed the base scenario |
| PATCH | `/scenarios/order` | Reorder |
| DELETE | `/scenarios/{name}` | Delete |
| POST | `/scenarios/import/preview` | Preview an Excel import |
| POST | `/scenarios/import` | Import |
| POST | `/compare` | Side-by-side metrics |
| POST | `/compare/export` | Compare workbook |

## Validation waves

Under `/api/studies/{slug}/validation`:

| Method | Path | Role |
|--------|------|------|
| GET | `` | List waves |
| GET | `/series` | Chart series |
| GET | `/score` | Fit summary |
| POST | `/upload` | One workbook, one wave |
| DELETE | `/{wave_id}` | Delete |

What the score means: [Analytics](../explanation/analytics.md).

## Study report

Under `/api/studies/{slug}`:

| Method | Path | Role |
|--------|------|------|
| GET | `/report/sections?mode=analyst\|client` | `mode`, and `sections` in deck order, each with `parts` and an optional `choice` (study-specific picks). Client mode adds `objectives` (the goals this study supports), `brands` (own brands) and `limits` (`max_price_change_pct` and `max_volume_drop_pct`, each `{options, default}` in percent). |
| POST | `/report.pptx` | Build and download the deck |

`POST /report.pptx` body:

| Field | Meaning |
|-------|---------|
| `mode` | `analyst` (default) or `client`. Anything else is 422. |
| `sections` | Section key → part keys. Omit it for every section and part. A section with parts that gets `[]` is left out. |
| `picks` | Choice key (for example `scenarios`, `drivers`) → values. Omitted keys use the defaults. |
| `objective` | Client deck goal: `revenue` (default), `margin`, `value_share` or `volume_share`. |
| `brands` | Client deck: own brands to assess and reprice. Omit, or `[]`, for all. |
| `max_price_change_pct` | Client deck: the plan's largest price change per SKU, in percent (above 0, at most 50). Default 10. |
| `max_volume_drop_pct` | Client deck: the largest loss of our total volume for revenue and margin plans, in percent (0 to 50). Default 3. |

The four client fields are rejected for the analyst deck. There is no segment field: the client deck is always total market. Unknown sections, parts or picks, a goal the study can't support (margin without unit costs), a brand that isn't ours, or a limit out of range return 400. The file name is `{study}_{mode}_report.pptx`. What each deck contains: [Study report](../explanation/report.md).

## Analytics

`GET /api/studies/{slug}/analytics/{type}`

`type` is `som-sop`, `cross-brand`, `introduction`, or `removal`. Anything else is 404.

Shared query: `base_scenario`, `filters` (JSON array of `{assignment_key, member}`), or the single pair `assignment_key` and `member`.

Cross-brand also takes `driver_product`, `level` (`sku` or `group`), `group_type`, `group_member`, and `scale` (comma-separated multipliers).

Introduction and removal also take `products`, `current_lineup`, `group_type`, `group_member`, `mode` (`exclusive` or `cumulative`), `incremental_type` (`Revenue`, `Market Share`, `Value Share`), and `intro_order` (`sorted` or `greedy`).

## Optimizer

Prefix `/api/{slug}/optimizer`. Fields: [Optimizer fields](optimizer.md). Meaning: [Price search](../explanation/optimizer.md).

| Method | Path | Role |
|--------|------|------|
| GET | `/context` | Group types, brands, segments, scenarios |
| GET | `/brand-summary` | One brand at current prices |
| GET | `/brand-summaries` | Every brand of a group type |
| GET | `/bounds` | Calibrated `[lo, hi]`, price, stored cost |
| POST | `/corridor-preview` | Reference-SKU sweep |
| POST | `/run` | Start a job |
| GET | `/jobs/{job_id}` | Status, progress, result |
| POST | `/jobs/{job_id}/cancel` | Cancel |
| POST | `/jobs/{job_id}/save-scenario` | Save result prices |
| POST | `/jobs/{job_id}/export` | CSV |
