# Volume shares

Source: `Products.calculateNewShare`, `Products.rebaseNewShare`, `ProjectCore.findPredictedShares`, `rebaseProductNewShare`, `resetProductTypes`.

`calculateNewShare` adjusts preference for distribution and size, turns that into a volume share, then rebases so the included market keeps the same total volume.

## Product types

`resetProductTypes` classifies each SKU from base share and `BaseProduct` (a 1-based product id; `0` means none).

| Type | Rule |
|------|------|
| `ExistingProduct` | `BaseMarketShare ≠ 0` and `BaseProduct = 0`. Included in the base market. |
| `ReplacedProduct` | `BaseMarketShare = 0` and `BaseProduct > 0`. Starts excluded. `BaseProduct` is the SKU it stands in for. |
| `NewProduct` | `BaseMarketShare = 0` and `BaseProduct = 0`. Starts excluded. |

`getDataRow` uses the same split, and also requires the product to be excluded before it labels a zero-share SKU as replaced or new.

## Two passes when a new product is included

`findPredictedShares` pulls every included `NewProduct` out of the set, runs preference and `calculateNewShare`, then writes the result back onto current share:

- A `ReplacedProduct` writes `NewShare` and `NewPreferenceShare` onto its base product's `CurrentShare` and `CurrentPreferenceShare`.
- Any other included product copies its own new share and new preference onto its current values.
- An excluded product whose alternatives (SKUs with `BaseProduct` equal to this product's id) are all excluded has `CurrentShare` set to 0. If one alternative is included, current share is left as it was.

The new products are included again and preference plus `calculateNewShare` run a second time. That second pass is what gives a new SKU a volume share against a market that already contains the replacement.

## Preference adjustment

Included products only. `size` is the current size-attribute value. `baseSize` is the default size-attribute value. If the size index is `-1`, both reads return `1`.

Existing and replaced:

```text
NewPreferenceShare *= (NewWD / OriginalWD) * (size / baseSize)
```

New products skip the WD ratio:

```text
NewPreferenceShare *= (size / baseSize)
```

Then included preference shares are scaled so they sum to 100:

```text
NewPreferenceShare = NewPreferenceShare / sum(included NewPreferenceShare) * 100
```

## Volume share before rebase

Each included product is multiplied by `PromotionCalibration` after the line below.

Existing:

```text
NewShare = CurrentShare * (NewPreferenceShare / CurrentPreferenceShare)
```

Replaced, using the base product's current share and current preference:

```text
NewShare = base.CurrentShare * (NewPreferenceShare / base.CurrentPreferenceShare)
```

New:

```text
NewShare = NewPreferenceShare * NewWD * ConsumptionCalibration * SOR * AwarenessLevel * RepeateRate / 100
```

`SOR`, `ConsumptionCalibration`, `AwarenessLevel`, `RepeateRate`, and `PromotionCalibration` come from the product row.

## Zero-sum rebase

`rebaseNewShare` keeps total volume equal to the sum of `CurrentShare` across every product (included or not).

For each included product:

```text
Diff = NewShare - CurrentShare          (existing and new)
Diff = NewShare - base.CurrentShare     (replaced)
```

Positive diffs are summed as `pos`. Negative diffs are summed as `neg`. A replaced product also adds its base product's `CurrentShare` into `neg`. An excluded product with `CurrentShare > 0` subtracts that current share from `neg` (the lost volume is a negative contribution).

Let `gap = sum(included NewShare) - sum(all CurrentShare)`.

- If `gap > 0`, each positive diff is multiplied by `abs(neg / pos)`, and `NewShare` is rebuilt as the reference current share plus the scaled diff. The reference is the base product's current share for a replaced SKU, otherwise its own.
- If `gap < 0`, each negative diff is multiplied by `abs(pos / neg)`, and `NewShare` is rebuilt the same way.
- If `gap = 0`, diffs are left alone.

Included shares are then scaled so they sum to `sum(all CurrentShare)`:

```text
NewShare *= sum(CurrentShare) / sum(included NewShare)
```

## Audit rebase

`rebaseProductNewShare(rebaseValue)` is separate from the zero-sum rebase. Validation calls it with `getTotalProductsAuditShare()` after `findPredictedShares`.

```text
NewShare *= rebaseValue / sum(NewShare over every product)
```

The sum includes excluded products. Their new share is whatever was last written; an excluded product is not updated inside `calculateNewShare`.
