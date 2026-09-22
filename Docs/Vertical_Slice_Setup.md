# Vertical Slice Setup

Build Levels 1, 6 and 15 before producing the remaining content. They prove every Chapter 1 puzzle family.

## Bootstrap scene

After the first Unity import, run `Tools > Bal Ganesha Game > Setup Project`. It creates the standard folders, a Bootstrap scene, a GameplayTemplate scene and their Build Settings entries. The Bootstrap scene contains:

- `JsonProgressStore`
- `LevelSceneLoader`

Add the chapter-select UI and audio service when their art/audio assets are available.

## Gameplay scene template

The generated `Assets/_Project/Scenes/Templates/GameplayTemplate.unity` contains:

- `PuzzleDirector`
- `PuzzleInputRouter`
- `ActionSequenceRunner`
- gameplay camera
- `ChapterProgressService` and `JsonProgressStore`
- roots ready for scene content and gameplay UI

Add a Canvas with `SafeAreaFitter`, `GameHUDController`, correct/wrong feedback groups, and separate `Actors`, `Interactive`, `Background`, `Foreground`, `VFX` and `Audio` roots for the first vertical slice.

Every runtime target gets a `SceneEntity` with a stable ID. Art objects never call `PuzzleDirector` directly; input adapters raise `PuzzleInputSignal` values through the router.

## Level 1 — Erase / Reveal

1. Divide the removable overlay into small sprite tiles with `Collider2D` and `ErasableTile`.
2. Put `TileEraseController` and `SceneEntity` on the pointer-receiving mask root; set its ID to `rope_mask`.
3. Configure an `EraseReveal` puzzle with `correctEntityIds = [rope_mask]` and `requiredProgress = 0.7`.
4. Create shared intro, success and failure sequence assets.

`TileEraseController` is the included low-cost mobile implementation. A future shader/RenderTexture eraser can report through `EraseProgressSource` without changing puzzle rules.

## Level 6 — Binary Choice

1. Create two choice-card buttons, each with `SceneEntity` and `PuzzleTapTarget`.
2. Configure `BinaryChoice` with the safe card ID in `correctEntityIds`.
3. Reuse the same choice prefab for Levels 6–10 and 30–39.

## Level 15 — Aim & Shoot

1. Add `AimAndShootController`, assign its launch point, trajectory `LineRenderer`, camera and projectile prefab.
2. Put `ProjectileTarget`, `SceneEntity` and a `Collider2D` on the correct pot and every hazard/decoy.
3. Configure `AimAndShoot` with the pot ID in `correctEntityIds`.
4. Give the projectile prefab `Rigidbody2D`, a collider and `PuzzleProjectile`.

Physics, trajectory preview and projectile visuals stay scene-side. The evaluator only judges the resolved target ID, so later projectile levels reuse the same rule code.

## Definition checklist

For every level create:

- one `LevelDefinition`
- one `PuzzleDefinition`
- `Intro`, `Success` and `Failure` sequence assets
- stable entity IDs for all interactive targets
- a thumbnail and localization key set
- one happy-path test and one failure/reset test in the Unity scene

Before committing a level, run `Tools > Bal Ganesha Game > Validate Project`. It checks duplicate/empty IDs, missing puzzle targets, empty levels and open-scene entity IDs.
