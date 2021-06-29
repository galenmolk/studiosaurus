using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ContextMenu : Window
{
    [SerializeField] private RectTransform rectTransform;

    private DoItObject currentDoItObject;
    private Vector2 menuSize;

    public void Open(DoItObject doItObject)
    {
        ActivateClosePanel(doItObject.transform);
        currentDoItObject = doItObject;
        PositionMenu(doItObject.transform as RectTransform);
        transform.SetAsLastSibling();
    }

    private void PositionMenu(RectTransform objectTransform)
    {
        if (menuSize == Vector2.zero)
            menuSize = rectTransform.sizeDelta;

        Vector2 size = objectTransform.sizeDelta;
        rectTransform.anchoredPosition = new Vector2(0, (size.y + menuSize.y) * 0.5f);
    }

    public override void Close()
    {
        base.Close();
        currentDoItObject = null;
        Destroy(gameObject);
    }

    public void SelectImage()
    {
        FileGallery.Instance.Open(currentDoItObject);
    }
}
