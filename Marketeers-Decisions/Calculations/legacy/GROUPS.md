# Groups

Source: `Groups`, `ProjectCore.getGroupShare`, `getGroupRevenue`, `getGroupValueShare`, `getGroupPenetration`, `getGroupNewPreferenceShare`, `setGroupResearchStart`, `checkGroupProductsIncluded`.

Groups roll up products. They do not change the share math. One group type is the research start: the results grid nests SKUs under that type's members.

## Stored mapping

When `UseGrouping` is true:

| Table | Contents |
|-------|----------|
| `GroupingTypes` | `TypeID`, `GroupType`, `MembersCount`, `ResearchStart` |
| `Groups` | `GroupID`, `GroupName`, `GroupTypeID` |
| `ProductsGrouping` | `ProductID`, and `Group{TypeID}` holding the member id |

`setGroupResearchStart(type, grouping)` clears `ResearchStart` on the previous type, sets it on the new type, sets `UseGrouping`, and loads excluded sales units for the active segment member (or whatever `getSegmentation` returns as the member).

## Rollups

All of these walk products whose `ProductsGrouping` value for that type equals the member id.

| Measure | Rule |
|---------|------|
| Base share | Sum of `BaseMarketShare` |
| New share | Sum of `NewShare` where the product is included |
| Audit share | Sum of `AuditShare` |
| Revenue | Sum of `Revenue` where included |
| Base revenue | Sum of `BaseRevenue` where `BaseIncluded` |
| Value-sales units | Sum of `ValueSalesUnit` where included |
| Base value-sales units | Sum of `BaseValueSalesUnit` where included |
| New / base preference | Sum where included |
| Penetration / base penetration | Sum where included |

Value share is not a sum of SKU value shares:

```text
groupValueShare = (groupValueSalesUnit + excludedSalesUnit) * 100 / valueShareUnit
```

Base group value share uses base value-sales units and `baseValueShareUnit`. `excludedSalesUnit` comes from `GroupsExcludedSalesUnit` for this type and member. `setGroupExcludedSalesUnit` reads the active segment's column when that column exists, otherwise `Total`. A missing member returns 0.

`getGroupContribution` is the group form of company MM$:

```text
round( sum( (NOS - TDC) * NewShare * MSU / 100 ) , 0 )
```

over included members. The group summary table computes it and does not write it onto member rows. See [METRICS.md](METRICS.md).

## Header “selected”

`checkGroupProductsIncluded` is false when any `ExistingProduct` in the member is excluded and no `ReplacedProduct` pointing at it is included.

Company products are a separate list: `Products.Company` true. Company share, revenue, penetration, and MM$ use that flag, not a group type. Chart code uses the label `Company Products` as a stand-in group type for introduction and removal. See [charts/INCREMENTAL.md](charts/INCREMENTAL.md).
