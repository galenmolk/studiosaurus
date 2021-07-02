using UnityEngine;
using UnityEngine.EventSystems;

public class Handles : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;

    private DoItObject doItObject;

    private void Awake()
    {
        doItObject = transform.parent.GetComponent<DoItObject>();
    }

    public void OpenContextMenu(PointerEventData eventData)
    {
        doItObject.OpenContextMenu(eventData);
    }

    public void MoveObject(PointerEventData eventData)
    {
        Vector2 newPosition = doItObject.AnchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor;
        doItObject.AnchoredPosition = StudioCanvas.Instance.ConstrainPositionToCanvas(newPosition);
    }

    public void AdjustLeftEdge(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) / 2f;
        doItObject.SizeDelta += new Vector2(-eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
    }

    public void AdjustRightEdge(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) / 2f;
        doItObject.SizeDelta += new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
    }

    public void AdjustTopEdge(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) / 2f;
        doItObject.SizeDelta += new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
    }

    public void AdjustBottomEdge(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) / 2f;
        doItObject.SizeDelta += new Vector2(0f, -eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
    }

    public void AdjustTopLeftCorner(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
        doItObject.SizeDelta += new Vector2(-eventData.delta.x, eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
    }

    public void AdjustTopRightCorner(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
        doItObject.SizeDelta += eventData.delta / StudioCanvas.Instance.ScaleFactor;
    }

    public void AdjustBottomLeftCorner(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
        doItObject.SizeDelta -= eventData.delta / StudioCanvas.Instance.ScaleFactor;
    }

    public void AdjustBottomRightCorner(PointerEventData eventData)
    {
        doItObject.AnchoredPosition += eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
        doItObject.SizeDelta += new Vector2(eventData.delta.x, -eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
    }

    public void SetHandlesVisibility(bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1f : 0f;
    }
}
