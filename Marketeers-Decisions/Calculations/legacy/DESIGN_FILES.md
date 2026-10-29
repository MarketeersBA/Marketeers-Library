# Design file and HBU file

Source: `StudyStructure.readExcelTemplate`, `fillDescriptionFromExcelTemplate`, `fillAttributeFromExcelTemplate`, `fillAttributeListFromExcelTemplate`, `fillProductsFromExcelTemplate`, `fillSharesAndWDFromExcelTemplate`, `fillGroupListFromExcelTemplate`, `fillGroupsExcludedSalesUnit`, `fillSegmentListFromExcelTemplate`, `fillRespondentSegmentationFromExcelTemplate`, `fillHBUFileFromTemplateSheet`, `DatabaseDesign.executeCommand`.

The Study Structure Builder reads one design workbook, then asks for a separate HBU workbook. Both are Excel (`.xlsx` through ACE, `.xls` through Jet). The design read uses `HDR=YES` and `IMEX=1`, so the first row of each sheet is the header and the engine returns stored cell values. It does not calculate formulas. A formula whose cached value is missing is an empty cell.

On Finish the builder writes a fresh Access study from the embedded template, then alters it. An `ALTER TABLE` failure is not shown by itself. The next step looks up `ConditionalPriceList` and Jet reports that the table cannot be found.

Sheet names below are the names in the `SELECT`. The `Finance` sheet is not read. `NOS` and `TDC` are stored as 0. `SOR`, consumption calibration, repeat rate, promotion calibration, and awareness are stored as 1.

Two synthetic workbooks in [demo/](demo/) exercise the sheets on this page: [Demo-Design.xlsx](demo/Demo-Design.xlsx) and [Demo-HBU.xlsx](demo/Demo-HBU.xlsx). The design file has an included company SKU, an included competitor, a new SKU, a replaced SKU, two group types, conditional price and size, a discrete flavor, and segment share, WD, price, MSU, and VSU under legal member names. The `Notes` sheet inside the design workbook lists which row is which.

## Read order

| Sheet | Required |
|-------|----------|
| `Description` | Yes |
| `Attributes` | Yes |
| `Product List` | Yes |
| `Segments Shares & WD` | Read always. An empty sheet is reported as "Can't Access Shares & WD Sheet" |
| `Excluded Sales Unit` | Optional. A missing sheet is skipped |
| `Segments` | Optional. A missing sheet is skipped |
| `Segmentation` | Read when the segment list is non-empty |
| `{Attribute} Attribute Levels` | One sheet per conditional attribute, and one per discrete attribute other than SKU |

After those sheets, the builder asks for the HBU file. Cancelling that dialog leaves an empty HBU grid.

## `Description`

Column A is the label. Column B is the value. The study name is the header of column B, not a data row.

| Row | How it is read |
|-----|----------------|
| Header | Column B header becomes the study name |
| First data row | Column B becomes the company name, by position |
| Second data row | Column B becomes the company location, by position |
| Label `MSU` | Total market-size unit. The match is case-sensitive |
| Label `(Member) MSU` | Same number for that segment member. The name inside the parentheses is the column name |
| Label `VSU` | Total value-sales anchor |
| Label `(Member) VSU` | Same number for that segment member |
| Label `Sales Unit` | `SU` |
| Label `Study Description` | Description text |

The exponent factor is set to 1. It is not read from this sheet.

`(Jaddah) MSU` stores column `Jaddah`. The label must start with `(`, contain `)`, and end with `MSU` or `VSU`.

| Study Name | Oil Example |
|------------|-------------|
| Company Name | Savola |
| Company Location | KSA |
| MSU | 2188800 |
| VSU | 19042400 |
| (Jaddah) MSU | 100000 |
| (Riyhad) MSU | 5000000 |
| (Jaddah) VSU | 1000000 |
| (Riyhad) VSU | 40000000 |
| Sales Unit | 1 |
| Study Description | Traditional oil |

