# Export

Source: `ExcelSheet.automateDataset`.

`automateDataset(chartSet, drawChart, createPowerPoint)` opens a visible Excel workbook and writes one worksheet per table in the chart `DataSet`.

## Sheet

The sheet name is the table name. Row 1 is a merged title (the table name), white text on a dark fill, 16 pt. Row 2 is the column headers, white text, bold, 11 pt, with an autofilter. Data starts at row 3. The first column is left-aligned and shaded. The used range is Tw Cen MT, centered, with borders, and columns autofit.

When neither a chart nor PowerPoint is requested, numeric columns from column 2 downward are set to `0.00` unless the format is already `0.00%`.

## Chart on the sheet

`drawChart` adds an Excel chart chosen by `ExtendedProperties["ChartName"]`. `S.Index And Revenues` does not draw a chart; its numbers are forced to `0.00`. `V.Share And Index` and `Penetration And Index` are written as sheets. The chart switch has no branch for those two names.

| Chart name | Excel chart |
|------------|-------------|
| `Price Index And Revenues` | Line with markers. Values `0.00%`, axis `Volume Share (%)`, category `Price Index`. A last row whose name ends with ` Revenues` moves to the secondary axis (dashed, square markers, `0.00`, title `Revenue Indicator (in millions)`). A row named exactly `Company Shares` is dashed with triangle markers. |
| `Brand Interaction` | Stacked columns, `0.00%`. The value axis is removed. Category tick labels are hidden. |
| `Fair Share` | Clustered bar, numbers `0.00`. The source range is the name column and the Index column, skipping the row whose Index cell is `0`. The series is named `Fair Share`. The value axis crosses at 1. |
| `SOM VS Brand Share` | First series clustered columns named `SOM`, second series a line with diamond markers named `Brand Share`. Values `0.00%`. The value axis is removed. |
| `Shares And R.Index` | A blank name row splits the table. Rows above it are clustered columns, `0.0%`, axis `Volume Share (%)`. Rows below it are lines on the secondary axis, `0.00`, title `Revenue Index`. If the blank row is first, only the revenue-index line is drawn. |
| `Elasticity_Product`, `Elasticity_Group`, `Elasticity_Company` | Line. Header level names formatted `0%`, body `0.00%`, sorted descending on the first level column. Value axis `0.0%`. |
| `EX Intro`, `EX Removal`, `CUM Intro Market Share`, `CUM Intro Revenue`, `CUM Removal Market Share`, `CUM Removal Revenue` | Line with markers. Trailing rows whose second cell is `Reversal Effect Happened` are left off the chart. Revenue is the secondary axis (`Revenue Indicator (in millions)`). Market share is `0.00%` with axis `Volume Share (%)`. |
| `Validation` | Line, `0.00%`. Series named `Audit Share` (with a 10% error bar) and `Rebased Simulator Share`. Axis title `Volume Share (%)`. Chart title is the sheet name. |

Series whose names are in `ProjectCore.ExcelColorNumberFormat` take that color. The legend sits on top except when the table name is `Brand Interaction` or contains `Elasticity`. Data labels show the value. The chart font is Tw Cen MT, 12 pt bold.

## PowerPoint

`createPowerPoint` saves a copy under the temp path as `Automated charts.xls` (Excel 12 is saved as xlsx and converted to the older workbook), then builds a slide deck from those charts and leaves the workbook unsaved so Excel still holds it open.
