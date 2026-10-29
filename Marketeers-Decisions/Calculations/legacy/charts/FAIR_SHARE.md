# Fair share

Source: `ProjectCore.FairShareOnSKUs`, `FairShareOnBrands`, `ChartForm.fairShareDoneButton_Click`.

The tab needs an attribute selected, a changed member (`memberChange` / `changeMember`), a basic scenario, and a comparison scenario. `Default` uses the base-scenario project. `Current` on the comparison uses the fair-share chart project. Any other name is loaded from the library.

Both projects run `findPredictedShares`. The attribute name is required by the tab and is not read by either calculation. The chart plots the Index column as a bar, skips zeros, and crosses the value axis at 1.

## Columns

Both methods fill the same columns: `Name`, `Index`, `Rebased Current Share`, `Rebaseto100Wot`, `New Share Before`, `New Share`, `Actual Share`, `Fair Share`. Chart name `Fair Share`.

## SKUs

A row is added when the chart project's SKU has `BaseMarketShare ≠ 0` or `NewShare ≠ 0`.

`Rebased Current Share` starts as the basic scenario's `NewShare` for that SKU. The source checks `product.ReplacedProduct` on the products container, not on the row. That container flag is not set by the row loop, so the branch that would substitute the base product's new share does not run per SKU. Included SKUs take the comparison scenario's `NewShare` as `New Share` and `New Share Before`. Excluded SKUs get 0.

## Brands

One row per member of the group type. `Rebased Current Share` is the basic scenario's group new share. `New Share` is the comparison scenario's group new share.

## The index

`sumBasic` is the sum of the rebased-current column. `sumScenario` is the sum of the new-share column.

```text
Rebased Current Share *= 100 / sumBasic
New Share             *= 100 / sumScenario
Actual Share           = abs(New Share - Rebased Current Share)
```

`others` is the sum of rebased current share over every row except the changed member. That member's actual share is `moved`.

| Row | Rebaseto100Wot | Fair Share | Index |
|-----|----------------|------------|-------|
| Changed member | Its rebased current share, unchanged | 0 | 0 |
| Every other row | `rebased * 100 / others` | `abs(Rebaseto100Wot * moved) / 100` | `Actual Share / Fair Share` |

Index is blank when that ratio is infinite or `NaN`. Fair share is the slice of the changed member's absolute move that matches the other row's weight in the market with the changed member taken out. Index is how large the actual move was compared with that slice.
