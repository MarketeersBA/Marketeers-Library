# Elasticity

Source: `ProjectCore.conditionalAttributeElasticityPerProduct`, `conditionalAttributeElasticityPerGroup`, `conditionalAttributeElasticityPerCompanyGroup`, `conditionalAttributeElasticityPerCompanySKU`. The tab wires those in `ChartForm.getElasticityDatatable`.

Elasticity here is the relative change in volume share when one conditional attribute is moved to each calibrated point. It is not an arc price elasticity.

## Setup

The attribute must be conditional. The run is either the default market (`resetProductValues` and `resetSegmentation`) or a saved scenario (`bindScenarioDatatableObjectToProjectProducts`). Optional segmentation calls `setSegmentation` for each checked member. Each combination is stored as its own table:

| Scope | Table name |
|-------|------------|
| SKU | `ES({segment} at {scenario})` — segment is `total` when segmentation is off, scenario is `Default` on the default market |
| Group | `EG(…)` |
| Company | `EC(…)` |

Checked series are kept. The chart is a line. The category label is the level name parsed as a number and formatted as a percent. The value axis is a percent.

## Per SKU

`conditionalAttributeElasticityPerProduct`.

1. `findPredictedShares` and remember each included product's `NewShare` as `baseShare`.
2. For each included product and each calibrated point `j` of the attribute, set that product's attribute to `ConditionalAttributeListValues[product, j]`, run `findPredictedShares`, and store

```text
(NewShare - baseShare) / baseShare
```

formatted `0.####`.

3. Set the attribute back to the **default** value (`getProductDefaultAttribute`), not the value from before the sweep.

Columns are the attribute level names. Chart name `Elasticity_Product`.

## Per group

`conditionalAttributeElasticityPerGroup(attribute, groupType)`.

`baseShare` is the group's new share at the start. For each member and each level, every included product in that member is set to its own calibrated value at that level, shares are recomputed, and the cell is

```text
(groupNewShare - baseShare) / baseShare
```

If that member's starting share is 0, the cell is `0`. Attributes are restored to defaults afterward. Chart name `Elasticity_Group`.

## Per company

Company by group (`conditionalAttributeElasticityPerCompanyGroup`) is the same sweep, but the measured share is `getCompanyShare("NewShare")`. The stored base is also the company new share, recorded once per group member before the sweep. A starting company share of 0 writes `0`. Chart name `Elasticity_Company`.

Company by SKU (`conditionalAttributeElasticityPerCompanySKU`) moves one included SKU at a time and records the company new share. The ratio uses the company new share from before the sweep. If `getCompanyShare("BaseMarketShare")` is 0, the cell is `0` even though the ratio's denominator is the starting new share. Chart name `Elasticity_Company`.
