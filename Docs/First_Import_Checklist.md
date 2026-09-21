# First Unity Import Checklist

Use Unity `6000.0.84f1` or a compatible Unity 6 LTS patch.

1. Open the repository root from Unity Hub and let Package Manager finish.
2. Confirm there are no compile errors in Console.
3. Run `Tools > Krishna Game > Setup Project`.
4. Open `Assets/_Project/Scenes/Templates/GameplayTemplate.unity`.
5. Create a `LevelDefinition` and `PuzzleDefinition` from the Assets Create menu.
6. Assign the level to `Gameplay Systems/PuzzleDirector`.
7. Add scene objects with stable `SceneEntity` IDs matching the puzzle definition.
8. Add an EventSystem and the scene-side interaction controller described in `Vertical_Slice_Setup.md`.
9. Run `Tools > Krishna Game > Validate Project`.
10. Enter Play Mode, verify success and failure/reset, then test a portrait Android build.

Do not begin mass level production until greybox Levels 1, 6 and 15 have passed on a target phone. Those three scenes cover every Chapter 1 mechanic.
