# Brand interaction

Source: `ProjectCore.crossBrandInteractionPerProduct` and `crossBrandInteractionPerGroup` (each has a basic-scenario overload and a default-market overload). `ChartForm.brandInteractionDoneButton_Click` chooses which one.

Each checked scenario is loaded into a new `ProjectCore` with `bindScenarioDatatableObjectToProjectProducts`. The basic scenario is `Default` or another library scenario (`Current` and `Base` are not listed).

The cell is a share difference divided by 100.

## Against a basic scenario

SKU (`crossBrandInteractionPerProduct(basic, scenarios)`):

```text
(scenario.NewShare - basic.NewShare) / 100
```

A row exists when the product is included in at least one interacting scenario. Both projects run `findPredictedShares` first.

Group (`crossBrandInteractionPerGroup`):

```text
(scenario group new share - basic group new share) / 100
```

One row per member of the group type.

## Against the default market

When the basic scenario is `Default`, the overloads without a basic project compare to base share on the chart project:

```text
(scenario.NewShare - BaseMarketShare) / 100
(scenario group new share - group base share) / 100
```

`getProductShare("BaseMarketShare")` on a replaced product is the base product's base share.

Checked series are kept. The screen chart is stacked columns labeled `0.00%`. Chart name `Brand Interaction`.
