using System.Collections.Generic;
using MythicPuzzle.Runtime;
using UnityEditor;
using UnityEngine;

namespace MythicPuzzle.Editor
{
    public static class ProjectValidator
    {
        [MenuItem("Tools/Krishna Game/Validate Project")]
        public static void ValidateProject()
        {
            var errors = 0;
            var warnings = 0;
            var knownLevelIds = new HashSet<string>();
            var knownPuzzleIds = new HashSet<string>();

            foreach (var level in LoadAssets<LevelDefinition>())
            {
                if (string.IsNullOrWhiteSpace(level.LevelId)) Error(level, "Level ID is empty", ref errors);
                else if (!knownLevelIds.Add(level.LevelId)) Error(level, $"Duplicate level ID: {level.LevelId}", ref errors);
                if (level.LevelNumber < 1) Error(level, "Level number must be at least 1", ref errors);
                if (string.IsNullOrWhiteSpace(level.SceneName)) Warn(level, "Scene name is empty", ref warnings);
                if (level.Puzzles.Count == 0) Warn(level, "Level has no puzzles", ref warnings);
            }

            foreach (var puzzle in LoadAssets<PuzzleDefinition>())
            {
                if (string.IsNullOrWhiteSpace(puzzle.PuzzleId)) Error(puzzle, "Puzzle ID is empty", ref errors);
                else if (!knownPuzzleIds.Add(puzzle.PuzzleId)) Error(puzzle, $"Duplicate puzzle ID: {puzzle.PuzzleId}", ref errors);

                var needsTarget = puzzle.PuzzleType == PuzzleType.BinaryChoice ||
                                  puzzle.PuzzleType == PuzzleType.AimAndShoot ||
                                  puzzle.PuzzleType == PuzzleType.TapSelect;
                if (needsTarget && puzzle.CorrectEntityIds.Count == 0)
                    Error(puzzle, "Puzzle requires at least one correct entity ID", ref errors);
            }

            var sceneIds = new HashSet<string>();
            foreach (var entity in Object.FindObjectsByType<SceneEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (string.IsNullOrWhiteSpace(entity.EntityId)) Warn(entity, "Scene entity ID is empty", ref warnings);
                else if (!sceneIds.Add(entity.EntityId)) Error(entity, $"Duplicate scene entity ID: {entity.EntityId}", ref errors);
            }

            var message = $"Krishna Game validation finished: {errors} error(s), {warnings} warning(s).";
            if (errors == 0) Debug.Log(message); else Debug.LogError(message);
        }

        private static IEnumerable<T> LoadAssets<T>() where T : Object
        {
            foreach (var guid in AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { "Assets/_Project" }))
            {
                var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
                if (asset != null) yield return asset;
            }
        }

        private static void Error(Object context, string message, ref int count)
        {
            count++;
            Debug.LogError(message, context);
        }

        private static void Warn(Object context, string message, ref int count)
        {
            count++;
            Debug.LogWarning(message, context);
        }
    }
}
