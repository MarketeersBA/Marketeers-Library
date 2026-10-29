# Validation

Source: `ChartForm.validationDoneButton_Click`, `ProjectCore.findPredictedShares`, `rebaseProductNewShare`, `getTotalProductsAuditShare`.

The scenario list is the library entries saved while the Audit Share column was visible. See [SCENARIOS.md](../SCENARIOS.md). Series are SKUs or members of the selected group type. At least one series and one scenario are required.

For each scenario the chart project binds that scenario, runs `findPredictedShares`, then

```text
rebaseProductNewShare(getTotalProductsAuditShare())
```

which is

```text
NewShare *= (sum of every product's AuditShare) / (sum of every product's NewShare)
```

The two plotted columns, both divided by 100 so the axis can format them as percents:

| Column | SKU | Group |
|--------|-----|-------|
| Audit Share | `getProductShare("AuditShare") / 100` | `getGroupShare("AuditShare") / 100` |
| Rebased Simulator Share | `getProductShare("NewShare") / 100` | `getGroupShare("NewShare") / 100` |

Group audit share is the sum of member `AuditShare`. Group rebased share is the sum of member `NewShare` after the rebase, and only included members contribute new share.

## By brand and by month

**By brand** builds one table per checked series. The first column is the series name, and each row is a checked scenario. Table name `{series}Validation`.

**By month** builds one table per checked scenario. The first column is the scenario name, and each row is a checked series. Table name `{scenario}Validation`.

Chart name `Validation`. The screen chart is a line with axis format `P0`. The Excel chart names the series `Audit Share` and `Rebased Simulator Share` and puts a 10% error bar on audit share. See [EXPORT.md](../EXPORT.md).
