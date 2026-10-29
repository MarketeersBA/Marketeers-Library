# SOM vs SOP

Source: `ProjectCore.SoMvsSoP`, `ChartForm.somsopDoneButton_Click`.

`SoMvsSoP` runs `findPredictedShares` and emits one row per included product.

| Column | Value |
|--------|--------|
| Name | Product name |
| SOM | `NewShare / 100` |
| Brand Share | `Penetration / 100` |

Brand Share is penetration, not preference share. The table name and chart name are `SOM VS Brand Share`.

SKU mode keeps checked products. Group mode keeps checked members of the selected group type and **sums** the SOM and Brand Share cells of the SKUs in that member. Those sums are not rebased.

On screen, SOM is a column and Brand Share is a line. Both labels are `0.0%`.
