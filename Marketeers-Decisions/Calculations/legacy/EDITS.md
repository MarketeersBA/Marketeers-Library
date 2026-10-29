# Edits

Source: `ProjectCore.changeProductAttributeCombination`, `includeExcludeProductScenarios`, `resetProductValues`, and the results grid in `SKUAttributeConfiguration`.

An edit marks the scenario name `Configured Scenario`. It does not itself rerun shares. The caller runs `findPredictedShares` after the edit.

## Attribute edit

`changeProductAttributeCombination(product, attribute, value)`.

| Column | Effect |
|--------|--------|
| `Audit Share` | `AuditShare =` the number with `%` removed. |
| `WD` | `NewWD =` the number. `0` also excludes the product and zeros `NewShare` and `NewPreferenceShare`. |
| Discrete | The level name is matched to `AttributeLevelsName`. The stored value becomes the 1-based index. |
| Conditional, value contains `%` | `current * (1 + percent / 100)`. The `%` is stripped before parsing. |
| Conditional, absolute, not zero | The number replaces the current value. |
| Conditional, zero | The product is excluded and its new share and new preference are zeroed. The attribute value is left unchanged. |

A conditional value that parses as 0, including `0%`, takes the exclude path above and does not write the attribute.

## Include and exclude

`includeExcludeProductScenarios`.

Excluding a product sets `Included` false and zeros new share and new preference.

Including a product that has `BaseProduct ≠ 0` (a replacement) first excludes every SKU with the same `BaseProduct`, and excludes the base product itself, zeroing their new share and new preference. Then the chosen SKU is included.

Including a product with `BaseProduct = 0` excludes every SKU whose `BaseProduct` equals this product's id, then includes this one.

On the grid, `Selected` toggles that call. A group header (`Product Index = -1`) applies the same included flag to each following visible SKU until the next header. Undo wraps the toggle as `IncludeExcludeProducts`.

## Bounds on the grid

`New WD` must be from 0 through 100 inclusive. Outside that, the edit is cancelled.

An absolute conditional value is checked against the cell tooltip, which is the product's first and last calibrated values (`Minimum value is` … `Maximum value is`). Above the maximum, or below the minimum, the edit is cancelled.

A value that contains `%`, and the literal `0`, are checked against the attribute level names instead. `getAttributeLevelsBoundries` parses the first and last level names as numbers. The entered number (percent sign removed) must sit between `first * 100` and `last * 100`. The dialog text is reversed relative to the test: a value under the low end is reported as outside the maximum, and a value over the high end is reported as outside the minimum.

A group-header edit looks up those same level names and divides the entered number by 100, then tests “below the low end and above the high end”. That conjunction does not reject a value.

Empty input is cancelled.

Out of the calibrated list, `getAttributeLevelUtility` still returns `NaN` and `findPredictedShares` throws `{product} has a base level out of boundry values`. The grid check and the utility check are different gates. The utility gate uses the product's `ProductCombination` values. The percent gate uses the level names.

## Restore defaults

`resetProductValues` runs `resetProductTypes`, copies every default attribute back onto the current combination, sets `NewWD = OriginalWD`, clears audit share, sets `CurrentShare = BaseMarketShare`, zeros new share and new preference, and names the scenario `Default`.

## Undo

`undoOperation` / `redoOperation` call `UndoRedoManager`. The product fields on that stack are `NewWD`, `NOS`, `TDC`, `Included`, `AuditShare`, and the attribute combination. `flushUndoRedoHistory` clears it. On the grid, Ctrl+Z undoes and Ctrl+Y redoes. Ctrl+C / Ctrl+V copy and paste CSV into the grid.
