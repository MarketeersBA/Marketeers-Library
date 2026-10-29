# Engine switches

Source: `Engine`.

`Engine` is a serializable set of switches. It does not calculate shares. Callers read the flags to decide which metrics and tools are available.

| Switch | Meaning |
|--------|---------|
| `VolumeShare` | Volume share |
| `PreferenceShare` | Preference share |
| `Penetration` | Penetration |
| `ValueShare` | Value share |
| `VSU` | Value sales units |
| `Revenue` | Revenue |
| `NosAndTdc` | NOS and TDC |
| `Charts` | Chart Analysis |
| `PackSimulator` | Pack simulator |
| `Products` | Product-name list carried with the engine |

The serialized form uses those names. `TwoMonthMessage` is stored under the key `DisplayMessage`.
