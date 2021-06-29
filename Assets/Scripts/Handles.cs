using UnityEngine;
using UnityEngine.EventSystems;

public class Handles : MonoBehaviour
{
    [SerializeField] private ContextMenu contextMenuPrefab = null;

    private DoItObject doItObject;
    private RectTransform objectRectTransform;
    private Canvas canvas;
    private ContextMenu contextMenu;

    private void Awake()
    {
        doItObject = transform.parent.GetComponent<DoItObject>();
        objectRectTransform = doItObject.transform as RectTransform;
        canvas = FindObjectOfType<Canvas>();
    }

    public void OpenContextMenu()
    {
        contextMenu = Instantiate(contextMenuPrefab, doItObject.transform);
        contextMenu.Open(doItObject);
    }

    public void CloseContextMenu()
    {
        if (contextMenu != null)
            contextMenu.Close();
    }

    public void MoveObject(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void AdjustLeftEdge(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += new Vector2(eventData.delta.x / canvas.scaleFactor, 0f) / 2f;
        objectRectTransform.sizeDelta += new Vector2(-eventData.delta.x / canvas.scaleFactor, 0f);
    }

    public void AdjustRightEdge(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += new Vector2(eventData.delta.x / canvas.scaleFactor, 0f) / 2f;
        objectRectTransform.sizeDelta += new Vector2(eventData.delta.x / canvas.scaleFactor, 0f);
    }

    public void AdjustTopEdge(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += new Vector2(0f, eventData.delta.y / canvas.scaleFactor) / 2f;
        objectRectTransform.sizeDelta += new Vector2(0f, eventData.delta.y / canvas.scaleFactor);
    }

    public void AdjustBottomEdge(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += new Vector2(0f, eventData.delta.y / canvas.scaleFactor) / 2f;
        objectRectTransform.sizeDelta += new Vector2(0f, -eventData.delta.y / canvas.scaleFactor);
    }

    public void AdjustTopLeftCorner(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / 2f;
        objectRectTransform.sizeDelta += new Vector2(-eventData.delta.x, eventData.delta.y) / canvas.scaleFactor;
    }

    public void AdjustTopRightCorner(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / 2f;
        objectRectTransform.sizeDelta += eventData.delta / canvas.scaleFactor;
    }

    public void AdjustBottomLeftCorner(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / 2f;
        objectRectTransform.sizeDelta -= eventData.delta / canvas.scaleFactor;
    }

    public void AdjustBottomRightCorner(PointerEventData eventData)
    {
        objectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor / 2f;
        objectRectTransform.sizeDelta += new Vector2(eventData.delta.x, -eventData.delta.y) / canvas.scaleFactor;
    }
}
