# Content Authoring Contract

## Purpose

This contract keeps story, art, audio and puzzle logic independently replaceable. A level is a composition of stable IDs; filenames and scene hierarchy names are not runtime contracts.

## Content ownership

| Layer | Owns | Must not own |
|---|---|---|
| `LevelDefinition` | Stable level ID, number, scene/address, ordered puzzles | Dialogue text, sprite filenames, hardcoded character names |
| `PuzzleDefinition` | Puzzle family, correct entity IDs, limits, sequence references | Scene object searches, UI layout, localized text |
| `LevelArtSet` + art slots | Replaceable sprites keyed by stable visual slot IDs | Success rules or gameplay entity IDs |
| Presentation data (remaining P0) | Story/objective/hint keys, environment kit, cast, camera preset, music/ambience event IDs | Success rules |
| Scene/prefab | Visual composition, colliders, stable `SceneEntity` IDs | Deciding which answer is correct |
| Sequence asset | Intro/success/failure actions | Puzzle evaluation |
| Localization tables | Player-facing strings and voice subtitle text | Level numbers or entity IDs |
| Audio catalog | Stable event IDs and mixer routing | Gameplay success decisions |

## Stable identifiers

Use ASCII, Pascal-case semantic suffixes and zero-padded numbers.

```text
LVL_C01_001
PZ_C01_001_RevealRibbon
SCN_C01_PalaceStore
KIT_ENV_PalaceStore
CHR_GANESHA_CHILD
ENT_C01_001_Rope_Target
SEQ_C01_001_Intro
SEQ_C01_001_Success
SEQ_C01_001_Fail_WrongSurface
LOC_C01_001_Objective
LOC_C01_001_Hint
AUD_MUS_Festival_Loop
AUD_SFX_LotusSeed_Launch_01
```

Rules:

- IDs never change after content reaches **Integrated**. Change display names instead.
- No runtime `GameObject.Find`, filename parsing or scene-object-name lookup.
- An entity ID is unique within a loaded scene. A level and puzzle ID is unique project-wide.
- Localization and audio keys are references, not literal player-facing content.
- Never encode language, platform or artist initials into a gameplay ID.

## Repository layout

```text
Assets/_Project/
├── Art/
│   ├── Characters/{CharacterId}/{Source,Export}
│   ├── Environments/{KitId}/{Backgrounds,Modules,Foregrounds}
│   ├── Props/{PropFamily}
│   ├── UI/{AtlasGroup}
│   └── VFX/{EffectFamily}
├── Audio/{Music,Ambience,SFX,Voice}/
├── Data/
│   ├── Chapters/C01/
│   ├── Levels/C01/
│   ├── Puzzles/C01/
│   ├── Presentation/C01/
│   ├── Sequences/C01/
│   └── Catalogs/
├── Localization/{StringTables,AssetTables}/
├── Prefabs/{Actors,Environment,Interactions,VFX}/
└── Scenes/Chapters/C01/
```

Source files larger than normal Git assets (`.psd`, `.kra`, `.blend`, `.aep`, lossless audio and video references) go through Git LFS. Unity `.meta` files are committed beside every asset and must never be regenerated casually.

## Level lifecycle

| State | Entry requirement | Exit gate |
|---|---|---|
| Briefed | Level row exists; gameplay locked | Story, target, hazards and reuse kit approved |
| Greyboxed | Mechanic prefab assembled with placeholder art | Correct/fail/reset/hint tested on device |
| Art Ready | Concept, layers, pivots and repaint approved | Export package passes import checklist |
| Integrated | Final sprites/rig/audio connected to stable IDs | No missing references; validator passes |
| Polish | Timing, camera, VFX and mix adjusted | Performance budget passes on low target device |
| Release Candidate | Localization, accessibility and analytics complete | Full chapter regression passes |

Art production must not begin at scale until the representative vertical slice for Levels 1, 6 and 15 is accepted. Cave art begins after Level 40 validates the second environment/aim skin.

## Art handoff package

Every task folder contains:

1. Approved concept and level row.
2. Layered master with named groups: `BG_Far`, `BG_Mid`, `Gameplay`, `FG_Near`, `Interactive_*`, `Repaint`.
3. Exported sprites with transparent padding and approved pivots.
4. One composition PNG with the 1080 × 1920 safe-area overlay.
5. Animation notes listing reusable clip IDs before any request for a new clip.
6. License/source note for fonts, brushes, textures and third-party elements.

## Audio handoff package

Every audio asset supplies event ID, category, duration, loop flag, loudness target, language, source sample rate, license/source and owner. Keep raw masters outside Unity import folders if they are not needed in a build. Export gameplay SFX as lossless source; set platform compression in Unity import presets.

Required buses:

```text
Master
├── Music
├── Ambience
├── SFX
├── Voice
└── UI
```

## Reuse and uniqueness budget

- One environment kit must support at least three levels; the five planned kits support all 54.
- One puzzle prefab per family, configured by data. Do not create `Level17Puzzle.cs`.
- At least 75% of character motion must use the shared clip library.
- Standard levels receive no unique code and no unique full-screen background.
- Hero levels are 1, 25, 29, 38 and 54. Only these are pre-approved for unique key art/camera animation.
- A new mechanic requires a short design record explaining why current input, rule and presentation primitives cannot express it.

## Scene assembly checklist

- Assign stable entity IDs before connecting definitions or sequences.
- Keep camera, HUD, puzzle root and outcome root separate.
- Store authored starting transform/state so reset is deterministic.
- Target collider must match the visible object; do not create invisible difficulty.
- Input is disabled during sequences and restored exactly once.
- Destroy/release spawned objects and handles when leaving the level.
- Put repeated projectiles/VFX in pools before content-complete review.
- Verify portrait phone, tall phone and tablet framing.

## Definition of done

A level is done only when gameplay, visual, audio, localization, accessibility, performance and regression gates pass. “The correct animation plays in Editor” is not a definition of done.
