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
    public static class PlayableDemoBuilder
    {
        private const string DemoRoot = "Assets/_Project/Content/Samples/GreyboxDemo";
        private const string ScenePath = "Assets/_Project/Scenes/Samples/PlayableGreyboxDemo.unity";

        [MenuItem("Tools/Bal Ganesha Game/Create Playable Greybox Demo")]
        public static void CreateDemo()
        {
            Directory.CreateDirectory(DemoRoot);
            Directory.CreateDirectory("Assets/_Project/Scenes/Samples");
            Directory.CreateDirectory("Assets/_Project/Prefabs/Interactions");
            AssetDatabase.Refresh();

            var sequences = CreateSequences();
            var puzzles = CreatePuzzles(sequences);
            var level = CreateLevel(puzzles, sequences["complete"]);
            var projectilePrefab = CreateProjectilePrefab();
            CreateScene(level, projectilePrefab);
            AddSceneToBuildSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(ScenePath);
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
            Debug.Log("Playable greybox demo created. Press Play and complete all three puzzles.");
        }

        private static Dictionary<string, ActionSequenceAsset> CreateSequences()
        {
            return new Dictionary<string, ActionSequenceAsset>
            {
                ["eraseIntro"] = Sequence("SEQ_Demo_EraseIntro",
                    Step(ActionStepType.SetActive, "erase_group", boolValue: true),
                    Step(ActionStepType.SetActive, "choice_group", boolValue: false),
                    Step(ActionStepType.SetActive, "aim_group", boolValue: false),
                    Step(ActionStepType.SetActive, "complete_group", boolValue: false)),
                ["eraseSuccess"] = Sequence("SEQ_Demo_EraseSuccess",
                    Step(ActionStepType.Wait, duration: 0.65f),
                    Step(ActionStepType.SetActive, "erase_group", boolValue: false)),
                ["choiceIntro"] = Sequence("SEQ_Demo_ChoiceIntro",
                    Step(ActionStepType.SetActive, "choice_group", boolValue: true)),
                ["choiceSuccess"] = Sequence("SEQ_Demo_ChoiceSuccess",
                    Step(ActionStepType.Wait, duration: 0.65f),
                    Step(ActionStepType.SetActive, "choice_group", boolValue: false)),
                ["aimIntro"] = Sequence("SEQ_Demo_AimIntro",
                    Step(ActionStepType.SetActive, "aim_group", boolValue: true)),
                ["aimSuccess"] = Sequence("SEQ_Demo_AimSuccess",
                    Step(ActionStepType.Wait, duration: 0.65f),
                    Step(ActionStepType.SetActive, "aim_group", boolValue: false)),
                ["failure"] = Sequence("SEQ_Demo_Failure", Step(ActionStepType.Wait, duration: 0.65f)),
                ["complete"] = Sequence("SEQ_Demo_Complete",
                    Step(ActionStepType.SetActive, "complete_group", boolValue: true))
            };
        }

        private static List<PuzzleDefinition> CreatePuzzles(Dictionary<string, ActionSequenceAsset> sequences)
        {
            var erase = Asset<PuzzleDefinition>("PZ_Demo_01_Erase");
            Set(erase, "puzzleId", "demo_erase");
            Set(erase, "puzzleType", (int)PuzzleType.EraseReveal);
            SetStrings(erase, "correctEntityIds", "rope_mask");
            Set(erase, "requiredProgress", 0.7f);
            Set(erase, "introSequence", sequences["eraseIntro"]);
            Set(erase, "successSequence", sequences["eraseSuccess"]);
            Set(erase, "failureSequence", sequences["failure"]);

            var choice = Asset<PuzzleDefinition>("PZ_Demo_02_Choice");
            Set(choice, "puzzleId", "demo_choice");
            Set(choice, "puzzleType", (int)PuzzleType.BinaryChoice);
            SetStrings(choice, "correctEntityIds", "safe_route");
            Set(choice, "introSequence", sequences["choiceIntro"]);
            Set(choice, "successSequence", sequences["choiceSuccess"]);
            Set(choice, "failureSequence", sequences["failure"]);

            var aim = Asset<PuzzleDefinition>("PZ_Demo_03_Aim");
            Set(aim, "puzzleId", "demo_aim");
            Set(aim, "puzzleType", (int)PuzzleType.AimAndShoot);
            SetStrings(aim, "correctEntityIds", "modak_jar");
            Set(aim, "introSequence", sequences["aimIntro"]);
            Set(aim, "successSequence", sequences["aimSuccess"]);
            Set(aim, "failureSequence", sequences["failure"]);
            return new List<PuzzleDefinition> { erase, choice, aim };
        }

        private static LevelDefinition CreateLevel(List<PuzzleDefinition> puzzles, ActionSequenceAsset complete)
        {
            var level = Asset<LevelDefinition>("LVL_Demo_Greybox");
            Set(level, "levelId", "greybox_demo");
            Set(level, "levelNumber", 1);
            Set(level, "sceneName", "PlayableGreyboxDemo");
            SetObjects(level, "puzzles", puzzles);
            Set(level, "completionSequence", complete);
            return level;
        }

        private static PuzzleProjectile CreateProjectilePrefab()
        {
            var temporary = new GameObject("DemoProjectile");
            temporary.transform.localScale = Vector3.one * 0.28f;
            temporary.AddComponent<SpriteRenderer>();
            var solid = temporary.AddComponent<DemoSolidSprite>();
            Set(solid, "color", new Color(1f, 0.75f, 0.12f));
            Set(solid, "sortingOrder", 5);
            var collider = temporary.AddComponent<CircleCollider2D>();
            collider.radius = 0.5f;
            var body = temporary.AddComponent<Rigidbody2D>();
            body.gravityScale = 1f;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            temporary.AddComponent<PuzzleProjectile>();

            var path = "Assets/_Project/Prefabs/Interactions/DemoProjectile.prefab";
            var prefab = PrefabUtility.SaveAsPrefabAsset(temporary, path).GetComponent<PuzzleProjectile>();
            Object.DestroyImmediate(temporary);
            return prefab;
        }

        private static void CreateScene(LevelDefinition level, PuzzleProjectile projectilePrefab)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var cameraObject = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener), typeof(Physics2DRaycaster));
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);
            var camera = cameraObject.GetComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 5.4f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.06f, 0.08f, 0.13f);

            var systems = new GameObject("Gameplay Systems");
            var registry = systems.AddComponent<SceneRegistry>();
            var router = systems.AddComponent<PuzzleInputRouter>();
            var runner = systems.AddComponent<ActionSequenceRunner>();
            var director = systems.AddComponent<PuzzleDirector>();
            Set(runner, "sceneRegistry", registry);
            Set(director, "level", level);
            Set(director, "inputRouter", router);
            Set(director, "sequenceRunner", runner);

            var content = new GameObject("Scene Content");
            content.transform.SetParent(systems.transform, false);
            CreateErasePuzzle(content.transform, router, director, camera);
            CreateChoicePuzzle(content.transform, router);
            CreateAimPuzzle(content.transform, router, director, camera, projectilePrefab);
            CreateCompleteGroup(content.transform);
            CreateUi(director);

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.transform.SetAsLastSibling();
            EditorSceneManager.SaveScene(scene, ScenePath);
        }

        private static void CreateErasePuzzle(Transform parent, PuzzleInputRouter router, PuzzleDirector director, Camera camera)
        {
            var group = Entity("Erase Puzzle", "erase_group", parent);
            var hidden = Solid("Hidden Modaks", group.transform, new Vector3(0f, 0f, 0f), new Vector2(3.4f, 2.3f), new Color(1f, 0.68f, 0.08f), 0);
            Label("MODAKS REVEALED", hidden.transform, Vector3.zero, 0.12f, 1);

            var mask = Entity("Erasable Mask", "rope_mask", group.transform);
            mask.AddComponent<BoxCollider2D>().size = new Vector2(5.2f, 4.2f);
            var controller = mask.AddComponent<TileEraseController>();
            Set(controller, "inputRouter", router);
            Set(controller, "puzzleDirector", director);
            Set(controller, "worldCamera", camera);
            Set(controller, "brushRadius", 0.48f);
            Set(controller, "useDirectPointerInput", true);

            for (var row = 0; row < 5; row++)
            for (var column = 0; column < 7; column++)
            {
                var position = new Vector3(-2.1f + column * 0.7f, -1.4f + row * 0.7f, 0f);
                var tile = Solid($"Erase Tile {row}_{column}", mask.transform, position, Vector2.one * 0.66f,
                    new Color(0.12f, 0.48f, 0.9f), 2);
                var collider = tile.AddComponent<BoxCollider2D>();
                var erasable = tile.AddComponent<ErasableTile>();
                Set(erasable, "tileRenderer", tile.GetComponent<SpriteRenderer>());
                Set(erasable, "tileCollider", collider);
            }
        }

        private static void CreateChoicePuzzle(Transform parent, PuzzleInputRouter router)
        {
            var group = Entity("Choice Puzzle", "choice_group", parent);
            var safe = Solid("Safe Route", group.transform, new Vector3(-1.35f, 0f, 0f), new Vector2(2.1f, 3.2f),
                new Color(0.15f, 0.72f, 0.32f), 1);
            Target(safe, "safe_route", router);
            Label("SAFE", safe.transform, Vector3.zero, 0.16f);

            var danger = Solid("Danger Route", group.transform, new Vector3(1.35f, 0f, 0f), new Vector2(2.1f, 3.2f),
                new Color(0.82f, 0.2f, 0.18f), 1);
            Target(danger, "danger_route", router);
            Label("DANGER", danger.transform, Vector3.zero, 0.16f);
        }

        private static void CreateAimPuzzle(Transform parent, PuzzleInputRouter router, PuzzleDirector director,
            Camera camera, PuzzleProjectile projectilePrefab)
        {
            var group = Entity("Aim Puzzle", "aim_group", parent);
            var interactionCollider = group.AddComponent<BoxCollider2D>();
            interactionCollider.size = new Vector2(6f, 8f);
            interactionCollider.isTrigger = true;

            var launchPoint = Solid("Launcher", group.transform, new Vector3(0f, -3.25f, 0f), new Vector2(0.65f, 0.4f),
                new Color(0.2f, 0.65f, 1f), 2).transform;
            var trajectoryObject = new GameObject("Trajectory", typeof(LineRenderer));
            trajectoryObject.transform.SetParent(group.transform, false);
            var line = trajectoryObject.GetComponent<LineRenderer>();
            line.startWidth = 0.06f;
            line.endWidth = 0.03f;
            line.startColor = Color.white;
            line.endColor = new Color(1f, 1f, 1f, 0.2f);
            line.sharedMaterial = CreateLineMaterial();

            var controller = group.AddComponent<AimAndShootController>();
            Set(controller, "inputRouter", router);
            Set(controller, "puzzleDirector", director);
            Set(controller, "worldCamera", camera);
            Set(controller, "launchPoint", launchPoint);
            Set(controller, "projectilePrefab", projectilePrefab);
            Set(controller, "trajectoryLine", line);
            Set(controller, "launchPower", 3.5f);
            Set(controller, "useDirectPointerInput", true);

            CreateProjectileTarget(group.transform, "Modak Jar", "modak_jar", new Vector3(1.35f, 1.7f, 0f),
                new Vector2(1.15f, 1.15f), new Color(1f, 0.68f, 0.08f), "JAR");
            CreateProjectileTarget(group.transform, "Hazard", "bell_hazard", new Vector3(-1.2f, 0.6f, 0f),
                new Vector2(1.1f, 1.1f), new Color(0.85f, 0.2f, 0.2f), "X");
        }

        private static void CreateCompleteGroup(Transform parent)
        {
            var group = Entity("Complete Group", "complete_group", parent);
            Solid("Celebration", group.transform, Vector3.zero, new Vector2(5.2f, 3.2f),
                new Color(0.14f, 0.55f, 0.35f), 0);
            Label("DEMO COMPLETE!", group.transform, Vector3.zero, 0.18f);
        }

        private static void CreateUi(PuzzleDirector director)
        {
            var canvasObject = new GameObject("Demo UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);
            scaler.matchWidthOrHeight = 0.5f;

            var instruction = UiText("Instructions", canvasObject.transform, new Vector2(0.06f, 0.82f), new Vector2(0.94f, 0.97f), 42);
            var status = UiText("Status", canvasObject.transform, new Vector2(0.08f, 0.05f), new Vector2(0.92f, 0.17f), 48);
            var presenter = canvasObject.AddComponent<GreyboxDemoPresenter>();
            Set(presenter, "puzzleDirector", director);
            Set(presenter, "instructionText", instruction);
            Set(presenter, "statusText", status);
        }

        private static Text UiText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, int size)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            gameObject.transform.SetParent(parent, false);
            var rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            var text = gameObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = size;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;
            return text;
        }

        private static GameObject Entity(string name, string id, Transform parent)
        {
            var gameObject = new GameObject(name);
            gameObject.transform.SetParent(parent, false);
            var entity = gameObject.AddComponent<SceneEntity>();
            Set(entity, "entityId", id);
            return gameObject;
        }

        private static GameObject Solid(string name, Transform parent, Vector3 position, Vector2 size, Color color, int order)
        {
            var gameObject = new GameObject(name, typeof(SpriteRenderer), typeof(DemoSolidSprite));
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.localPosition = position;
            gameObject.transform.localScale = new Vector3(size.x, size.y, 1f);
            var solid = gameObject.GetComponent<DemoSolidSprite>();
            Set(solid, "color", color);
            Set(solid, "sortingOrder", order);
            return gameObject;
        }

        private static void Label(string value, Transform parent, Vector3 position, float characterSize,
            int sortingOrder = 4)
        {
            var label = new GameObject($"Label {value}", typeof(TextMesh));
            label.transform.SetParent(parent, false);
            label.transform.localPosition = position + new Vector3(0f, 0f, -0.1f);
            label.transform.localScale = new Vector3(1f / parent.localScale.x, 1f / parent.localScale.y, 1f);
            var text = label.GetComponent<TextMesh>();
            text.text = value;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.characterSize = characterSize;
            text.fontSize = 48;
            text.color = Color.white;
            text.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        }

        private static void Target(GameObject gameObject, string id, PuzzleInputRouter router)
        {
            gameObject.AddComponent<BoxCollider2D>();
            var entity = gameObject.AddComponent<SceneEntity>();
            Set(entity, "entityId", id);
            var target = gameObject.AddComponent<PuzzleTapTarget>();
            Set(target, "inputRouter", router);
            Set(target, "useDirectPointerInput", true);
        }

        private static void CreateProjectileTarget(Transform parent, string name, string id, Vector3 position,
            Vector2 size, Color color, string label)
        {
            var gameObject = Solid(name, parent, position, size, color, 2);
            gameObject.AddComponent<BoxCollider2D>();
            var entity = gameObject.AddComponent<SceneEntity>();
            Set(entity, "entityId", id);
            gameObject.AddComponent<ProjectileTarget>();
            Label(label, gameObject.transform, Vector3.zero, 0.16f);
        }

        private static ActionSequenceAsset Sequence(string name, params ActionStep[] steps)
        {
            var sequence = Asset<ActionSequenceAsset>(name);
            var serialized = new SerializedObject(sequence);
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
            return sequence;
        }

        private static Material CreateLineMaterial()
        {
            var path = $"{DemoRoot}/MAT_DemoTrajectory.mat";
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material != null) return material;
            material = new Material(Shader.Find("Sprites/Default"));
            AssetDatabase.CreateAsset(material, path);
            return material;
        }

        private static ActionStep Step(ActionStepType type, string entityId = "", string stringValue = "",
            Vector3 vectorValue = default, float duration = 0f, bool boolValue = false) => new()
        {
            type = type,
            entityId = entityId,
            stringValue = stringValue,
            vectorValue = vectorValue,
            duration = duration,
            boolValue = boolValue
        };

        private static T Asset<T>(string name) where T : ScriptableObject
        {
            var path = $"{DemoRoot}/{name}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<T>(path);
            if (existing != null) return existing;
            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            return asset;
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

        private static void AddSceneToBuildSettings()
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            if (!scenes.Exists(item => item.path == ScenePath))
                scenes.Add(new EditorBuildSettingsScene(ScenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