The first row is the header. `Oil Example` is the study name.

## `Attributes`

Columns: `Attribute Name`, `Levels Count`, `Discrete Type`, `Conditional Type`.

Every row needs all four cells. `Discrete Type` or `Conditional Type` must be the text `1`. The row whose name trims to `sku` is the SKU attribute and must be discrete. The row whose name trims to `size` is the size attribute. `Levels Count` is how many level columns are read from that attribute's levels sheet, and how many HBU columns that attribute contributes.

| Attribute Name | Levels Count | Discrete Type | Conditional Type |
|----------------|-------------:|:-------------:|:----------------:|
| SKU | 2 | 1 | 0 |
| Price | 2 | 0 | 1 |
| Size | 2 | 0 | 1 |

## `Product List`

One row per SKU. A row is kept when `SKU Name` is not empty.

| Column | Role |
|--------|------|
| `SKU Name` | Product name. Row order is the product id, starting at 1 |
| `Company` | `1` marks a company SKU |
| `Market Share` | `BaseMarketShare`. `0` with an empty base product is a new SKU. `0` with a base product is a replaced SKU. Any other share with an empty base product is included |
| `WD` | Base weighted distribution |
| `Base Product` | Name of the SKU this row replaces. Empty stores 0. A name that is not in `SKU Name` raises "Base products in the design file has wrong values" |
| `Base {Attribute}` | Base value of that attribute. Conditional attributes store the number. Discrete attributes store the level name, and the builder stores its 1-based index. `Base Price` and `Base Size` are read even when price or size is not an attribute row |
| `Group {Type}` | Group membership. The header must contain `Group ` and must not contain `(`. The type name is the header with those six characters removed |

| SKU Name | Company | Group Brand | Market Share | WD | Base Price | Base Size | Base Product |
|----------|:-------:|-------------|-------------:|---:|-----------:|----------:|--------------|
| Afia 1.5L | 1 | Afia | 6.6 | 80 | 21.88 | 1.5 | |
| Afia 9L | 1 | Afia | 0.3 | 4 | 94.5 | 9 | |
| New 1.5L | 1 | Afia | 0 | 0 | 20 | 1.5 | |
| Old 1.5L | 0 | Other | 0 | 10 | 18 | 1.5 | Afia 1.5L |

`Afia 1.5L` is included. `New 1.5L` is a new SKU. `Old 1.5L` replaces `Afia 1.5L`.

## `Segments Shares & WD`

One row per SKU, matched to `Product List` by `SKU Name`.

A header that ends with `Share`, `WD`, or `Price` becomes a column on `SegmentsShares`, `SegmentsWD`, or `SegmentsPrice`. The column name is the text between the first `(` and the first `)`.

| Header | Table column |
|--------|----------------|
| `(Total) Share` | `SegmentsShares.Total` |
| `(Jaddah) Share` | `SegmentsShares.Jaddah` |
| `(Jaddah) WD` | `SegmentsWD.Jaddah` |
| `(Jaddah) Price` | `SegmentsPrice.Jaddah` |

A header that ends with `Sales` is ignored. The same member name must be a legal Access identifier, because Finish inserts it unquoted:

```text
ALTER TABLE segmentsShares ADD Jaddah Double, 20-24 Double
```

`20-24` is subtraction, so the statement fails. Later `CREATE TABLE` statements, including `ConditionalPriceList`, never run. A usable member name starts with a letter and then uses only letters, digits, and underscore. `20-24`, `20_24`, and any name with a space or a hyphen fail. The level text on `Segments` has to be that same name, because selecting the member looks up the column by the menu text.

When the member column is absent at run time, share, price, and WD fall back to `Products`, and MSU and VSU fall back to `Total`. See [SEGMENTS.md](SEGMENTS.md).

