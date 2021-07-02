using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ContextMenu : Window
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private DoItObject currentDoItObject;
    private readonly WaitForEndOfFrame endOfFrame;

    public IEnumerator Open(DoItObject doItObject, Vector2 clickPosition)
    {
        currentDoItObject = doItObject;
        OpenControlSections();
        transform.SetAsLastSibling();
        ActivateClosePanel(doItObject.transform);

        yield return endOfFrame;

        PositionMenu(clickPosition);
    }

    private void OpenControlSections()
    {
        foreach (ControlSection controlSection in currentDoItObject.controlSections)
        {
            ControlSection newSection = Instantiate(controlSection, transform);
            newSection.InitializeControls(currentDoItObject);
        }
    }

    public void PositionMenu(Vector2 clickPosition)
    {
        Vector2 menuSize = rectTransform.sizeDelta;
        Vector2 canvasSize = StudioCanvas.Instance.GetCanvasBounds();

        float xOffset = (canvasSize.x - clickPosition.x >= menuSize.x) ? menuSize.x * 0.5f : menuSize.x * -0.5f;
        float yOffset = (canvasSize.y - clickPosition.y >= menuSize.y) ? menuSize.y * 0.5f : menuSize.y * -0.5f;
        Vector2 offset = new Vector2(xOffset, yOffset);

        rectTransform.anchoredPosition = currentDoItObject.RectTransform.InverseTransformPoint(clickPosition + (offset * StudioCanvas.Instance.ScaleFactor));
        Utils.SetCanvasGroupEnabled(canvasGroup, true);
    }

    public void SelectImage()
    {
        FileGallery.Instance.Open(currentDoItObject);
    }

    public void MoveContextMenu(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor;
        rectTransform.anchoredPosition = StudioCanvas.Instance.ConstrainPositionToCanvas(newPosition);
    }
}
