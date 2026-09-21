# Framework Blueprint

## Product structure

The game is a sequence of short narrative puzzle beats. Each beat has five states:

1. **Intro** — establish the problem using a short reusable animation sequence.
2. **Awaiting Input** — activate only the relevant interactive entities.
3. **Resolving** — lock input and evaluate the player's action.
4. **Outcome** — play success, soft-fail, or hard-fail feedback.
5. **Transition** — reset, continue to the next puzzle, or complete the level.

## Architecture

```text
LevelDefinition
├── Scene name and metadata
├── Ordered PuzzleDefinitions
└── Level completion sequence

PuzzleDefinition
├── Puzzle type
├── Input and rule configuration
├── Intro sequence
├── Success sequence
└── Failure sequence

PuzzleDirector
├── State machine
├── Puzzle evaluator
├── Input router
├── Scene registry
└── Sequence runner
```

## Runtime responsibilities

| System | Responsibility |
| --- | --- |
| `PuzzleDirector` | Owns puzzle lifecycle and progression |
| `PuzzleEvaluatorRegistry` | Creates the correct logic module from puzzle type |
| `IPuzzleEvaluator` | Evaluates inputs without knowing scene art |
| `PuzzleInputRouter` | Converts scene interactions into generic signals |
| `SceneRegistry` | Resolves stable entity IDs to GameObjects |
| `ActionSequenceRunner` | Plays intro, success and failure commands |
| `LevelDefinition` | Stores level order and metadata |
| `PuzzleDefinition` | Stores reusable mechanic configuration |

## Why this structure scales

- Art does not contain gameplay rules.
- Puzzle logic does not contain animation details.
- Animation clips do not decide success or failure.
- Every interactive object has a stable string ID.
- Success and failure are reusable action lists.
- Puzzle mechanics can be unit-tested without loading a Unity scene.

## Puzzle composition model

Do not think of the project as 18 separate puzzle engines. Think of it as three layers:

### Input primitives

- Tap
- Drag
- Swipe
- Hold
- Trace

### Rule primitives

- Select
- Match
- Order
- Route
- Timing
- State
- Resource

### Presentation primitives

- Move, rotate, scale and shake an entity
- Play an animation
- Swap a pose or expression
- Attach or detach a prop
- Spawn VFX
- Move or punch the camera
- Play sound or dialogue
- Enable, disable or reveal an entity

Most future puzzles are a different combination of these primitives.

## Dependency boundaries

The core puzzle layer must not directly reference Spine, DOTween, Addressables, analytics, ads, or a specific save SDK. Each external system should enter through an adapter. This keeps the engine testable and prevents package upgrades from breaking puzzle rules.

## Scene rules

- One scene may contain multiple puzzle beats.
- No puzzle code may search for objects by Unity object name.
- Every referenced object uses a `SceneEntity.EntityId`.
- Inputs are disabled while a sequence is resolving.
- Failure must be reversible unless the level explicitly defines a hard fail.
- A reset restores authored starting transforms and puzzle-local state.

## Suggested folders inside the final Unity project

```text
Assets/_Project/
├── Art/
│   ├── Characters/
│   ├── Environments/
│   ├── Props/
│   ├── UI/
│   └── VFX/
├── Audio/
├── Data/
│   ├── Levels/
│   ├── Puzzles/
│   └── Sequences/
├── Prefabs/
│   ├── Actors/
│   ├── Interactions/
│   └── VFX/
├── Scenes/
│   ├── Bootstrap/
│   └── Chapters/
├── Scripts/
│   ├── Runtime/
│   ├── Editor/
│   └── Tests/
└── ThirdParty/
```

## Companion specifications

- `Puzzle_Catalog.md` defines all reusable mechanics.
- `Content_Pipeline.md` defines level production and QA.
- `UI_Framework.md` defines screens, widgets and UI ownership boundaries.
- `Code_Segmentation.md` defines modules, categories and dependency direction.
- `Chapter_1_Production_Bible.md` tracks verified reference analysis and the final level-matrix fields.
- `Project_Handoff_Checklist.md` defines what is required to begin inside the real Unity repository.
