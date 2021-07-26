using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Window : MonoBehaviour
    {
        [SerializeField] private ClosePanel closePanelPrefab = null;

        protected ClosePanel closePanel;
        protected CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            Utils.SetCanvasGroupEnabled(canvasGroup, false);
        }

        public void OpenWindow(Transform windowScope)
        {
            ActivateClosePanel(windowScope);
        }

        private void ActivateClosePanel(Transform windowScope)
        {
            if (closePanel != null)
                return;

            int siblingIndex = windowScope.GetSiblingIndex();
            closePanel = Instantiate(closePanelPrefab, windowScope.parent);
            closePanel.currentWindow = this;
            closePanel.transform.SetSiblingIndex(siblingIndex--);
        }

        public virtual void Close()
        {
            Destroy(closePanel?.gameObject);
            Utils.SetCanvasGroupEnabled(canvasGroup, false);
        }
    }
}
