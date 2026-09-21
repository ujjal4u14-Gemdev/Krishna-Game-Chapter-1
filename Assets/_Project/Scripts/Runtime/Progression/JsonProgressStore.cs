using System.IO;
using UnityEngine;

namespace MythicPuzzle.Runtime
{
    public sealed class JsonProgressStore : MonoBehaviour, IProgressStore
    {
        [SerializeField] private string fileName = "player-progress.json";
        private string FilePath => Path.Combine(Application.persistentDataPath, fileName);

        public PlayerProgressData Load()
        {
            if (!File.Exists(FilePath)) return new PlayerProgressData();

            try
            {
                return JsonUtility.FromJson<PlayerProgressData>(File.ReadAllText(FilePath))
                    ?? new PlayerProgressData();
            }
            catch (System.Exception exception) when (
                exception is IOException ||
                exception is System.UnauthorizedAccessException ||
                exception is System.ArgumentException)
            {
                Debug.LogWarning($"Could not load progress: {exception.Message}");
                return new PlayerProgressData();
            }
        }

        public void Save(PlayerProgressData data)
        {
            try
            {
                var temporaryPath = FilePath + ".tmp";
                File.WriteAllText(temporaryPath, JsonUtility.ToJson(data, true));

                if (File.Exists(FilePath)) File.Delete(FilePath);
                File.Move(temporaryPath, FilePath);
            }
            catch (System.Exception exception) when (
                exception is IOException ||
                exception is System.UnauthorizedAccessException ||
                exception is System.ArgumentException)
            {
                Debug.LogWarning($"Could not save progress: {exception.Message}");
            }
        }
    }
}
