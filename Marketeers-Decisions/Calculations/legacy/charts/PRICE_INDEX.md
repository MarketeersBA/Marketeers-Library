# Price index

Source: `ProjectCore.priceIndexAndRevenues` (SKU overload and group overload), `ChartForm.priceIndexDoneButton_Click`, `priceIndexRandomizeButton_Click`.

The tab sweeps a price index against a reference SKU and records volume share at each index. Company share and company revenue are always computed and then included only if those boxes are checked.

## Price at an index

For scale point `s` (the column header, parsed as a number):

```text
unitPrice = referencePrice / referenceSize
focalPrice = unitPrice * s * focalSize
```

`referencePrice` and `focalPrice` are the current Price attribute. `referenceSize` and `focalSize` are `SizeUnit` (`BaseSize`). The focal price is applied with `changeProductAttributeCombination`, then `findPredictedShares`.

Each included SKU gets a row. The cell is `NewShare / 100`. Two extra rows are `{CompanyName} Shares` (`getCompanyShare("NewShare") / 100`) and `{CompanyName} Revenues` (`getCompanyRevenue()`, not divided). Chart name `Price Index And Revenues`. The sweep runs inside an undo block that is flushed at the end.

## Group sweep

The group overload takes a group type, a member, the focal SKU, the reference SKU, and the scale.

Before the loop it records, for every SKU in the member,

```text
ratio = memberPrice / focalPrice
```

using prices before the sweep. At each scale point it sets the focal price as above, then sets each member SKU to `ratio * focalPrice`. Shares are read the same way as the SKU sweep.

The tab then collapses SKU rows: for each checked group member, each scale column is the sum of `NewShare / 100` of SKUs that belong to that member. SKU mode instead keeps the checked SKU rows and drops the rest.

## Random scale

Randomize needs a focal SKU and a reference SKU. It builds six index points. The first is `round(focalDefaultPrice / referenceDefaultPrice, 2)`. The other five are random percent steps between the Price level-name bounds:

```text
lowPrice = focalDefaultPrice * (1 + firstLevelName)
highPrice = focalDefaultPrice * (1 + lastLevelName)
step = random integer from firstLevelName*100 to lastLevelName*100
index = round( focalDefaultPrice * (step/100 + 1) / referenceDefaultPrice , 2 )
```

A draw is kept when it lies between `lowPrice / referenceDefaultPrice` and `highPrice / referenceDefaultPrice` and is not already in the list. The six values are sorted and written comma-separated into the scale box.
