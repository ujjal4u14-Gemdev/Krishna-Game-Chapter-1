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
            catch (IOException exception)
            {
                Debug.LogWarning($"Could not load progress: {exception.Message}");
                return new PlayerProgressData();
            }
        }

        public void Save(PlayerProgressData data)
        {
            var temporaryPath = FilePath + ".tmp";
            File.WriteAllText(temporaryPath, JsonUtility.ToJson(data, true));

            if (File.Exists(FilePath)) File.Delete(FilePath);
            File.Move(temporaryPath, FilePath);
        }
    }
}
