using UnityEngine;

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

    public void Close()
    {
        Destroy(closePanel.gameObject);
        Destroy(gameObject);
    }
} 