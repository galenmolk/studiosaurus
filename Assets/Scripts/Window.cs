using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private ClosePanel closePanelPrefab = null;

    private ClosePanel closePanel;

    protected void ActivateClosePanel(Transform windowScope)
    {
        int siblingIndex = windowScope.GetSiblingIndex();
        closePanel = Instantiate(closePanelPrefab, windowScope.parent);
        closePanel.CurrentWindow = this;
        closePanel.transform.SetSiblingIndex(siblingIndex--);
    }

    public virtual void Close()
    {
        if (closePanel != null)
            Destroy(closePanel);
    }
} 