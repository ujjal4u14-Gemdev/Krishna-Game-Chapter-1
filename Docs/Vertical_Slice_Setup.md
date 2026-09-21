# Vertical Slice Setup

Build Levels 1, 6 and 15 before producing the remaining content. They prove every Chapter 1 puzzle family.

## Bootstrap scene

Create `Assets/_Project/Scenes/Bootstrap.unity` with one persistent root containing:

- `SceneRegistry`
- save/progression service (implementation milestone 2)
- audio service
- `UIScreenController`
- loading canvas

## Gameplay scene template

Create `Assets/_Project/Scenes/Templates/GameplayTemplate.unity` containing:

- `PuzzleDirector`
- `PuzzleInputRouter`
- `ActionSequenceRunner`
- gameplay camera and safe-area canvas
- `GameHUDController`
- correct/wrong feedback groups
- separate `Actors`, `Interactive`, `Background`, `Foreground`, `VFX` and `Audio` roots

Every runtime target gets a `SceneEntity` with a stable ID. Art objects never call `PuzzleDirector` directly; input adapters raise `PuzzleInputSignal` values through the router.

## Level 1 — Erase / Reveal

1. Add the visual eraser implementation of your choice.
2. Put `EraseProgressSource` on the mask and give its `SceneEntity` ID `rope_mask`.
3. Configure an `EraseReveal` puzzle with `correctEntityIds = [rope_mask]` and `requiredProgress = 0.7`.
4. Create shared intro, success and failure sequence assets.

The framework intentionally does not force one masking technology. A shader RenderTexture solution is best for freehand erasing; tiled sprite masks are simpler for low-end devices.

## Level 6 — Binary Choice

1. Create two choice-card buttons, each with `SceneEntity` and `PuzzleTapTarget`.
2. Configure `BinaryChoice` with the safe card ID in `correctEntityIds`.
3. Reuse the same choice prefab for Levels 6–10 and 30–39.

## Level 15 — Aim & Shoot

1. Add an aim controller that previews a sampled 2D trajectory.
2. Put `ProjectileTarget` on the correct pot and every hazard/decoy.
3. Configure `AimAndShoot` with the pot ID in `correctEntityIds`.
4. On collision, the projectile calls `ProjectileTarget.ResolveHit`.

Physics, trajectory preview and projectile visuals stay scene-side. The evaluator only judges the resolved target ID, so later projectile levels reuse the same rule code.

## Definition checklist

For every level create:

- one `LevelDefinition`
- one `PuzzleDefinition`
- `Intro`, `Success` and `Failure` sequence assets
- stable entity IDs for all interactive targets
- a thumbnail and localization key set
- one happy-path test and one failure/reset test in the Unity scene

