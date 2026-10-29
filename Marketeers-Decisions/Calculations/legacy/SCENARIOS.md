# Scenarios

Source: `ProjectCore.getScenarioDatatableObject`, `bindScenarioDatatableObjectToProjectProducts`, `SKUAttributeConfiguration.saveToolStripButton_Click`, `saveScenarioToXmlFile`.

A scenario is the current product state plus the active segment. Chart tabs bind a saved scenario onto their own `ProjectCore`, then call `findPredictedShares`.

## Table

`getScenarioDatatableObject` builds a `DataTable` named with the scenario.

| Column | Contents |
|--------|----------|
| Product Name | |
| Included | `true` / `false` text |
| One column per conditional attribute | Current numeric value |
| One column per discrete attribute except SKU | 1-based level index |
| WD | `NewWD` |
| AuditShare | |
| NOS | |
| TDC | |

Extended properties `SegmentType` and `SegmentMember` come from `getSegmentation`. Both are empty when no segment is active.

## Bind

`bindScenarioDatatableObjectToProjectProducts`:

1. If `SegmentType` and `SegmentMember` are both non-empty, `setSegmentation` runs. Otherwise `resetSegmentation` runs.
2. Each row sets `Included`, each conditional value, each discrete index, `NewWD`, `AuditShare`, `NOS`, and `TDC`.
3. `scenarioName` becomes the table name.

Binding does not call `findPredictedShares`.

## Files

The study folder holds `Database` (the Access file) and `Scenarios`.

Saving from the grid opens the scenario name dialog. The name is rejected when `Scenarios.xml` already has a `Scenario` / `Name` with that text.

- `Scenarios\{name}.osl` is a binary-serialized scenario table (`BinaryFormatter` of `getScenarioDatatableObject`).
- `Scenarios.xml` gains a `Scenario` element with `Name`, `Description`, and attribute `Validation`.

`Validation` is `true` when the grid's Audit Share column was visible at save time, after every SKU audit cell parsed as a number. A bad audit cell blocks the save with “There is error with the audit shares”. Those scenarios are the ones the Validation chart lists. Other charts list the scenario library and skip names `Current` and `Base`.

The Shares & Revenues chart treats the name `Current` as a special case: it calls `resetProductValues` and `resetSegmentation` instead of loading a file.
