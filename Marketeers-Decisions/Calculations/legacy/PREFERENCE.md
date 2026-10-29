# Preference

Source: `Attributes.getAttributeLevelUtility`, `Attributes.interpolateUtiltiy`, `ProjectCore.loadUtilityMatrix`, `getProductUtility`, `getPreferenceShare`, `loadPreferenceShare`.

Preference share is the average, over selected respondents, of each person's choice probability. Only included products enter the choice set. Only attributes with `IsIncluded` add utility.

## Utility matrix

`Attributes.getDataRow` builds `attributeUtility[respondent, level]`.

- SKU columns are `ProductID` plus the level name.
- Every other attribute uses `{AttributeName}Level1`, `Level2`, … in level-name order.

A conditional attribute also loads `Conditional{Name}List`. Each product row has a `ProductCombination` of comma-separated calibrated values, one per level, in the same order as the level names.

## Discrete attribute

The product stores a 1-based level index. The utility is

```text
attributeUtility[respondent, levelIndex - 1]
```

## Conditional attribute

`getAttributeLevelUtility` walks that product's calibrated values.

- Equal to a calibrated value: return that level's utility.
- Strictly between two calibrated values: linear interpolation (below).
- Below the first calibrated value, or above the last: the method returns `NaN`. `getProductUtility` then throws `{product} has a base level out of boundry values`. There is no extrapolation.

```text
U = U_next - (v_next - x) * (U_next - U_prev) / (v_next - v_prev)
```

That is the same line as

```text
U = U_prev + (x - v_prev) / (v_next - v_prev) * (U_next - U_prev)
```

`v_prev` and `v_next` are the calibrated values around `x`. `U_prev` and `U_next` are the utilities at those levels for this respondent.

## Product utility

For one respondent and one product, over every included attribute:

```text
utility = RespondentWeight * sum(attribute utilities)
```

Base preference (`"BasePreferenceShare"`) reads each attribute from the default combination. Scenario preference (`"NewPreferenceShare"`) reads the current combination.

## Choice probability

```text
expUtility[respondent, product] = exp(utility * ExponentFactor)
```

For each selected respondent, included products are normalized:

```text
choiceProb[respondent, product] = expUtility / sum(expUtility of included products)
```

Respondents who are not selected are left un-normalized and are skipped by the average.

## Preference share

```text
preferenceShare[product] = 100 * sum(choiceProb over selected respondents) / selectedCount
```

`loadPreferenceShare` writes that into `BasePreferenceShare` or `NewPreferenceShare` for included products only.

The average is a headcount of selected respondents. `RespondentWeight` already scaled the utility. It is not applied again as a weight on the average.
