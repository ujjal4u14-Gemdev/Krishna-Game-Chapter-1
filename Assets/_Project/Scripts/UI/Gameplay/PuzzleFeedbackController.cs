using System.Collections;
using UnityEngine;

namespace MythicPuzzle.UI
{
    public sealed class PuzzleFeedbackController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup correctGroup;
        [SerializeField] private CanvasGroup wrongGroup;
        [SerializeField, Min(0f)] private float visibleSeconds = 0.6f;

        public void ShowCorrect() => StartCoroutine(Show(correctGroup));
        public void ShowWrong() => StartCoroutine(Show(wrongGroup));

        private IEnumerator Show(CanvasGroup group)
        {
            group.alpha = 1f;
            group.blocksRaycasts = true;
            yield return new WaitForSecondsRealtime(visibleSeconds);
            group.alpha = 0f;
            group.blocksRaycasts = false;
        }
    }
}
