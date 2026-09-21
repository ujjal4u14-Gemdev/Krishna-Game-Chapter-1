using System.Collections.Generic;
using UnityEngine;

namespace MythicPuzzle.UI
{
    public sealed class UIScreenController : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> panels = new();
        private readonly Dictionary<UIScreenId, UIPanel> registry = new();
        private readonly Stack<UIScreenId> history = new();

        private void Awake()
        {
            registry.Clear();
            foreach (var panel in panels)
            {
                if (panel == null || panel.ScreenId == UIScreenId.None) continue;
                registry[panel.ScreenId] = panel;
                panel.Hide();
            }
        }

        public void Show(UIScreenId screenId, bool rememberCurrent = true)
        {
            if (!registry.TryGetValue(screenId, out var next)) return;

            foreach (var pair in registry)
            {
                if (pair.Value.gameObject.activeSelf && pair.Key != screenId)
                {
                    if (rememberCurrent) history.Push(pair.Key);
                    pair.Value.Hide();
                }
            }

            next.Show();
        }

        public void Back()
        {
            if (history.Count > 0)
            {
                Show(history.Pop(), false);
            }
        }
    }
}

