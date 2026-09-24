using System.Collections;
using MythicPuzzle.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicPuzzle.UI
{
    /// <summary>Level 1's visual tutorial and short, automatic reward close-up.</summary>
    public sealed class LevelOneSequenceController : MonoBehaviour
    {
        [SerializeField] private PuzzleDirector puzzleDirector;
        [SerializeField] private PuzzleInputRouter inputRouter;
        [SerializeField] private Camera worldCamera;
        [SerializeField] private GameObject crawlActor;
        [SerializeField] private GameObject happyActor;
        [SerializeField] private SpriteRenderer tutorialEraser;
        [SerializeField] private string nextSceneName = "Level_002_BalGanesha";
        [SerializeField] private Vector3 closeUpCameraPosition = new(0.7f, -2.1f, -10f);
        [SerializeField] private float closeUpOrthographicSize = 4f;
        [SerializeField] private float closeUpDuration = 0.7f;
        [SerializeField] private float closeUpHoldSeconds = 2f;

        private Coroutine tutorialRoutine;
        private Coroutine finishRoutine;

        private void Awake()
        {
            if (happyActor != null) happyActor.SetActive(false);
            if (tutorialEraser != null) tutorialEraser.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (puzzleDirector != null)
            {
                puzzleDirector.StateChanged += OnStateChanged;
                puzzleDirector.LevelCompleted += OnLevelCompleted;
            }
            if (inputRouter != null) inputRouter.SignalRaised += OnInputSignal;
        }

        private void OnDisable()
        {
            if (puzzleDirector != null)
            {
                puzzleDirector.StateChanged -= OnStateChanged;
                puzzleDirector.LevelCompleted -= OnLevelCompleted;
            }
            if (inputRouter != null) inputRouter.SignalRaised -= OnInputSignal;
        }

        private void OnStateChanged(PuzzleState state)
        {
            if (state == PuzzleState.AwaitingInput && tutorialRoutine == null && finishRoutine == null)
                tutorialRoutine = StartCoroutine(AnimateTutorial());
            else if (state == PuzzleState.Success || state == PuzzleState.Complete)
                StopTutorial();
        }

        private void OnInputSignal(PuzzleInputSignal signal)
        {
            if (signal.Kind == PuzzleInputKind.EraseProgress && signal.ScalarValue > 0f)
                StopTutorial();
        }

        private IEnumerator AnimateTutorial()
        {
            if (tutorialEraser == null) yield break;
            tutorialEraser.gameObject.SetActive(true);
            var origin = tutorialEraser.transform.position;
            var color = tutorialEraser.color;
            var clock = 0f;
            while (true)
            {
                clock += Time.deltaTime;
                var phase = (clock % 1.8f) / 1.8f;
                var sweep = Mathf.SmoothStep(0f, 1f, phase < 0.5f ? phase * 2f : (1f - phase) * 2f);
                tutorialEraser.transform.position = origin + new Vector3(0.13f * Mathf.Sin(clock * 7f), (sweep - 0.5f) * 1.15f, 0f);
                tutorialEraser.color = new Color(color.r, color.g, color.b, 0.72f + 0.28f * Mathf.Sin(clock * 3f) * Mathf.Sin(clock * 3f));
                yield return null;
            }
        }

        private void StopTutorial()
        {
            if (tutorialRoutine != null) StopCoroutine(tutorialRoutine);
            tutorialRoutine = null;
            if (tutorialEraser != null) tutorialEraser.gameObject.SetActive(false);
        }

        private void OnLevelCompleted(LevelDefinition _)
        {
            if (finishRoutine == null) finishRoutine = StartCoroutine(ShowRewardAndAdvance());
        }

        private IEnumerator ShowRewardAndAdvance()
        {
            StopTutorial();
            if (happyActor != null) happyActor.SetActive(true);
            if (crawlActor != null) crawlActor.SetActive(false);

            if (worldCamera != null)
            {
                var fromPosition = worldCamera.transform.position;
                var fromSize = worldCamera.orthographicSize;
                var elapsed = 0f;
                while (elapsed < closeUpDuration)
                {
                    elapsed += Time.deltaTime;
                    var blend = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsed / Mathf.Max(0.01f, closeUpDuration)));
                    worldCamera.transform.position = Vector3.Lerp(fromPosition, closeUpCameraPosition, blend);
                    worldCamera.orthographicSize = Mathf.Lerp(fromSize, closeUpOrthographicSize, blend);
                    yield return null;
                }
                worldCamera.transform.position = closeUpCameraPosition;
                worldCamera.orthographicSize = closeUpOrthographicSize;
            }

            yield return new WaitForSeconds(closeUpHoldSeconds);
            if (!string.IsNullOrWhiteSpace(nextSceneName)) SceneManager.LoadScene(nextSceneName);
        }
    }
}
