# Chart Analysis

Source: `ChartForm`. Share math for every tab is [the pipeline](../README.md). Each tab keeps its own `ProjectCore`, binds a scenario or the default market, calls `findPredictedShares` (or a method that does), and stores a `DataTable` on the chart set. Export of that set is [EXPORT.md](../EXPORT.md).

Tabs, in order:

| Tab | Page |
|-----|------|
| Elasticity | [ELASTICITY.md](ELASTICITY.md) |
| Price Index | [PRICE_INDEX.md](PRICE_INDEX.md) |
| Shares & Revenues | [SHARES_AND_REVENUES.md](SHARES_AND_REVENUES.md) |
| Brand Interaction | [BRAND_INTERACTION.md](BRAND_INTERACTION.md) |
| SOM Vs. SOP | [SOM_VS_SOP.md](SOM_VS_SOP.md) |
| Fair Share | [FAIR_SHARE.md](FAIR_SHARE.md) |
| Validation | [VALIDATION.md](VALIDATION.md) |
| Introduction | [INCREMENTAL.md](INCREMENTAL.md) |
| Removal | [INCREMENTAL.md](INCREMENTAL.md) |

Opening a tab with no SKU/group choice yet selects Groups. Brand Interaction and Fair Share also select the first basic scenario. Fair Share selects the first comparison scenario as well.

A chart name that fails `validateChartName` is not run. The set can be cleared, which drops every stored table.