| SKU Name | (Total) Share | (Total) WD | (Total) Price | (Total) Sales | (Jaddah) Share | (Jaddah) WD | (Jaddah) Price | (Jaddah) Sales |
|----------|-------------:|-----------:|--------------:|--------------:|---------------:|------------:|---------------:|---------------:|
| Afia 1.5L | 6.6 | 80 | 21.88 | 172915 | 13.2 | 80 | 10.94 | 345830 |
| Afia 9L | 0.3 | 4 | 94.5 | 6566 | 0.6 | 4 | 47.25 | 13132 |

`(Total) Sales` and `(Jaddah) Sales` are not imported.

## `Excluded Sales Unit`

Columns `Group Types` and `Groups`, then one column per segment member. Those headers are the member names themselves. They are not wrapped in parentheses. `Total` is the usual first value column.

A blank `Group Types` cell keeps the previous type. `Groups` must be a member of that type. The same Access-identifier rule applies to these headers.

| Group Types | Groups | Total | Jaddah | Riyhad |
|-------------|--------|------:|-------:|-------:|
| Brand | Afia | 0 | 0 | 1200 |
| | Other | 0 | 50 | 0 |

## `Segments`

Columns: `Segment`, `Segment Levels`.

`Segment Levels` is a comma-separated list. Order in that list is the member id: the first name is `1`, the next is `2`, and so on. `reorderGroupsAndSegmentsListView` restarts the count at 1 for each segment type.

| Segment | Segment Levels |
|---------|----------------|
| Age | Age_20_24,Age_25_34,Age_35_45 |
| Region | Jaddah,Riyhad,Dammam |

`Age_20_24` is member id 1 of Age. `Jaddah` is member id 1 of Region.

## `Segmentation`

One row per respondent. Columns: `Respondent ID`, then `Segment {Type}` for each type on the `Segments` sheet (`Segment Age`, `Segment Region`, …).

The cell is the member id from the level list, not the level name. `1` under `Segment Region` selects the first Region level. The builder copies the number unchanged into `RespondentSegmentation.Segment{TypeID}`.

| Respondent ID | Segment Age | Segment Region |
|--------------:|:-----------:|:--------------:|
| 45 | 2 | 1 |
| 47 | 1 | 2 |
| 49 | 3 | 3 |

Respondent 45 is `Age_25_34` and `Jaddah`.

## `{Attribute} Attribute Levels`

Header row: `SKU Name`, then `Level 1`, `Level 2`, … through `Levels Count`.

The first data row is the level-name row. Its `SKU Name` cell is `Levels Name`. The other cells are the names stored on the attribute (for Price, the offsets such as `-0.6`). Each following row is one SKU. Every `Level n` cell on a row with a SKU name must contain a stored number. An empty cell makes `fillConditionalAttributeLevels` return without storing that attribute. Next on Conditional List Values then reports `The attribute Price, Size, doesn't have values`.

Level sheets are the usual place formulas lose their cached values. Save calculated numbers in those cells before the builder reads the file.

`Price Attribute Levels`, with `Levels Count` 2:

| SKU Name | Level 1 | Level 2 |
|----------|--------:|--------:|
| Levels Name | -0.5 | 0 |
| Afia 1.5L | 10.95 | 21.88 |
| Afia 9L | 47.25 | 94.5 |

`Size Attribute Levels`:

| SKU Name | Level 1 | Level 2 |
|----------|--------:|--------:|
| Levels Name | -0.2 | 0 |
| Afia 1.5L | 1.2 | 1.5 |
| Afia 9L | 7.2 | 9 |

## HBU workbook

Chosen in a second dialog. The builder reads the first sheet only.

The column count must be at least `2 + sum of Levels Count` across every attribute, including one SKU level per product row. Fewer columns repeats the dialog: `Columns count is not correct`.

Columns are taken by position, not by header text:

