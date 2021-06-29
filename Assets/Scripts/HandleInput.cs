using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnPointerClick : UnityEvent<PointerEventData> { }

[RequireComponent(typeof(CanvasGroup))]
public class HandleInput : InputDetector, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup canvasGroup = null;

    public OnPointerClick onRightClick;
    public OnPointerClick onDoubleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
            onDoubleClick.Invoke(eventData);
        else if (eventData.button == PointerEventData.InputButton.Right)
            onRightClick.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0f;
    }
}
