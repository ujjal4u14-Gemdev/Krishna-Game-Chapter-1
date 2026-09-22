# Scalable Content Pipeline Audit

Audit target: current repository at the Bal Ganesha redesign baseline, Unity `6000.0.84f1`, Android portrait. Status is based on files present in the repository; it does not claim a successful Unity Editor or Android-device run.

## Executive result

The project has a **good reusable gameplay core** but is **not ready for mass final-art production yet**. The evaluator/scene/sequence separation, assembly definitions, stable entity IDs and data-driven levels are the right foundation. The remaining risk is the content layer around that foundation: presentation metadata, Addressables ownership, localization, audio events, import presets, automated validation, CI and device budgets.

Complete the P0 gates below with the three-level vertical slice before commissioning all 54 levels. Artists can safely begin concepts, turnarounds, environment kits and the level briefs now; final export/integration should wait for the import and presentation contracts.

## Current-state scorecard

| Area | Status | Evidence | Decision |
|---|---|---|---|
| Puzzle reuse | Green | Three evaluator families cover 54 levels | Keep; no per-level scripts |
| Code boundaries | Green | Runtime, UI, Editor and Tests assemblies; adapter interfaces | Keep dependency direction |
| Level data | Amber | `LevelDefinition` and `PuzzleDefinition` exist | Add presentation metadata without polluting rule data |
| Scene references | Green/Amber | Stable `SceneEntity` IDs and duplicate checking | Add definition-to-scene cross-validation |
| Sequencing | Amber | Data-driven intro/success/failure commands | Add cancellation, skip policy and missing-target diagnostics |
| Save/progression | Amber | JSON store and progression service | Add atomic write, backup, schema version and migration |
| Input | Amber | Input System plus direct pointer fallback | Validate touch and mouse on target devices; prevent duplicate feeds |
| UI | Amber | Separate UI assembly and safe-area support | Add localization layout tests and accessibility settings |
| Asset loading | Red | Direct scene/assets; no Addressables policy | Add local Addressables groups before chapter scale-up |
| Localization | Red | No string/asset tables | Use stable keys from the first final line of text |
| Audio | Red | No catalog/mixer/import contract | Add mixer buses, event catalog and voice ownership |
| Art import | Red | Folder intention only | Add texture/sprite/audio import presets and atlas policy |
| Validation/CI | Amber | Editor validator and evaluator EditMode tests | Add repository content validation, Unity batch compile/tests |
| Performance | Red | No device budget or captured profile | Lock low/mid target devices and profile builds on hardware |
| Version control | Amber | Text rules and LFS for PSD/Spine | Extend LFS types; define branch/PR ownership and lock large sources |
| Analytics | Red | No interface/event schema | Add privacy-reviewed adapter and stable event names before beta |
| Cultural review | Red | No formal gate | Add consultant approval at concept/script/voice milestones |

## P0 — required before mass final-art integration

### 1. Presentation data separate from puzzle rules

Create `LevelPresentationDefinition`/`PuzzlePresentationDefinition` assets or an equivalent catalog with:

- `storyTitleKey`, `objectiveKey`, `hintKey`, `successLineKey`
- environment kit ID, cast IDs and camera preset ID
- music, ambience and SFX event IDs
- thumbnail/address and optional voice address
- content schema version and production status

