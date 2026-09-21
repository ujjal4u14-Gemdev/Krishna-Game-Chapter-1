# UI Framework

## Principle

UI observes game state and sends player intent. UI never evaluates puzzle answers, moves story actors, or owns level progression.

## UI layers

| Layer | Responsibility | Examples |
| --- | --- | --- |
| System | App-wide protection and transitions | Safe area, loading, fade, connectivity |
| Navigation | Screen routing | Home, chapter map, level select, settings |
| Gameplay HUD | Persistent puzzle controls | Level number, hint, pause, restart |
| Interaction | Contextual guidance | Hand pointer, drag trail, target glow |
| Feedback | Short outcome messages | Correct, wrong, try again, reward |
| Modal | Blocking overlays | Pause, quit confirmation, reward, chapter complete |
| Debug | Development-only tools | Skip puzzle, complete level, entity IDs |

## UI screens

- Bootstrap / Loading
- Home
- Chapter Map
- Level Select
- Gameplay HUD
- Pause
- Settings
- Reward
- Chapter Complete
- Download / Update Content
- Privacy and parental information

## Reusable gameplay widgets

- `LevelProgressWidget`
- `HintButton`
- `RestartButton`
- `PauseButton`
- `ChoiceButton`
- `InteractionHand`
- `TargetHighlight`
- `CorrectFeedback`
- `WrongFeedback`
- `RewardBurst`
- `DialoguePanel`
- `TutorialCoachmark`

## UI state flow

```text
Boot → Home → Chapter Map → Gameplay
                             ├── Pause
                             ├── Failure Feedback → Retry
                             ├── Success Feedback → Next Puzzle
                             └── Chapter Complete → Chapter Map
```

## Code boundaries

### UI may know

- Current puzzle state
- Current level and puzzle index
- Whether input is enabled
- Hint availability
- Player settings

### UI may not know

- Correct target IDs
- Puzzle evaluator rules
- Spine animation names for story actors
- Scene prop transforms
- Save-provider implementation
- Ads or analytics SDK APIs

## Navigation pattern

`UIScreenController` owns a stack of registered panels. Screens request navigation using `UIScreenId`; they do not reference each other directly. Modals are pushed on top and restore the previous screen when closed.

## Gameplay binding

`GameplayUIBinder` listens to `PuzzleDirector.StateChanged` and `PuzzleDirector.PuzzleChanged`:

- Intro: hide interaction widgets.
- AwaitingInput: enable hint and restart.
- Resolving: disable buttons.
- Failure: display failure feedback.
- Success: display success feedback.
- Complete: open level or chapter completion UI.

## Resolution rules

- Canvas Scaler reference: 1080 × 1920
- Match Width Or Height: 0.5 as the initial value
- Use anchors instead of absolute positions
- Apply safe-area padding only at the system-root layer
- Minimum touch target: 96 × 96 reference pixels
- Gameplay art and UI must use separate canvases/sorting spaces
- Test 16:9, 19.5:9, 20:9 and tablet layouts

