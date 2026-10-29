# Segments

Source: `Segments.getDataRow`, `ProjectCore.respondentSegmentationProcess`, `setSegmentation`, `resetSegmentation`, `setSegmentsShare`, `setSegmentsPrice`, `setSegmentsWD`, `setMSU`, `setVSU`.

A segment is a respondent filter plus a replacement of base share, base price, base WD, MSU, and VSU. It does not reweight respondents to a population. After the filter, preference is still the unweighted average of the selected people. `RespondentWeight` still multiplies utility. See [PREFERENCE.md](PREFERENCE.md).

## Stored mapping

When `UseSegmenting` is true the study loads:

| Table | Contents |
|-------|----------|
| `SegmentingTypes` | `TypeID`, `SegmentType`, `MembersCount` |
| `Segments` | `SegmentID`, `SegmentName`, `SegmentTypeID` |
| `RespondentSegmentation` | `RespondentID`, and `Segment{TypeID}` holding the member id for that respondent |

Respondent order in `respondentSelection` follows `HBUData` row order. `respondentSegmentationProcess` walks `RespondentSegmentation` in that same row order and sets `respondentSelection[row] = (Segment{TypeID} == member id)`.

## Applying a segment

`setSegmentation(type, member)` requires a non-empty type. It then:

1. Stores the type on `StudySegmentTypeStart` and the member on that segment's `SegmentMemberStart` (undoable).
2. `setSegmentsShare(member)` — if `SegmentsShares` has a column named for the member, each product's `BaseMarketShare` comes from that column. Otherwise it comes from `Products.BaseMarketShare`. Product types are reset afterward.
3. `setSegmentsPrice(member)` — column on `SegmentsPrice`, or `Base{Price}` on `Products`. Written to both the default price and the current price.
4. `setSegmentsWD(member)` — column on `SegmentsWD`, or `Products.WD`. Written to `OriginalWD` and `NewWD`.
5. Excluded sales units for the research-start group are reloaded for that member. See [GROUPS.md](GROUPS.md).
6. `setMSU` / `setVSU` — the member column on `SegmentsMSU` / `SegmentsVSU` when it exists, otherwise `Total`.
7. `respondentSegmentationProcess`, which recalculates base preference on the selected respondents and base penetration, and names the scenario `Configured Scenario`.
8. Base revenue, base value-sales units, and base value share are recomputed.

`calculateBasePreferenceShareOnBaseMarketProducts` temporarily includes every product whose `Products.Included` flag is true, runs `loadPreferenceShare("BasePreferenceShare")` and `setProductsBasePenetration`, then restores the included flags it saved at the start.

## Clearing a segment

`resetSegmentation` clears `StudySegmentTypeStart`, restores base share, WD, and price from `Products`, sets MSU, VSU, and excluded sales units from `Total`, resets product types, selects every respondent, recalculates base preference, and recalculates base revenue.

`initializeRespondentSelection` selects every HBU row and calculates base preference. That is the all-respondent state.
