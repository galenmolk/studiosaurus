using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ContextMenu : Window
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private DoItObject doItObject;
    private readonly WaitForEndOfFrame endOfFrame;

    [SerializeField] private AssetSlotGallery gallery;

    public IEnumerator Open(DoItObject doItObject, Vector2 clickPosition)
    {
        this.doItObject = doItObject;
        OpenControlSections();
        transform.SetAsLastSibling();
        ActivateClosePanel(doItObject.transform);

        yield return endOfFrame;

        PositionMenu(clickPosition);
    }

    private void OpenControlSections()
    {
        foreach (ControlSection controlSection in doItObject.controlSections)
        {
            ControlSection newSection = Instantiate(controlSection, transform);
            newSection.InitializeControls(doItObject);
        }
    }

    public void PositionMenu(Vector2 clickPos)
    {
        Vector2 menuSize = rectTransform.sizeDelta * StudioCanvas.Instance.ScaleFactor;

        float xOffset = Screen.width - clickPos.x > menuSize.x ? menuSize.x * 0.5f : menuSize.x * -0.5f;
        float yOffset = clickPos.y > menuSize.y ? menuSize.y * -0.5f : menuSize.y * 0.5f;
        Vector2 offset = new Vector2(xOffset, yOffset);

        rectTransform.anchoredPosition = doItObject.RectTransform.InverseTransformPoint(clickPos + offset);
        transform.SetParent(StudioCanvas.Instance.transform);
        Utils.SetCanvasGroupEnabled(canvasGroup, true);
    }

    public void SelectImage()
    {
        AssetSelector.Instance.Open(doItObject, gallery);
    }

    private bool mouseExitedScreen;
    private Vector2 dragOffset;

    public void SetOffset(PointerEventData eventData)
    {
        dragOffset = rectTransform.anchoredPosition - (Vector2)StudioCanvas.Instance.RectTransform.InverseTransformPoint(eventData.position);
    }

    public void MoveContextMenu(PointerEventData eventData)
    {
        if (!StudioCanvas.Instance.CanvasContainsMouse())
        {
            mouseExitedScreen = true;
            return;
        }

        if (mouseExitedScreen)
        {
            rectTransform.anchoredPosition = (Vector2)StudioCanvas.Instance.RectTransform.InverseTransformPoint(eventData.position) + dragOffset;
            mouseExitedScreen = false;
        }

        Vector2 newPos = rectTransform.anchoredPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor;
        rectTransform.anchoredPosition = StudioCanvas.Instance.ConstrainPositionToCanvas(newPos);
    }
}
