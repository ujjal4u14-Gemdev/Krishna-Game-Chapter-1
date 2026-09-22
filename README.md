# Bal Ganesha — Chapter 1

Unity 6 portrait-mobile foundation for **Bal Ganesha and the Festival of Wisdom**. It preserves the 54-level puzzle structure verified from the supplied reference walkthrough while replacing its story, cast, environments, dialogue, art and audio with an original Bal Ganesha adaptation. Reference-game artwork and audio are intentionally not included.

## Bal Ganesha production scope

- 54 levels across two story arcs
- Arc 1, **The Missing Modaks**: Levels 1–25
- Arc 2, **The Shadow Naga Cave**: Levels 26–54
- Three reusable gameplay families cover the full chapter:
  - Erase / Reveal — 13 levels
  - Binary Choice / Route — 15 levels
  - Aim & Shoot — 26 levels

Start with the [`Bal Ganesha production pack`](Docs/Bal_Ganesha/README.md). The original [`reference level matrix`](Docs/Chapter_1_Level_Matrix.md) remains only as mechanical evidence.

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

- Put a modak in Ganesha's hand
- Put a festival ornament into the correct silhouette
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
- Ready-to-wire tile eraser and trajectory-based aim/shoot controllers
- JSON progress saving and centralized scene loading
- Separate reusable UI assembly and navigation foundation
- Safe-area fitter plus Unity setup and validation menu tools
- Dedicated UI and code-segmentation documents
- Sample level JSON and planning CSV

## Open the project

1. Install Unity `6000.0.84f1` or a compatible Unity 6000.0 LTS patch.
2. Clone this repository and open its root in Unity Hub.
3. Allow Unity Package Manager to restore packages.
4. Run `Tools > Bal Ganesha Game > Setup Project` once scripts compile.
5. Open the generated GameplayTemplate and follow `Docs/Vertical_Slice_Setup.md` for Levels 1, 6 and 15.

## Recommended first vertical slice

Build Levels 1, 6 and 15 first. Together they validate every Chapter 1 mechanic. Once their UI, save flow and action sequences work, the remaining 51 levels are primarily data and art authoring.

## Play the included greybox demo

After the first Unity import finishes without compiler errors:

1. Run `Tools > Bal Ganesha Game > Create Playable Greybox Demo`.
2. Unity creates and opens `Assets/_Project/Scenes/Samples/PlayableGreyboxDemo.unity`.
3. Press Play.
4. Complete Erase/Reveal, Binary Choice and Aim & Shoot in sequence.

The demo uses generated colored shapes and contains no final copyrighted artwork. It exists to verify the complete input, evaluation, reset, sequencing and completion pipeline before art production.

## Rights note

The walkthrough is used only as a gameplay-structure reference. Use artwork, music, character designs, trademarks and story text only when you own them or have permission. The Bal Ganesha story in this repository is an original family-game adaptation, not a claim of scriptural canon; complete cultural review before release.
