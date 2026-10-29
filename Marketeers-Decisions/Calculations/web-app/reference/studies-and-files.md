# Studies and files

Tracks: `backend/config.py` (`resolve_study_folder`, `validation_dir`), `backend/data/excel_importer.py`, `backend/data/validation_importer.py`, `backend/db/`.

## Catalog

The study list (name, company, slug, default) is the database. On startup, a legacy `backend/studies/index.json` is imported only when that catalog table is empty, then renamed to `index.json.imported`.

## Study folder

`resolve_study_folder` picks the directory:

- Client `local-dev`, when both `study.json` and `hbu.parquet` already sit in `backend/studies/{slug}/`: that legacy flat folder.
- Otherwise: `backend/studies/clients/{client_id}/{slug}/`.

With `MARKET_SIM_STORAGE=s3`, the same relative layout is the object prefix, mirrored under `backend/data/cache`. A download is skipped when the mirror file exists and its stored ETag still matches the listing.

```text
{study folder}/
  study.json
  hbu.parquet
  scenarios/
  validation/
```

## Design workbook

The importer looks up sheets by name:

| Sheet | Role |
|-------|------|
| Description | Study name, company, MSU, sales unit. `(Member) MSU` and `(Member) VSU` are that member's market size and value anchor. |
| Attributes | Attribute definitions |
| Product List | SKUs, base shares, WD, optional `Group …` columns |
| Segments Shares | Total share, WD, and price, plus `(Member) Share`, `(Member) WD`, and `(Member) Price`. The name inside the parentheses is the segment level name. |
| Segments | One row per dimension. `Segment Levels` lists member names in respondent-code order. |
| Segmentation | One row per respondent. Each `Segment {Dimension}` cell is the 1-based level code. |
| `{Name} Attribute Levels` | Conditional levels per product |
| Unit Costs | Optional. Absence leaves costs empty. |
| Groups | Optional. Otherwise groups come from `Group …` columns on Product List. |

HBU is the first sheet. The importer maps `RespondentID`, then `RLH` / `RespondentWeight` / `Respondent Weight` / `Weight` into `RespondentWeight`, then product and attribute-level columns. Share math uses that weight only when `MARKET_SIM_USE_RESPONDENT_WEIGHT=true`.

## Validation wave workbook

One file, one wave. Two sheets.

**Description.** Column A label, column B value. `Validation Name` and `Validation Date` are required. `Notes` is optional. A duplicate name is HTTP 409.

**Product List.** SKU names must match the study. `Base {Attribute}` supplies the validation-period values. `WD` is optional. Benchmark share is `Audit Share`, or `Market Share` when audit is absent.

SKUs in the file that are not in the study fail the upload. SKUs in the study that are missing from the file keep study base inputs and study audit. Inclusion is not taken from the wave file.

Waves are stored under `{study folder}/validation/` as `index.json` and `{wave_id}.json`. Uploading a wave does not modify `study.json` or the HBU file.

Routes: [HTTP](http.md). The rebase used when scoring: [Analytics](../explanation/analytics.md).