Keep these references out of evaluator code. Unity `ScriptableObject` assets are appropriate shared authored-data containers, but runtime save data stays in JSON/player storage rather than ScriptableObjects ([Unity ScriptableObject manual](https://docs.unity3d.com/6000.0/Documentation/Manual/class-ScriptableObject.html)).

### 2. Addressables ownership and unload rules

Adopt Addressables for chapter scenes, environment kits, character prefabs, large audio and localized assets. Begin with local groups; remote delivery can be a later business decision. Every load must have one explicit owner and matching release/unload. Unity describes Addressables as asynchronous loading by address with dependency management across local or remote locations ([Unity Addressables manual](https://docs.unity3d.com/6000.0/Documentation/Manual/com.unity.addressables.html)).

Recommended groups:

| Group | Lifetime | Examples |
|---|---|---|
| `Core_Local` | App | UI shell, common feedback, fonts |
| `C01_Common_Local` | Chapter | Ganesha, Mushika, shared puzzle prefabs |
| `C01_ArcA_Local` | Arc | Palace/gallery kits, festival audio |
| `C01_ArcB_Local` | Arc | Forest/Naga/cave kits, cave audio |
| `C01_Voice_{Locale}` | Level/optional | Localized voice clips |

### 3. Localization from the start

Replace literal UI/story strings with keys before final copy. Support at least English and the first planned Indian language during vertical slice so font fallback, wrapping and voice lookup are tested early. Unity's Localization system uses table collections and runtime locale resolution, and its package supports Addressables plus CSV/XLIFF workflows ([Unity Localization manual](https://docs.unity3d.com/6000.7/Documentation/Manual/localization/_index.html)).

### 4. Audio contract

Create an `AudioEventCatalog`, the five mixer buses in the content contract, snapshot policy (normal, paused, cinematic) and import presets. Unity's Audio Mixer provides grouped routing and effect/mix control suitable for separate player settings ([Unity Audio Mixer manual](https://docs.unity3d.com/6000.0/Documentation/Manual/AudioMixerOverview.html)).

### 5. Import presets and atlases

Lock pixels-per-unit, filter mode, max size, alpha, mipmaps and Android compression by asset class. Pack atlases by shared load lifetime/environment kit so loading one level does not pull unrelated chapter art. Choose the Android texture format after device coverage tests rather than by guess; Unity documents platform-specific format selection ([Unity texture format guidance](https://docs.unity3d.com/6000.0/Documentation/Manual/texture-choose-format-by-platform.html)) and sprite-atlas creation ([Unity Sprite Atlas manual](https://docs.unity3d.com/6000.0/Documentation/Manual/sprite/atlas/create-sprite-atlas.html)).

### 6. Automated gates

- Repository check: exactly 54 level rows, unique IDs, correct arc ranges and exact 13/15/26 family counts.
- Unity batch check: import/compile, EditMode tests and project validator.
- Content check: missing references, duplicate entity IDs, wrong environment labels, unassigned sequences, literal player-facing strings.
- Build check: Android development build from a clean clone.

The repository now includes the first check. Unity test automation remains P0; Unity's Test Framework supports automated code tests and should cover both EditMode rules and PlayMode lifecycle/integration ([Unity testing manual](https://docs.unity3d.com/6000.4/Documentation/Manual/test-framework/test-framework-introduction.html)).

## P1 — required before content complete/beta

1. **Sequence robustness:** cancellation token/handle, scene-unload cancellation, skip behavior, timeout and missing-entity failure diagnostics.
2. **Save safety:** temp-file write then atomic replace, backup, schema version, migration tests and corruption fallback.
3. **Pooling:** projectiles, hit VFX, erase particles and common audio sources.
4. **Accessibility:** separate music/SFX/voice controls, reduced motion, haptic toggle, high-contrast hints, non-color-only feedback and readable localized text.
5. **Analytics adapter:** `level_start`, `puzzle_attempt`, `hint_used`, `puzzle_complete`, `level_complete`, `level_quit`; no direct vendor dependency in puzzle assemblies.
6. **Content debugging:** in-game level picker, complete/reset buttons, entity-ID overlay, sequence stepper and simulated save states in development builds.
7. **Cultural and rights approvals:** source/license field on every external asset; cultural sign-off for turnarounds, script and final voice.

## P2 — scale and live-content improvements

- Remote Addressables/CCD only if post-install content delivery is actually required.
- Automated screenshot comparison at three aspect ratios.
- Localization pseudo-language and overflow report.
- Bundle duplication report and memory regression snapshots.
- Per-level downloadable voice packs if install size demands it.
- Editor authoring window that creates a level, definitions, scene/prefab skeleton and tracker row from one approved brief.

## Performance budgets to lock during vertical slice

These are project targets, not measured results yet:

| Budget | Initial target |
|---|---|
| Frame rate | Stable 60 fps preferred; 30 fps supported on low target device |
| Frame time | 16.7 ms preferred / 33.3 ms low-tier maximum |
| Gameplay GC allocation | 0 B per frame during Awaiting Input; no visible collection hitch |
| Scene transition | Under 2 s warm / 4 s cold on low target device, with visible progress |
| Texture memory | Defined after Level 25 and Level 54 representative captures |
| Peak memory | Defined from device class; must leave safe OS headroom |
| Initial install | Tracked in CI; voice and unused source files excluded |

Do not approve budgets using Editor-only results. Unity recommends collecting realistic performance data from a build running on the target platform/device ([Unity target-device profiling](https://docs.unity3d.com/6000.0/Documentation/Manual/profiling-target-device.html)).

## Version-control policy

- `main` is always importable; content work uses short branches and pull requests.
- One owner locks each large layered source file while editing to avoid unmergeable conflicts.
- Scenes/prefabs are kept small and modular; prefer prefab variants and separate data assets.
- Review `.meta` file changes with their asset and never copy an asset without its `.meta` unless intentionally creating a new GUID.
- Store large binary sources through Git LFS. GitHub explains that LFS commits pointer files while the large objects are stored separately ([GitHub LFS documentation](https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-git-large-file-storage)).
- Do not commit `Library`, generated builds, caches, raw captures or licensed reference material.

## Architecture judgment

The current neutral `MythicPuzzle` runtime namespace is correct and should **not** be renamed to Bal Ganesha. It keeps the engine reusable for future story packs. Product branding belongs in content, UI and build configuration. Existing assembly definitions are also the right direction: Unity notes they improve dependency control, architecture clarity and recompilation behavior as codebases grow ([Unity assembly definition manual](https://docs.unity3d.com/6000.0/Documentation/Manual/assembly-definition-files.html)).

## Go/no-go gates

| Gate | Required proof |
|---|---|
| Start concept art | This 54-level brief approved; cast and palette approved |
| Start final character rig | Ganesha turnaround and Level 1 greybox framing approved |
| Start final environment exports | Import presets, atlas policy and safe-area overlay approved |
| Start levels 2–54 integration | Levels 1, 6 and 15 pass correct/fail/reset/hint on Android |
| Content complete | All 54 rows Integrated; automated checks and device budgets pass |
| Release candidate | Localization, accessibility, cultural, rights and regression sign-off |

## Immediate implementation order

1. Apply this production pack and run the repository content validator.
2. Approve the Bal Ganesha turnaround, five environment kits and prop replacement map.
3. Add presentation definitions, localization keys, audio catalog and mixer.
4. Add Addressables local groups and explicit lifetime ownership.
5. Build final-quality Levels 1, 6 and 15; test on Android.
6. Freeze import/rig/sequence conventions, then batch-produce the remaining levels by kit.
