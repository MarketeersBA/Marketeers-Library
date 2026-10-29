# Desktop Market Simulator

Behavior and math of the desktop Market Simulator. The running study is a `ProjectCore`. Chart Analysis calls the same share pipeline, then shapes the result into a table.

## Pipeline

`getPredictedSharesDatatable` runs `findPredictedShares`, then fills the results grid.

```text
findPredictedShares
  hold NewProduct SKUs out of the included set
  set CurrentShare = BaseMarketShare
  set CurrentPreferenceShare = BasePreferenceShare
  loadPreferenceShare("NewPreferenceShare")
  calculateNewShare(size attribute)
  if any NewProduct was held out:
      copy the pass onto CurrentShare / CurrentPreferenceShare
      (a ReplacedProduct writes onto its base product)
      put the NewProduct SKUs back
      loadPreferenceShare + calculateNewShare again
  setProductsRevenue
  setProductsValueSalesUnit
  setProductsValueShare
  setProductsPenetration
```

Preference itself is `loadUtilityMatrix` → `getProductUtility` → `getPreferenceShare`. Volume share is `Products.calculateNewShare` and `rebaseNewShare`.

## Pages

| Page | What it covers |
|------|----------------|
| [PREFERENCE.md](PREFERENCE.md) | Utilities, interpolation, choice probabilities, preference share |
| [SHARES.md](SHARES.md) | WD, size, product types, zero-sum rebase, audit rebase |
| [METRICS.md](METRICS.md) | Revenue, value share, penetration, NOS, TDC, company MM$, results grid |
| [EDITS.md](EDITS.md) | Attribute and WD edits, include / exclude, bounds, undo |
| [SCENARIOS.md](SCENARIOS.md) | Scenario table, `.osl` file, `Scenarios.xml` |
| [SEGMENTS.md](SEGMENTS.md) | Respondent filter and segment share, price, WD, MSU, VSU |
| [GROUPS.md](GROUPS.md) | Group membership and rollups |
| [STUDY_DATABASE.md](STUDY_DATABASE.md) | Access study tables and HBU columns |
| [DESIGN_FILES.md](DESIGN_FILES.md) | Design workbook, HBU workbook, and builder constraints |
| [ENGINE.md](ENGINE.md) | Metric and feature switches |
| [EXPORT.md](EXPORT.md) | Excel workbook and PowerPoint from a chart set |
| [charts/README.md](charts/README.md) | Chart Analysis tabs |

## Study constants

Loaded from the `Study` row in `getDataRow`:

| Field | Role |
|-------|------|
| `ExponentFactor` | Multiplies utility before `exp` |
| `SU` | Sales-unit factor in revenue |
| `MSU` | Market-size unit. Starts on the `Total` column of `SegmentsMSU` |
| `VSU` | Value-sales anchor. Starts on the `Total` column of `SegmentsVSU` |
| `SizeAttribute` | Attribute id of the size attribute, stored 1-based and kept as an index |
| `SKUAttribute` | Attribute id of the SKU attribute |
| `RespondentsCount` | Length of the respondent-selection flags and of `RespondentWeight` |
| `UseGrouping` / `UseSegmenting` | Whether groups and segments are loaded |
| `Financial` | Stored on the study row with the NOS / TDC path |

`RespondentWeight` is read from `HBUData` for every respondent and always multiplies that person's utility.
