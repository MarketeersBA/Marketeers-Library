# Shares and revenues

Source: `ChartForm.sharesDoneButton_Click`.

One run compares checked series across checked scenarios. Series are SKUs or members of the selected group type, plus a `Company` entry. Scenarios come from the library. The entry `Current` resets products and segmentation instead of loading a file. Every other name is bound with `bindScenarioDatatableObjectToProjectProducts`, then `findPredictedShares`.

For each series and scenario the tab reads new share, base share, revenue, base revenue, value share, base value share, penetration, and base penetration. A row whose name ends with ` Shares` uses the company getters. A group series uses the group getters. A SKU series uses the product getters. A replaced SKU's base share from `getProductShare("BaseMarketShare")` is the base product's base share.

## Tables

Eight tables are filled. Four of them are what gets stored, depending on the checkboxes.

| Stored table | Chart name | When |
|--------------|------------|------|
| Volume share, and revenue index appended after a blank row | `Shares And R.Index` | Volume share or revenue index is checked |
| Share index, and revenue appended after a blank row when both are on | `S.Index And Revenues` | Share index or revenue is checked |
| Value share, and value-share index appended | `V.Share And Index` | Value share or its index is checked |
| Penetration, and penetration index appended | `Penetration And Index` | Weighted SOP or its index is checked |

The screen chart plots volume share as columns (`0.0%`, primary axis) and revenue index as a line (secondary axis when volume share is also on).

## Cells

`num` is new share, `num2` base share, `num3` revenue, `num4` base revenue, `num5` value share, `num6` base value share, `num7` penetration, `num8` base penetration.

```text
volume share column     = num / 100
share index             = num / num2
revenue                 = num3
revenue index           = num3 / num4
value share             = num5          (already on a 0–100 scale)
value-share index       = num5 / num6
weighted SOP            = num7          (penetration)
penetration index       = num7 / num8
```

Company series names are `{CompanyName} Shares`, `Revenues`, `S.Index`, `R.Index`, `Value Share`, `VS.Index`, `Penetration`, and `P.Index`. Other series use the SKU or group name, with the index rows suffixed ` S.Index`, ` R.Index`, ` VS.Index`, or ` P.Index`.

If the chart name box is non-empty, the volume-share table is renamed to that name and the share-index table to `Index` plus that name.