| Position | Stored as |
|----------|-----------|
| 1 | Ignored. Respondent ids are renumbered from 1 in row order |
| 2 | Ignored. `RespondentWeight` is stored as `"1"` for every row |
| Next `Levels Count` of SKU | `ProductID1`, `ProductID2`, … in `Product List` order |
| Next levels of each later attribute | `{Attribute}Level1`, `{Attribute}Level2`, … in `Attributes` sheet order |

Attribute order follows the `Attributes` sheet. A Price column placed before the SKU block will be read as a SKU utility.

The running study multiplies utility by the stored `RespondentWeight`. Because this import writes `"1"`, that factor does not change shares unless the weight is edited in the study database afterward. See [PREFERENCE.md](PREFERENCE.md) and [STUDY_DATABASE.md](STUDY_DATABASE.md).

For the attribute sheet above (2 SKUs, Price × 2, Size × 2) the first sheet needs 8 columns. Headers are not used:

| Respondent ID | RLH | ProductID1 | ProductID2 | PriceLevel1 | PriceLevel2 | SizeLevel1 | SizeLevel2 |
|--------------:|----:|-----------:|-----------:|------------:|------------:|-----------:|-----------:|
| 45 | 0.8 | 1.2 | -0.4 | -1.1 | -2.4 | 0.3 | 0.1 |
| 47 | 1.4 | 0.2 | 0.9 | -0.6 | -1.8 | 0.5 | -0.2 |

Stored weight for both rows is `1`. Respondent ids become 1 and 2. `0.8` and `1.4` are discarded. `ProductID1` is the utility of `Afia 1.5L`.

## Errors that stop the build

A bad cell is often only written into the import log. The dialog that blocks Next or Finish comes later, when that sheet was never stored.

| When | Dialog | Cause |
|------|--------|--------|
| Opening the design file | `{message} in the attribute sheet` / `Can't Process the automtion` | An `Attributes` row is missing a cell, or neither `Discrete Type` nor `Conditional Type` is `1` |
| Opening the design file | `Base products in the design file has wrong values` | `Base Product` is not empty and is not a `SKU Name` in `Product List`. The import continues |
| HBU dialog | `Columns count is not correct` | The first HBU sheet has fewer columns than `2 + sum of Levels Count`. The dialog stays open until a file with enough columns is chosen, or until it is cancelled |
| Next on Attribute Levels | `The number of elements for " {name} " doesn't equal the levels count` | The levels sheet has fewer names than `Levels Count`, or the sheet was not read |
| Next on Design File | `The selected cells has empty string or null value` | A product cell the grid requires is empty. A formula with no cached value arrives as empty |
| Next on HBU File | `The selected cells has empty string or null value` | An HBU cell is empty, or the HBU dialog was cancelled and the grid was left blank |
| Next on Conditional List Values | `The attribute Price, Size, doesn't have values` | `fillConditionalAttributeLevels` returned before storing the attribute. A `Level n` cell on a SKU row was empty, almost always a formula with no cached value |
| Finish | `Grouping has some Missing Value` | Grouping is on and a grouping cell is empty |
| Finish | `Segmentation has some Missing Value` | Segmentation is on and a respondent cell is null |
| Finish | `Foreign Key Error at row {n} & column {m}` | A `Segment {Type}` cell is not a member id of that type. Ids restart at 1 for each type |
| Finish | `The Microsoft Jet database engine cannot find the input table or query 'ConditionalPriceList'` | An `ALTER TABLE` failed, so `ConditionalPriceList` was never created. The usual cause is a segment member name that is not a legal unquoted Access identifier (`20-24`, `20_24`, a leading digit, a space, or a hyphen) on `Segments Shares & WD`, on a `(Member) MSU` / `(Member) VSU` label, or on `Excluded Sales Unit` |

`Can't Access {sheet} Sheet` and `missing value in the {sheet} level {n} Column` are returned to the import log. They do not open their own dialog. The missing levels sheet shows up on Next as a levels-count error. The missing level value shows up as `doesn't have values`.
