# Study database

Source: `DataTransaction.initializeDataTable`, `DatabaseDesign`, `DatabaseDesign2`, `Attributes.getDataRow`, `Products.getDataRow`, `ProjectCore.getDataRow`.

A study is an Access database. `DatabaseDesign` writes the template bytes out to the chosen path, then alters it for this study's attributes, segments, and groups. The open study is read through `DataTransaction` into one `DataSet`.

## Tables the loader always opens

Sorted by the column in parentheses.

| Table | Sort | Role |
|-------|------|------|
| `Attributes` | `AttributeID` | One row per attribute |
| `Products` | `ProductID` | One row per SKU |
| `SegmentsShares` | `ProductID` | Optional per-segment base share columns |
| `SegmentsPrice` | `ProductID` | Optional per-segment base price columns |
| `SegmentsWD` | `ProductID` | Optional per-segment WD columns |
| `GroupsExcludedSalesUnit` | `GroupTypeID` | Excluded value sales by group member |
| `SegmentsMSU` | `ID` | One row; `Total` plus a column per segment member |
| `SegmentsVSU` | `ID` | Same shape as MSU |
| `Study` | `StudyID` | Study constants. The code calls this table the module. |
| `HBUData` | `RespondentID` | One row per respondent |

Conditional attributes add `Conditional{Name}List` (`ProductID`, `ProductCombination` memo).

Grouping adds `GroupingTypes`, `Groups`, `ProductsGrouping`. Segmenting adds `SegmentingTypes`, `Segments`, `RespondentSegmentation`.

`DatabaseDesign.executeCommand` adds the study-specific columns and creates the conditional, grouping, and respondent tables when those arrays are non-empty. `DatabaseDesign2` is the shorter builder: products, HBU level columns, conditional lists, grouping, and respondent segmentation. It does not add segment share, price, WD, MSU, VSU, or excluded-sales columns.

## `Study` row

`StudyName`, `StudyDescription`, `CompanyName`, `CompanyLocation`, `ResearchLogo`, `SU`, `ExponentFactor`, `Analysis`, `SizeAttribute`, `SKUAttribute`, `RespondentsCount`, `UseGrouping`, `UseSegmenting`, `Financial`. `getDataRow` also reads `RespondentWeight` from `HBUData` into `respondentsWeight`. MSU and VSU are not taken from this row at load; they come from `SegmentsMSU` / `SegmentsVSU` column `Total`. `setDataRow` writes `MSU` back onto `Study` along with the fields above.

## `Attributes` row

`AttributeID`, `AttributeName`, `IsDiscrete`, `IsConditional`, `AttributeLevelCount`, `AttributeLevelsName` (comma-separated), `AttributeBaseName`, `IsVisible`, `IsIncluded`.

`IsIncluded` controls whether the attribute enters utility. `IsVisible` controls whether the results grid shows it. The attribute whose name trims to `price` is the price attribute. `SizeAttribute` on the study row picks the size attribute by id.

## `Products` row

Identity and flags: `ProductID`, `ProductName`, `Company`, `Included`, `ReplacedProduct`, `NewProduct`, `BaseProduct`.

Market inputs: `BaseMarketShare`, `WD`, `PromotionCalibration`, `SOR`, `ConsumptionCalibration`, `AwarenessLevel`, `RepeateRate`, `NOS`, `TDC`.

Each attribute adds `Base{AttributeName}`. The design step always ensures `BasePrice` and `BaseSize` exist even when price or size was not in the attribute list. `BasePrice` is stored as `PriceUnit`. `BaseSize` is stored as `SizeUnit` and stays the revenue and value-sales denominator. See [METRICS.md](METRICS.md).

The current combination starts as a copy of the `Base*` values.

## `HBUData` row

`RespondentID`, `RespondentWeight`, then one column per level name with spaces removed (`DatabaseDesign` adds those as text). At runtime the utility loader reads:

- `ProductID{levelName}` for the SKU attribute
- `{AttributeName}Level{n}` for every other attribute, `n` starting at 1

## Segment and group columns

Share, price, and WD tables gain one double column per segment-member name. MSU, VSU, and `GroupsExcludedSalesUnit` do the same, and the loader falls back to `Total` when the member column is absent.

`ProductsGrouping` is created with one short column per group-type name. The running study reads membership as `Group{TypeID}`. `RespondentSegmentation` is created with one short column per segment-type name. The running study reads `Segment{TypeID}`.

## Write-back

`fillDatabase` updates attributes, HBU, study, products, the segment tables, conditional lists, and, when enabled, groups and respondent segmentation. `updateFinancial` runs `UPDATE Products SET NOS = …, TDC = … WHERE ProductID = …` for each product row.
