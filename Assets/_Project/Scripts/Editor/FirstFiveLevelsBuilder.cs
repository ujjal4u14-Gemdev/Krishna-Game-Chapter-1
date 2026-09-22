using System.Collections.Generic;
using System.IO;
using MythicPuzzle.Runtime;
using MythicPuzzle.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MythicPuzzle.Editor
{
    /// <summary>
    /// Creates the production-slice data and five independent portrait scenes. The generated
    /// scenes are deliberately art-swappable: mechanics bind to SceneEntity IDs and visuals
    /// bind to SpriteArtSlot IDs through a LevelArtSet.
    /// </summary>
    public static class FirstFiveLevelsBuilder
    {
        private const string DataRoot = "Assets/_Project/Data";
        private const string SceneRoot = "Assets/_Project/Scenes/Chapters/C01";
        private const string GaneshaCrawlPath =
            "Assets/_Project/Art/Characters/CHR_GANESHA_CHILD/Export/CHR_Ganesha_Crawl.png";
        private const string LevelOneBackgroundPath =
            "Assets/_Project/Art/Environments/C01/BG_C01_001_Courtyard.png";
        private const string LevelOnePropsRoot = "Assets/_Project/Art/Props/C01";
        private const float PortraitWorldWidth = 6.1f;
        private const float PortraitWorldHeight = PortraitWorldWidth * 2532f / 1170f;

        private sealed class LevelSpec
        {
            public int Number;
            public string Title;
            public string Objective;
            public string TargetId;
            public string NextScene;
            public Vector3 SurfacePosition;
            public Vector2 SurfaceSize;
            public int Columns;
            public int Rows;
        }

        [MenuItem("Tools/Bal Ganesha Game/Build First 5 Playable Levels")]
        public static void Build()
        {
            EnsureFolders();
            EnsureSpriteImport(GaneshaCrawlPath);
            EnsureLevelOneSpriteImports();
            var specs = Specs();
            var buildScenes = new List<EditorBuildSettingsScene>();

            foreach (var spec in specs)
            {
                var intro = CreateIntroSequence(spec);
                var success = CreateSuccessSequence(spec);
                var puzzle = CreatePuzzle(spec, intro, success);
                var level = CreateLevel(spec, puzzle);
                var artSet = CreateArtSet(spec);
                var scenePath = ScenePath(spec.Number);
                if (!File.Exists(scenePath)) CreateScene(spec, level, artSet);
                else Debug.Log($"Preserving existing scene: {scenePath}");
                buildScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            MergeBuildSettings(buildScenes);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(ScenePath(1));
            Debug.Log("Levels 1-5 built. Level 1 is open; press Play and erase the highlighted obstacle.");
        }

        [MenuItem("Tools/Bal Ganesha Game/Apply 1170x2532 Portrait Art")]
        public static void ApplyPortraitArt()
        {
            EnsureLevelOneSpriteImports();
            CreateArtSet(Specs()[0]);
            PlayerSettings.defaultScreenWidth = 1170;
            PlayerSettings.defaultScreenHeight = 2532;
            PlayerSettings.defaultIsNativeResolution = false;
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

            for (var number = 1; number <= 5; number++)
            {
                var scene = EditorSceneManager.OpenScene(ScenePath(number));
                var camera = Object.FindFirstObjectByType<Camera>();
                if (camera != null) camera.orthographicSize = PortraitWorldHeight * 0.5f;
                var scaler = Object.FindFirstObjectByType<CanvasScaler>();
                if (scaler != null) scaler.referenceResolution = new Vector2(1170f, 2532f);

                var slots = Object.FindObjectsByType<SpriteArtSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var slot in slots)
                {
                    if (slot.SlotId == "BG_Far")
                    {
                        var size = new Vector2(PortraitWorldWidth, PortraitWorldHeight);
                        slot.transform.localScale = new Vector3(size.x, size.y, 1f);
                        Set(slot, "referenceSize", size);
                    }
                    if (number == 1 && (slot.SlotId == "BG_Mid_Palace" ||
                                        slot.SlotId == "BG_Floor" || slot.SlotId == "ENV_Palace_Arch"))
                        Object.DestroyImmediate(slot.gameObject);
                }
                if (number == 1)
                {
                    foreach (var label in Object.FindObjectsByType<TextMesh>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                        if (label.gameObject.name.StartsWith("Label THE FIRST MODAK JAR"))
                            Object.DestroyImmediate(label.gameObject);
                }
                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
            }
            AssetDatabase.SaveAssets();
            EditorSceneManager.OpenScene(ScenePath(1));
            Debug.Log("Portrait art applied to Levels 1-5 at 1170 x 2532; Level 1 art bound.");
        }

        private static List<LevelSpec> Specs() => new()
        {
            new LevelSpec
            {
                Number = 1,
                Title = "The First Modak Jar",
                Objective = "Rub the rope until it breaks and the hanging jar drops.",
                TargetId = "ENT_C01_001_Rope_Target",
                NextScene = SceneName(2),
                SurfacePosition = new Vector3(1.65f, 1.65f),
                SurfaceSize = new Vector2(0.42f, 2.5f),
                Columns = 1,
                Rows = 8
            },
            new LevelSpec
            {
                Number = 2,
                Title = "A Messy Path",
                Objective = "Rub away the butter spill and clay pieces blocking Ganesha's path.",
                TargetId = "ENT_C01_002_Spill_Target",
                NextScene = SceneName(3),
                SurfacePosition = new Vector3(0.15f, -2.15f),
                SurfaceSize = new Vector2(3.9f, 0.9f),
                Columns = 8,
                Rows = 2
            },
            new LevelSpec
            {
                Number = 3,
                Title = "The Cupboard Secret",
                Objective = "Rub away the cupboard door to reveal the three modak bowls.",
                TargetId = "ENT_C01_003_CupboardDoor_Target",
                NextScene = SceneName(4),
                SurfacePosition = new Vector3(0.85f, 0.7f),
                SurfaceSize = new Vector2(2.25f, 2.7f),
                Columns = 5,
                Rows = 6
            },
            new LevelSpec
            {
                Number = 4,
                Title = "The Balance Trick",
                Objective = "Rub away the small weight so the balance tips toward the modak jar.",
                TargetId = "ENT_C01_004_LightWeight_Target",
                NextScene = SceneName(5),
                SurfacePosition = new Vector3(-1.55f, 0.4f),
                SurfaceSize = new Vector2(1.0f, 1.0f),
                Columns = 3,
                Rows = 3
            },
            new LevelSpec
            {
                Number = 5,
                Title = "Which Pot Has Modaks?",
                Objective = "Rub away the correct lower cover to reveal the modak jar.",
                TargetId = "ENT_C01_005_LowerCover_Target",
                NextScene = string.Empty,
                SurfacePosition = new Vector3(0f, -0.65f),
                SurfaceSize = new Vector2(1.65f, 1.55f),
                Columns = 4,
                Rows = 4
            }
        };

        private static ActionSequenceAsset CreateIntroSequence(LevelSpec spec)
        {
            return Sequence(
                $"SEQ_C01_{spec.Number:000}_Intro",
                Step(ActionStepType.SetActive, "level_complete_visual", boolValue: false));
        }

        private static ActionSequenceAsset CreateSuccessSequence(LevelSpec spec)
        {
            var name = $"SEQ_C01_{spec.Number:000}_Success";
            switch (spec.Number)
            {
                case 1:
                    return Sequence(name,
                        Step(ActionStepType.Wait, duration: 0.15f),
                        Step(ActionStepType.MoveTo, "jar_hanging", new Vector3(1.55f, -1.8f), 0.55f),
                        Step(ActionStepType.SetActive, "jar_hanging", boolValue: false),
                        Step(ActionStepType.SetActive, "level_complete_visual", boolValue: true));
                case 2:
                    return Sequence(name,
                        Step(ActionStepType.MoveTo, "ganesha_actor", new Vector3(1.25f, -2.15f), 0.75f),
                        Step(ActionStepType.SetActive, "level_complete_visual", boolValue: true));
                case 3:
                    return Sequence(name,
                        Step(ActionStepType.Wait, duration: 0.35f),
                        Step(ActionStepType.SetActive, "level_complete_visual", boolValue: true));
                case 4:
                    return Sequence(name,
                        Step(ActionStepType.RotateTo, "balance_beam", new Vector3(0f, 0f, -13f), 0.55f),
                        Step(ActionStepType.SetActive, "level_complete_visual", boolValue: true));
                default:
                    return Sequence(name,
                        Step(ActionStepType.Wait, duration: 0.3f),
                        Step(ActionStepType.SetActive, "level_complete_visual", boolValue: true));
            }
        }

        private static PuzzleDefinition CreatePuzzle(
            LevelSpec spec,
            ActionSequenceAsset intro,
            ActionSequenceAsset success)
        {
            var puzzle = Asset<PuzzleDefinition>(
                $"{DataRoot}/Puzzles/C01/PZ_C01_{spec.Number:000}_EraseReveal.asset");
            Set(puzzle, "puzzleId", $"PZ_C01_{spec.Number:000}_EraseReveal");
            Set(puzzle, "puzzleType", (int)PuzzleType.EraseReveal);
            SetStrings(puzzle, "correctEntityIds", spec.TargetId);
            Set(puzzle, "requiredProgress", 0.72f);
            Set(puzzle, "introSequence", intro);
            Set(puzzle, "successSequence", success);
            Set(puzzle, "resetAfterFailure", true);
            return puzzle;
        }

        private static LevelDefinition CreateLevel(LevelSpec spec, PuzzleDefinition puzzle)
        {
            var level = Asset<LevelDefinition>($"{DataRoot}/Levels/C01/LVL_C01_{spec.Number:000}.asset");
            Set(level, "levelId", $"LVL_C01_{spec.Number:000}");
            Set(level, "levelNumber", spec.Number);
            Set(level, "sceneName", SceneName(spec.Number));
            SetObjects(level, "puzzles", new List<PuzzleDefinition> { puzzle });
            return level;
        }

        private static LevelArtSet CreateArtSet(LevelSpec spec)
        {
            var artSet = Asset<LevelArtSet>($"{DataRoot}/Presentation/C01/ART_C01_{spec.Number:000}.asset");
            Set(artSet, "levelId", $"LVL_C01_{spec.Number:000}");
            var crawl = AssetDatabase.LoadAssetAtPath<Sprite>(GaneshaCrawlPath);
            if (crawl != null) EnsureBinding(artSet, "CHR_Ganesha_Crawl", crawl);
            if (spec.Number == 1)
            {
                BindSprite(artSet, "BG_Far", LevelOneBackgroundPath);
                foreach (var slot in new[] { "INT_C01_001_Target", "PROP_Jar_Hanging", "PROP_Jar_Broken", "PROP_Modaks_Pile" })
                    BindSprite(artSet, slot, $"{LevelOnePropsRoot}/{slot}.png");
            }
            return artSet;
        }

        private static void BindSprite(LevelArtSet artSet, string slot, string path)
        {
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (sprite != null) EnsureBinding(artSet, slot, sprite);
            else Debug.LogWarning($"Missing Level 1 sprite: {path}");
        }

        private static void EnsureLevelOneSpriteImports()
        {
            EnsureSpriteImport(LevelOneBackgroundPath);
            foreach (var slot in new[] { "INT_C01_001_Target", "PROP_Jar_Hanging", "PROP_Jar_Broken", "PROP_Modaks_Pile" })
                EnsureSpriteImport($"{LevelOnePropsRoot}/{slot}.png");
        }

        private static void EnsureSpriteImport(string path)
        {
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null) return;
            var isBackground = path == LevelOneBackgroundPath;
            if (importer.textureType == TextureImporterType.Sprite &&
                !importer.mipmapEnabled && importer.alphaIsTransparency &&
                (!isBackground || importer.maxTextureSize >= 4096)) return;
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = 100f;
            importer.mipmapEnabled = false;
            importer.alphaIsTransparency = true;
            if (isBackground) importer.maxTextureSize = 4096;
            importer.SaveAndReimport();
        }

        private static void EnsureBinding(LevelArtSet artSet, string slotId, Sprite sprite)
        {
            var serialized = new SerializedObject(artSet);
            var bindings = serialized.FindProperty("bindings");
            for (var i = 0; i < bindings.arraySize; i++)
                if (bindings.GetArrayElementAtIndex(i).FindPropertyRelative("slotId").stringValue == slotId)
                    return;
            var binding = bindings.GetArrayElementAtIndex(bindings.arraySize++);
            binding.FindPropertyRelative("slotId").stringValue = slotId;
            binding.FindPropertyRelative("sprite").objectReferenceValue = sprite;
            binding.FindPropertyRelative("tint").colorValue = Color.white;
            binding.FindPropertyRelative("useNativeSize").boolValue = false;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(artSet);
        }

        private static string CreateScene(LevelSpec spec, LevelDefinition level, LevelArtSet artSet)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var camera = CreateCamera();

            var systems = new GameObject("Gameplay Systems");
            var registry = systems.AddComponent<SceneRegistry>();
            var router = systems.AddComponent<PuzzleInputRouter>();
            var runner = systems.AddComponent<ActionSequenceRunner>();
            var director = systems.AddComponent<PuzzleDirector>();
            Set(runner, "sceneRegistry", registry);
            Set(director, "level", level);
            Set(director, "inputRouter", router);
            Set(director, "sequenceRunner", runner);

            var content = new GameObject("Level Content");
            var binder = content.AddComponent<LevelArtBinder>();
            Set(binder, "artSet", artSet);
            BuildEnvironment(spec, content.transform);
            BuildLayout(spec, content.transform);
            CreateEraseSurface(spec, content.transform, router, director, camera);
            CreateHud(spec, director);

            new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            var path = ScenePath(spec.Number);
            EditorSceneManager.SaveScene(scene, path);
            return path;
        }

        private static Camera CreateCamera()
        {
            var gameObject = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener), typeof(Physics2DRaycaster));
            gameObject.tag = "MainCamera";
            gameObject.transform.position = new Vector3(0f, 0f, -10f);
            var camera = gameObject.GetComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = PortraitWorldHeight * 0.5f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.08f, 0.05f, 0.14f);
            return camera;
        }

        private static void BuildEnvironment(LevelSpec spec, Transform parent)
        {
            Art("Background", "BG_Far", parent, Vector3.zero, new Vector2(PortraitWorldWidth, PortraitWorldHeight),
                new Color(0.18f, 0.09f, 0.24f), -20);
            if (spec.Number == 1) return; // Its full-bleed plate has clean repaint below every removable object.
            var wallSlot = spec.Number <= 2 ? "BG_Mid_Palace" : "BG_Mid_Storeroom";
            Art("Palace Wall", wallSlot, parent, new Vector3(0f, 0.25f), new Vector2(5.4f, 8.6f),
                new Color(0.91f, 0.63f, 0.33f), -15);
            Art("Floor", "BG_Floor", parent, new Vector3(0f, -3.75f), new Vector2(5.7f, 2.0f),
                new Color(0.48f, 0.19f, 0.14f), -10);
            Art("Arch", "ENV_Palace_Arch", parent, new Vector3(0f, 2.55f), new Vector2(4.7f, 0.45f),
                new Color(0.49f, 0.16f, 0.18f), -8);
            Label(spec.Title.ToUpperInvariant(), parent, new Vector3(0f, 3.75f), 0.075f, 8, new Color(0.35f, 0.12f, 0.13f));
        }

        private static void BuildLayout(LevelSpec spec, Transform parent)
        {
            switch (spec.Number)
            {
                case 1: LayoutOne(parent); break;
                case 2: LayoutTwo(parent); break;
                case 3: LayoutThree(parent); break;
                case 4: LayoutFour(parent); break;
                case 5: LayoutFive(parent); break;
            }
        }

        private static void LayoutOne(Transform parent)
        {
            Actor(parent, new Vector3(-1.65f, -2.55f), new Vector2(1.35f, 1.85f));
            var jar = Entity("Hanging Modak Jar", "jar_hanging", parent);
            Art("Jar Art", "PROP_Jar_Hanging", jar.transform, Vector3.zero, new Vector2(1.45f, 1.55f),
                new Color(0.71f, 0.23f, 0.12f), 1);
            jar.transform.position = new Vector3(1.55f, 0.05f);
            var reward = Entity("Broken Jar Reward", "level_complete_visual", parent);
            Art("Broken Jar", "PROP_Jar_Broken", reward.transform, new Vector3(1.55f, -2.15f), new Vector2(2.0f, 0.9f),
                new Color(0.82f, 0.35f, 0.13f), 2);
            Art("Modaks", "PROP_Modaks_Pile", reward.transform, new Vector3(1.55f, -1.75f), new Vector2(1.25f, 0.55f),
                new Color(1f, 0.72f, 0.12f), 3);
        }

        private static void LayoutTwo(Transform parent)
        {
            Actor(parent, new Vector3(-2.0f, -2.3f), new Vector2(1.35f, 1.85f));
            Art("Large Serving Jar", "PROP_Jar_Large", parent, new Vector3(1.75f, -1.55f), new Vector2(1.55f, 2.1f),
                new Color(0.69f, 0.22f, 0.12f), 1);
            Art("Decorative Shards", "PROP_Clay_Shards", parent, new Vector3(-0.6f, -2.5f), new Vector2(1.1f, 0.35f),
                new Color(0.62f, 0.24f, 0.15f), 2);
            var reward = Entity("Level Complete Visual", "level_complete_visual", parent);
            Art("Reward Sparkle", "VFX_Reward_Glow", reward.transform, new Vector3(1.75f, -1.1f), new Vector2(2.0f, 2.0f),
                new Color(1f, 0.8f, 0.2f, 0.35f), 0);
        }

        private static void LayoutThree(Transform parent)
        {
            Actor(parent, new Vector3(-1.85f, -2.55f), new Vector2(1.35f, 1.85f));
            Art("Cupboard Frame", "ENV_Cupboard_Frame", parent, new Vector3(0.85f, 0.7f), new Vector2(2.75f, 3.35f),
                new Color(0.35f, 0.12f, 0.08f), 0);
            for (var i = 0; i < 3; i++)
                Art($"Modak Bowl {i + 1}", $"PROP_Modak_Bowl_{i + 1}", parent,
                    new Vector3(0.15f + i * 0.7f, 0.4f), new Vector2(0.58f, 0.48f),
                    new Color(1f, 0.7f, 0.12f), 1);
            var reward = Entity("Level Complete Visual", "level_complete_visual", parent);
            Art("Reward Glow", "VFX_Reward_Glow", reward.transform, new Vector3(0.85f, 0.5f), new Vector2(2.5f, 2.5f),
                new Color(1f, 0.84f, 0.2f, 0.3f), 0);
        }

        private static void LayoutFour(Transform parent)
        {
            Actor(parent, new Vector3(-2.0f, -2.55f), new Vector2(1.35f, 1.85f));
            Art("Fulcrum", "PROP_Balance_Fulcrum", parent, new Vector3(0f, -0.25f), new Vector2(0.55f, 2.2f),
                new Color(0.34f, 0.15f, 0.1f), 0);
            var beam = Entity("Balance Beam", "balance_beam", parent);
            beam.transform.position = new Vector3(0f, 0.55f);
            Art("Beam", "PROP_Balance_Beam", beam.transform, Vector3.zero, new Vector2(4.25f, 0.28f),
                new Color(0.45f, 0.2f, 0.1f), 1);
            Art("Heavy Covered Jar", "PROP_CoveredJar_Heavy", beam.transform, new Vector3(1.55f, 0.6f), new Vector2(1.35f, 1.55f),
                new Color(0.26f, 0.55f, 0.7f), 2);
            var reward = Entity("Level Complete Visual", "level_complete_visual", parent);
            Art("Reward Modak Jar", "PROP_Jar_Reward", reward.transform, new Vector3(1.55f, 1.05f), new Vector2(1.05f, 1.2f),
                new Color(0.83f, 0.29f, 0.12f), 3);
        }

        private static void LayoutFive(Transform parent)
        {
            Actor(parent, new Vector3(-2.0f, -2.65f), new Vector2(1.35f, 1.85f));
            Art("Upper Left Covered Pot", "PROP_CoveredJar_Decoy_A", parent, new Vector3(-1.3f, 1.3f), new Vector2(1.55f, 1.65f),
                new Color(0.48f, 0.55f, 0.82f), 1);
            Art("Upper Right Covered Pot", "PROP_CoveredJar_Decoy_B", parent, new Vector3(1.3f, 1.3f), new Vector2(1.55f, 1.65f),
                new Color(0.66f, 0.39f, 0.75f), 1);
            Art("Hidden Lower Jar", "PROP_Jar_Reward", parent, new Vector3(0f, -0.65f), new Vector2(1.25f, 1.35f),
                new Color(0.82f, 0.28f, 0.12f), 1);
            var reward = Entity("Level Complete Visual", "level_complete_visual", parent);
            Art("Modak Sparkle", "VFX_Reward_Glow", reward.transform, new Vector3(0f, -0.45f), new Vector2(2.3f, 2.3f),
                new Color(1f, 0.8f, 0.2f, 0.3f), 0);
        }

        private static void Actor(Transform parent, Vector3 position, Vector2 size)
        {
            var actor = Entity("Bal Ganesha", "ganesha_actor", parent);
            actor.transform.position = position;
            Art("Ganesha Art", "CHR_Ganesha_Crawl", actor.transform, Vector3.zero, size,
                new Color(0.93f, 0.48f, 0.45f), 4);
        }

        private static void CreateEraseSurface(
            LevelSpec spec,
            Transform parent,
            PuzzleInputRouter router,
            PuzzleDirector director,
            Camera camera)
        {
            var surface = Entity("Erasable Target", spec.TargetId, parent);
            var cover = Art("Interactive Cover", $"INT_C01_{spec.Number:000}_Target", surface.transform,
                spec.SurfacePosition, spec.SurfaceSize, new Color(0.25f, 0.62f, 0.82f), 6);
            cover.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            var controller = surface.AddComponent<TileEraseController>();
            Set(controller, "inputRouter", router);
            Set(controller, "puzzleDirector", director);
            Set(controller, "worldCamera", camera);
            Set(controller, "brushRadius", 0.42f);
            Set(controller, "useDirectPointerInput", true);

            var cellWidth = spec.SurfaceSize.x / spec.Columns;
            var cellHeight = spec.SurfaceSize.y / spec.Rows;
            var startX = spec.SurfacePosition.x - spec.SurfaceSize.x * 0.5f + cellWidth * 0.5f;
            var startY = spec.SurfacePosition.y - spec.SurfaceSize.y * 0.5f + cellHeight * 0.5f;
            for (var row = 0; row < spec.Rows; row++)
            for (var column = 0; column < spec.Columns; column++)
            {
                var tile = new GameObject($"Erase Cell {row:00}_{column:00}", typeof(SpriteMask), typeof(BoxCollider2D), typeof(ErasableTile));
                tile.transform.SetParent(surface.transform, false);
                tile.transform.position = new Vector3(startX + column * cellWidth, startY + row * cellHeight, 0f);
                tile.transform.localScale = new Vector3(cellWidth * 1.03f, cellHeight * 1.03f, 1f);
                var mask = tile.GetComponent<SpriteMask>();
                mask.isCustomRangeActive = true;
                mask.frontSortingOrder = 7;
                mask.backSortingOrder = 5;
                tile.GetComponent<BoxCollider2D>().size = Vector2.one;
            }
        }

        private static void CreateHud(LevelSpec spec, PuzzleDirector director)
        {
            var canvasObject = new GameObject("Gameplay UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1170f, 2532f);
            scaler.matchWidthOrHeight = 0.5f;

            var level = UiText("Level", canvasObject.transform, new Vector2(0.06f, 0.91f), new Vector2(0.3f, 0.97f), 38);
            level.alignment = TextAnchor.MiddleLeft;
            var objective = UiText("Objective", canvasObject.transform, new Vector2(0.12f, 0.80f), new Vector2(0.88f, 0.91f), 34);
            var feedback = UiText("Feedback", canvasObject.transform, new Vector2(0.12f, 0.08f), new Vector2(0.88f, 0.16f), 44);
            feedback.color = new Color(1f, 0.82f, 0.2f);

            var panel = UiPanel("Completion Panel", canvasObject.transform, new Vector2(0.1f, 0.2f), new Vector2(0.9f, 0.46f));
            var complete = UiText("Complete", panel.transform, new Vector2(0.08f, 0.56f), new Vector2(0.92f, 0.9f), 48);
            complete.text = "MODAK FOUND!";
            var next = UiButton("Next Button", panel.transform, new Vector2(0.52f, 0.12f), new Vector2(0.92f, 0.48f), "NEXT LEVEL");
            var retry = UiButton("Retry Button", panel.transform, new Vector2(0.08f, 0.12f), new Vector2(0.48f, 0.48f), "REPLAY");

            var presenter = canvasObject.AddComponent<FirstFiveLevelPresenter>();
            Set(presenter, "puzzleDirector", director);
            Set(presenter, "levelText", level);
            Set(presenter, "objectiveText", objective);
            Set(presenter, "feedbackText", feedback);
            Set(presenter, "completionPanel", panel);
            Set(presenter, "nextButton", next);
            Set(presenter, "retryButton", retry);
            Set(presenter, "objective", spec.Objective);
            Set(presenter, "nextSceneName", spec.NextScene);
        }

        private static GameObject Art(string name, string slotId, Transform parent, Vector3 position, Vector2 size,
            Color color, int order)
        {
            var gameObject = new GameObject(name, typeof(SpriteRenderer), typeof(SpriteArtSlot));
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.localPosition = position;
            gameObject.transform.localScale = new Vector3(size.x, size.y, 1f);
            var slot = gameObject.GetComponent<SpriteArtSlot>();
            Set(slot, "slotId", slotId);
            Set(slot, "targetRenderer", gameObject.GetComponent<SpriteRenderer>());
            Set(slot, "fallbackColor", color);
            Set(slot, "sortingOrder", order);
            Set(slot, "referenceSize", size);
            return gameObject;
        }

        private static GameObject Entity(string name, string id, Transform parent)
        {
            var gameObject = new GameObject(name);
            gameObject.transform.SetParent(parent, false);
            var entity = gameObject.AddComponent<SceneEntity>();
            Set(entity, "entityId", id);
            return gameObject;
        }

        private static void Label(string value, Transform parent, Vector3 position, float size, int order, Color color)
        {
            var gameObject = new GameObject($"Label {value}", typeof(TextMesh));
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.position = position + new Vector3(0f, 0f, -0.1f);
            var text = gameObject.GetComponent<TextMesh>();
            text.text = value;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.characterSize = size;
            text.fontSize = 48;
            text.color = color;
            text.GetComponent<MeshRenderer>().sortingOrder = order;
        }

        private static Text UiText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, int fontSize)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            gameObject.transform.SetParent(parent, false);
            var rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            var text = gameObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;
            return text;
        }

        private static GameObject UiPanel(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            gameObject.transform.SetParent(parent, false);
            var rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            gameObject.GetComponent<Image>().color = new Color(0.12f, 0.05f, 0.18f, 0.94f);
            return gameObject;
        }

        private static Button UiButton(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, string label)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            gameObject.transform.SetParent(parent, false);
            var rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            gameObject.GetComponent<Image>().color = new Color(0.86f, 0.45f, 0.13f);
            var text = UiText("Label", gameObject.transform, Vector2.zero, Vector2.one, 30);
            text.text = label;
            return gameObject.GetComponent<Button>();
        }

        private static ActionSequenceAsset Sequence(string name, params ActionStep[] steps)
        {
            var asset = Asset<ActionSequenceAsset>($"{DataRoot}/Sequences/C01/{name}.asset");
            var serialized = new SerializedObject(asset);
            var property = serialized.FindProperty("steps");
            property.arraySize = steps.Length;
            for (var index = 0; index < steps.Length; index++)
            {
                var item = property.GetArrayElementAtIndex(index);
                var step = steps[index];
                item.FindPropertyRelative("type").enumValueIndex = (int)step.type;
                item.FindPropertyRelative("entityId").stringValue = step.entityId;
                item.FindPropertyRelative("stringValue").stringValue = step.stringValue;
                item.FindPropertyRelative("vectorValue").vector3Value = step.vectorValue;
                item.FindPropertyRelative("duration").floatValue = step.duration;
                item.FindPropertyRelative("boolValue").boolValue = step.boolValue;
            }
            serialized.ApplyModifiedPropertiesWithoutUndo();
            return asset;
        }

        private static ActionStep Step(ActionStepType type, string entityId = "", Vector3 vector = default,
            float duration = 0f, bool boolValue = false) => new()
        {
            type = type,
            entityId = entityId,
            vectorValue = vector,
            duration = duration,
            boolValue = boolValue
        };

        private static void EnsureFolders()
        {
            Directory.CreateDirectory($"{DataRoot}/Levels/C01");
            Directory.CreateDirectory($"{DataRoot}/Puzzles/C01");
            Directory.CreateDirectory($"{DataRoot}/Presentation/C01");
            Directory.CreateDirectory($"{DataRoot}/Sequences/C01");
            Directory.CreateDirectory(SceneRoot);
            AssetDatabase.Refresh();
        }

        private static T Asset<T>(string path) where T : ScriptableObject
        {
            var existing = AssetDatabase.LoadAssetAtPath<T>(path);
            if (existing != null) return existing;
            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            return asset;
        }

        private static string SceneName(int number) => $"Level_{number:000}_BalGanesha";
        private static string ScenePath(int number) => $"{SceneRoot}/{SceneName(number)}.unity";

        private static void MergeBuildSettings(IEnumerable<EditorBuildSettingsScene> generated)
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            foreach (var item in generated)
            {
                var index = scenes.FindIndex(scene => scene.path == item.path);
                if (index >= 0) scenes[index] = item;
                else scenes.Add(item);
            }
            EditorBuildSettings.scenes = scenes.ToArray();
        }

        private static void Set(Object target, string property, Object value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, string value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).stringValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, int value)
        {
            var serialized = new SerializedObject(target);
            var item = serialized.FindProperty(property);
            if (item.propertyType == SerializedPropertyType.Enum) item.enumValueIndex = value;
            else item.intValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, float value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).floatValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, bool value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).boolValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, Color value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).colorValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void Set(Object target, string property, Vector2 value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(property).vector2Value = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void SetStrings(Object target, string property, params string[] values)
        {
            var serialized = new SerializedObject(target);
            var array = serialized.FindProperty(property);
            array.arraySize = values.Length;
            for (var index = 0; index < values.Length; index++)
                array.GetArrayElementAtIndex(index).stringValue = values[index];
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }

        private static void SetObjects<T>(Object target, string property, IReadOnlyList<T> values) where T : Object
        {
            var serialized = new SerializedObject(target);
            var array = serialized.FindProperty(property);
            array.arraySize = values.Count;
            for (var index = 0; index < values.Count; index++)
                array.GetArrayElementAtIndex(index).objectReferenceValue = values[index];
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(target);
        }
    }
}
