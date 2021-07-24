using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    [RequireComponent(typeof(CanvasGroup))]
    public class ClosePanel : MonoBehaviour, IPointerDownHandler
    {
        [HideInInspector] public Window currentWindow;
        [HideInInspector] public CanvasGroup canvasGroup;

        private static readonly List<ClosePanel> closePanels = new List<ClosePanel>();

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            closePanels.Add(this);
            DisableOtherClosePanels();
        }

        private void DisableOtherClosePanels()
        {
            foreach (ClosePanel closePanel in closePanels)
            {
                if (closePanel != this)
                    Utils.SetCanvasGroupEnabled(closePanel.canvasGroup, false);
            }
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || !canvasGroup.blocksRaycasts)
                return;

            Close();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Close();
        }

        private void Close()
        {
            currentWindow.Close();
        }

        private void OnDestroy()
        {
            closePanels.Remove(this);

            int count = closePanels.Count;
            if (count > 0)
                Utils.SetCanvasGroupEnabled(closePanels[count - 1].canvasGroup, true);
        }
    }
}
