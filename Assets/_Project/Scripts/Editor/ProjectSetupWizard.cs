using System.Collections.Generic;
using System.IO;
using MythicPuzzle.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicPuzzle.Editor
{
    public static class ProjectSetupWizard
    {
        private const string Root = "Assets/_Project";
        private static readonly string[] Folders =
        {
            "Art/Characters", "Art/Environments", "Art/UI", "Audio/Music", "Audio/SFX",
            "Content/Levels", "Content/Puzzles", "Content/Sequences", "Content/Samples",
            "Prefabs/Actors", "Prefabs/Interactions", "Prefabs/UI",
            "Scenes/Bootstrap", "Scenes/Templates", "Scenes/Chapter01"
        };

        [MenuItem("Tools/Krishna Game/Setup Project")]
        public static void SetupProject()
        {
            foreach (var folder in Folders) Directory.CreateDirectory(Path.Combine(Root, folder));
            AssetDatabase.Refresh();

            var bootstrapPath = $"{Root}/Scenes/Bootstrap/Bootstrap.unity";
            var templatePath = $"{Root}/Scenes/Templates/GameplayTemplate.unity";
            CreateBootstrapScene(bootstrapPath);
            CreateGameplayTemplate(templatePath);
            AddScenesToBuildSettings(bootstrapPath, templatePath);
            AssetDatabase.SaveAssets();
            Debug.Log("Krishna Game setup complete. Open GameplayTemplate and assign a LevelDefinition to PuzzleDirector.");
        }

        private static void CreateBootstrapScene(string path)
        {
            if (File.Exists(path)) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var services = new GameObject("Services");
            services.AddComponent<JsonProgressStore>();
            services.AddComponent<LevelSceneLoader>();
            EditorSceneManager.SaveScene(scene, path);
        }

        private static void CreateGameplayTemplate(string path)
        {
            if (File.Exists(path)) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var cameraObject = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
            cameraObject.tag = "MainCamera";
            var camera = cameraObject.GetComponent<Camera>();
            camera.orthographic = true;
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);

            var systems = new GameObject("Gameplay Systems");
            var registry = systems.AddComponent<SceneRegistry>();
            var router = systems.AddComponent<PuzzleInputRouter>();
            var runner = systems.AddComponent<ActionSequenceRunner>();
            var director = systems.AddComponent<PuzzleDirector>();
            var store = systems.AddComponent<JsonProgressStore>();
            var progress = systems.AddComponent<ChapterProgressService>();

            SetReference(runner, "sceneRegistry", registry);
            SetReference(director, "inputRouter", router);
            SetReference(director, "sequenceRunner", runner);
            SetReference(progress, "puzzleDirector", director);
            SetReference(progress, "progressStore", store);

            new GameObject("Scene Content").transform.SetParent(systems.transform, false);
            new GameObject("Gameplay UI");
            EditorSceneManager.SaveScene(scene, path);
        }

        private static void SetReference(Object target, string propertyName, Object value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(propertyName).objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void AddScenesToBuildSettings(params string[] paths)
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            foreach (var path in paths)
            {
                if (scenes.Exists(scene => scene.path == path)) continue;
                scenes.Add(new EditorBuildSettingsScene(path, true));
            }
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
