using UnityEngine;

namespace Studiosaurus
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private ClosePanel closePanelPrefab = null;

        protected ClosePanel closePanel;

        protected void ActivateClosePanel(Transform windowScope)
        {
            int siblingIndex = windowScope.GetSiblingIndex();
            closePanel = Instantiate(closePanelPrefab, windowScope.parent);
            closePanel.CurrentWindow = this;
            closePanel.transform.SetSiblingIndex(siblingIndex--);
        }

        public virtual void Close()
        {
            Destroy(closePanel.gameObject);
            Destroy(gameObject);
        }
    }
}
