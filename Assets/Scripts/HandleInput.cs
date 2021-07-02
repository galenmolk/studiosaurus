using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnPointerClick : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnPointerEnter : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnPointerExit : UnityEvent<PointerEventData> { }

public class HandleInput : InputDetector, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public OnPointerClick onRightClick;
    public OnPointerClick onDoubleClick;
    public OnPointerEnter onPointerEnter;
    public OnPointerEnter onPointerExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
            onDoubleClick.Invoke(eventData);
        else if (eventData.button == PointerEventData.InputButton.Right)
            onRightClick.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit.Invoke(eventData);
    }
}
