# Krishna Game — Chapter 1

Unity 6 portrait-mobile foundation for recreating the 54-level walkthrough supplied for *Krishna Stories*. This repository starts with the reusable engine and production plan; original game artwork and audio are intentionally not included.

## Scope confirmed from the walkthrough

- 54 requested levels across two in-app story cards
- Story 1, **Collect Makhan**: Levels 1–25
- Story 2, **Aghasura's Cave**: Levels 26–54
- Three reusable gameplay families cover the full chapter:
  - Erase / Reveal — 13 levels
  - Binary Choice / Route — 15 levels
  - Aim & Shoot — 26 levels

The authoritative level-by-level production matrix is in [`Docs/Chapter_1_Level_Matrix.md`](Docs/Chapter_1_Level_Matrix.md).

## Engine lock

- Unity 6000.0 LTS, portrait mobile
- Reference resolution: 1080 × 1920
- 2D Renderer / URP
- Unity Input System or pointer events
- Spine optional through an animation adapter
- DOTween optional through a sequence adapter
- ScriptableObjects for authored levels

The starter code contains no required third-party dependency. It can compile using standard Unity components first, then receive Spine and DOTween adapters later.

## Core idea

`Level = Scene + Actors + Puzzle Definition + Rule + Outcome Sequences`

The same Drag & Drop puzzle can become:

- Put butter in Krishna's hand
- Put the flute into the correct silhouette
- Give food to the correct animal
- Place stones to complete a bridge
- Match a weapon to the correct character

Only the configuration and artwork change.

## Included

- Puzzle taxonomy and MVP priority
- Production pipeline
- Runtime architecture
- Level and puzzle ScriptableObject schemas
- Input signal router
- Puzzle state machine
- Evaluator registry
- Implemented reusable evaluators:
  - Erase / Reveal
  - Binary Choice / Route
  - Aim & Shoot
  - Tap Select, Multi Select, Sequence Order, Drag & Drop and Swipe Direction
- Data-driven action sequence runner
- Separate reusable UI assembly and navigation foundation
- Dedicated UI and code-segmentation documents
- Sample level JSON and planning CSV

## Open the project

1. Install Unity `6000.0.84f1` or a compatible Unity 6000.0 LTS patch.
2. Clone this repository and open its root in Unity Hub.
3. Allow Unity Package Manager to restore packages.
4. Follow `Docs/Vertical_Slice_Setup.md` to create the Bootstrap and first gameplay scene.

## Recommended first vertical slice

Build Levels 1, 6 and 15 first. Together they validate every Chapter 1 mechanic. Once their UI, save flow and action sequences work, the remaining 51 levels are primarily data and art authoring.

## Rights note

The walkthrough is used as a gameplay reference. Use artwork, music, character designs, trademarks and story text only when you own them or have permission. The framework deliberately keeps content separate so licensed or original replacement art can be used without changing puzzle logic.
