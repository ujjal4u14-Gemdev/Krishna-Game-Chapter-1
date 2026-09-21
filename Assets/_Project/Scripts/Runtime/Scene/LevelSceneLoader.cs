using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicPuzzle.Runtime
{
    public sealed class LevelSceneLoader : MonoBehaviour
    {
        public void Load(LevelDefinition level)
        {
            if (level == null || string.IsNullOrWhiteSpace(level.SceneName))
            {
                Debug.LogError("Cannot load a level without a configured scene name.");
                return;
            }

            Load(level.SceneName);
        }

        public void Load(string sceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadCurrent()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
