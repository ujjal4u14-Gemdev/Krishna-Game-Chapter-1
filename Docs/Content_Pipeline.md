# Content and Animation Pipeline

## Stage 1 — Level brief

Write one sentence for each beat:

> Bal Ganesha must release the hanging modak jar by choosing the safe tool.

Define the correct action, one or two plausible failures, and the visual payoff before creating art.

## Stage 2 — Choose a template

Select an existing puzzle type. A new mechanic requires approval only when none of the existing input and rule combinations can express the idea.

## Stage 3 — Greybox

Build the level using colored rectangles and temporary targets. Prove input, evaluation, reset, hint, success and failure before producing final artwork.

## Stage 4 — Art package

Deliver layered source art with:

- Background separated into near, middle and far layers
- Characters separated for the selected master rig
- Props exported independently with generous pivot-safe padding
- Hidden areas repainted behind removable objects
- Expression and hand variants named consistently
- No important object baked into the background

## Stage 5 — Prefab assembly

- Add `SceneEntity` and a stable ID.
- Assign sprite sorting layer and pivot.
- Add interaction component only to active targets.
- Connect the animation adapter.
- Save actors and recurring props as prefabs.

## Stage 6 — Configure data

- Create or duplicate a `PuzzleDefinition`.
- Assign puzzle type and correct IDs/pairs/order.
- Assign intro, success and failure sequences.
- Add the puzzle to the correct `LevelDefinition`.

## Stage 7 — Animation budget

Use the cheapest technique that communicates the action:

1. Transform tween
2. Pose or expression swap
3. Reusable skeletal clip
4. VFX and camera enhancement
5. New custom animation only when the first four cannot sell the moment

## Stage 8 — QA checklist

- Correct solution works on the first attempt.
- Every wrong target has visible feedback.
- Input is locked during outcome animation.
- Reset restores all transforms and attachments.
- Hint does not reveal a wrong target.
- Success cannot fire twice.
- The puzzle works at 20:9 and common tablet ratios.
- No essential interaction sits under safe areas or system gestures.
- The level remains understandable with sound disabled.

## Naming

```text
LVL_C01_001_RevealModaks
PZ_C01_001_RevealRibbon
SEQ_C01_001_Intro
SEQ_C01_001_Success
SEQ_C01_001_Fail_WrongPot
ENT_Ganesha
ENT_ModakJar_Correct
ENT_ModakJar_Decoy_A
```

## Definition of done

A level is not complete when its success animation works. It is complete when intro, interaction, wrong answer, reset, hint, success, transition, safe-area layout and low-end Android performance have all been verified.
