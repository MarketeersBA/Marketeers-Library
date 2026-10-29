# Metrics and the results grid

Source: `ProjectCore.setProductsRevenue`, `setProductsValueSalesUnit`, `setProductsValueShare`, `setProductsPenetration`, `getCompanyContribution`, `fillDisplayResultTable`, `fillGroupsDisplayResult`.

These run at the end of `findPredictedShares`, after volume share is final. Base revenue and base penetration are computed when the base preference shares are built (`calculateBasePreferenceShareOnBaseMarketProducts` and `setSegmentation`).

## Revenue

For each included product, if the study has a `Price` attribute, `PriceUnit` is set to the current price. `SizeUnit` stays the `BaseSize` loaded with the product. A size edit changes preference and volume share. It does not change the size in this denominator.

```text
Revenue = MSU * SU * NewShare * PriceUnit / (SizeUnit * 100)
```

Base revenue uses `BaseIncluded`, the default price, and `BaseMarketShare` in place of `NewShare`.

`R.Index = Revenue / BaseRevenue`.

Company revenue is the sum of `Revenue` over included company products (`IsCompany`). Company base revenue uses `BaseIncluded`.

## Value sales and value share

```text
ValueSalesUnit = MSU * NewShare * currentPrice / (100 * SizeUnit)
```

`currentPrice` is the price attribute on the product. `SizeUnit` is again `BaseSize`.

```text
valueShareUnit = sum(included ValueSalesUnit) + VSU - sum(BaseRevenue of BaseIncluded products)
ValueShare = ValueSalesUnit / valueShareUnit * 100
```

`VSU` is the study anchor (segment column or `Total`). Base value share uses the same shape with base value-sales units, `BaseMarketShare`, the default price, and `baseValueShareUnit`. The base value-sales total used in that anchor sums products that are included now, not `BaseIncluded`.

Group value share adds the group's excluded sales unit before dividing. See [GROUPS.md](GROUPS.md).

## Penetration

```text
scale = sum(NewShare) / sum(NewPreferenceShare * NewWD)     over included products
Penetration = NewPreferenceShare * NewWD * scale
```

Base penetration uses `BasePreferenceShare`, `OriginalWD`, and `BaseMarketShare`, and only while the product is included during the base-preference pass.

Company and group penetration are sums of the member penetrations. A company sum does not re-check `Included`. A group sum does.

## NOS, TDC, and company MM$

`NOS` and `TDC` are stored per product. `bindNOSAndTDCValues` sets them. `updateFinancial` writes both columns back to the `Products` table.

Company MM$ is `getCompanyContribution`:

```text
MM$ = round( sum( (NOS - TDC) * NewShare * MSU / 100 ) , 0 )
```

The sum is included company products only. The same expression per group is `getGroupContribution`. The SKU grid shows `NOS` and `TDC`. It does not show MM$. MM$ is the company total row of the group summary table.

## SKU results grid

`createDisplayResultTableSchema` / `fillProductDisplayResultRow`. Percents are formatted with the study's decimal pattern and a ` %` suffix. `S.Index` and `R.Index` are ratios.

| Column | Value |
|--------|--------|
| Product Index | Product id. A group header row uses `-1`. |
| Selected | Included |
| Product Name | |
| Base Product | `BaseProduct` |
| B.Share | `BaseMarketShare` |
| N.Share | `NewShare` |
| S.Index | `NewShare / BaseMarketShare` |
| B.P.Share | `BasePreferenceShare` |
| Preference Share | `NewPreferenceShare` |
| Penetration | |
| Type | `ExistingProduct`, `ReplacedProduct`, or `NewProduct` |
| Base WD / New WD | `OriginalWD` / `NewWD` |
| Base {attr} / New {attr} | Visible conditional attributes. `New` is present only when the attribute is included. |
| Base {attr} | Visible discrete attributes other than the SKU attribute, as the level name |
| Revenue, R.Index, VSU, Value Share | |
| Audit Share | Editable |
| NOS, TDC | Editable |

`getProductShare("BaseMarketShare")` for a replaced product returns the base product's base share. The grid does not. It prints the replaced product's own `BaseMarketShare`.

Number format comes from `decimalCount`: `0` when the count is 0, otherwise `0.` plus that many `#`, capped at 10.

## Grouped grid

When grouping is on, `fillDisplayResultTable` walks the research-start group type. Each member is a header row (`fillGroupDisplayResultRow`) followed by its SKUs. Header columns are group base share, new share, share index, new preference, penetration, base preference, revenue, revenue index, value-sales units, and value share. `Selected` on the header is true only when every existing member is included, or an excluded existing member has an included replacement.

## Group summary table

`fillGroupsDisplayResult` is a separate table: one row per member of a group type, plus `Total {company}`.

| Column | Member row | Company total |
|--------|------------|----------------|
| B.Share / N.Share | Sum of member base / new share | Company base / new share |
| S.Index | N.Share / B.Share | same |
| Revenue / R.Index | Group revenue and revenue / base revenue | Company revenue and company revenue / company base revenue |
| V.Share / VS.Index | Group value share and value share / base value share | Company value share / company base value share |
| MM$ | Empty | `getCompanyContribution` |
