using UnityEngine;

namespace MythicPuzzle.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        [SerializeField] private UIScreenId screenId;
        [SerializeField] private CanvasGroup canvasGroup;

        public UIScreenId ScreenId => screenId;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            if (canvasGroup == null) return;
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            gameObject.SetActive(false);
        }
    }
}

