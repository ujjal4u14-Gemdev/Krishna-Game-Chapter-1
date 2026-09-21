# Code Segmentation

## Chapter 1 implementation decision

The walkthrough does not require the full general-purpose catalog for its first 54 levels. Production starts with three independently reusable folders:

| Folder | Rule | Level coverage |
|---|---|---:|
| `Puzzles/EraseReveal` | Reports mask coverage and succeeds at a configured threshold | 13 |
| `Puzzles/Choice` | Resolves a tapped option ID against the configured answer | 15 |
| `Puzzles/Aim` | Resolves a projectile target ID after scene physics | 26 |

Generic evaluators remain available for later chapters, but they are not on the Chapter 1 critical path.

## Top-level modules

```text
MythicPuzzle
├── Core
│   ├── Bootstrap
│   ├── StateMachine
│   ├── Events
│   ├── SceneEntities
│   └── Utilities
├── Content
│   ├── LevelDefinitions
│   ├── PuzzleDefinitions
│   ├── Catalogs
│   └── Validation
├── Sequencing
│   ├── Actions
│   ├── Runner
│   └── AnimationAdapters
├── Puzzles
│   ├── Common
│   ├── EraseReveal
│   ├── Choice
│   ├── Aim
│   ├── Selection
│   ├── Manipulation
│   ├── Gesture
│   ├── Logic
│   ├── Timing
│   └── Resource
├── UI
│   ├── Core
│   ├── Navigation
│   ├── Screens
│   ├── Gameplay
│   ├── Feedback
│   └── Widgets
├── Progression
│   ├── Save
│   ├── ChapterProgress
│   └── Rewards
├── Audio
├── Integrations
│   ├── Spine
│   ├── Tweening
│   ├── Analytics
│   ├── Ads
│   └── Store
└── Tests
```

## Puzzle categories

| Category | Puzzle modules |
| --- | --- |
| Selection | Tap Select, Multi Select, Hidden Object |
| Manipulation | Drag & Drop, Combine Items, State Toggle, Physics Balance |
| Gesture | Swipe Direction, Hold Charge, Trace Path |
| Logic | Sequence Order, Match Pairs, Route Choice, Count/Compare, Memory, Dialogue Choice |
| Timing | Timing Tap |
| Resource | Resource Battle |

Every category has its own folder, namespace, tests, prefabs, data validation and sample scene. Puzzle modules communicate with Core only through `PuzzleInputSignal`, `IPuzzleEvaluator`, `PuzzleEvaluation` and common events.

## Assembly boundaries

Recommended assembly definitions:

```text
MythicPuzzle.Core
MythicPuzzle.Content
MythicPuzzle.Sequencing
MythicPuzzle.Puzzles
MythicPuzzle.UI
MythicPuzzle.Progression
MythicPuzzle.Integrations
MythicPuzzle.Editor
MythicPuzzle.Tests
```

Use category folders and namespaces inside `MythicPuzzle.Puzzles` initially. Split every category into an independent assembly only if compile time or team ownership later justifies it. This avoids circular assembly references during the prototype.

## Dependency direction

```text
UI ───────────────┐
Puzzles ──────────┼──> Core + Content
Sequencing ───────┘
Progression ─────────> Core + Content
Integrations ────────> Adapter interfaces only
Editor ──────────────> All authoring modules
```

Core never references UI, a puzzle category, Spine, DOTween, analytics, ads or store SDKs.

## Rules for organized code

1. One primary behavior class per file. Closely coupled enums and serialized value contracts may share a contract file.
2. File name must match its public type.
3. Runtime and Editor code stay in separate assemblies.
4. No singleton access from puzzle evaluators.
5. No `FindObjectOfType` or string-based GameObject searches.
6. Scene objects are addressed through stable `SceneEntity` IDs.
7. UI uses events/read models, never direct puzzle internals.
8. Third-party SDK calls live only in Integrations.
9. Each puzzle module includes evaluator tests and one sample configuration.
10. No story-specific names inside reusable mechanic code.

## Example

Bad reusable class name:

`GiveFluteToKrishnaPuzzle`

Correct reusable class name:

`DragDropEvaluator`

The story-specific entities belong only in a `PuzzleDefinition`:

```text
Source: ENT_Flute
Destination: ENT_Krishna_Hand
Success sequence: SEQ_KrishnaReceivesFlute
Failure sequence: SEQ_MonkeyStealsFlute
```
