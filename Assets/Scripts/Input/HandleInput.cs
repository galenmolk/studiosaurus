using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnRightClick : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnPointerEnter : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnPointerExit : UnityEvent<PointerEventData> { }

public class HandleInput : InputDetector, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public OnRightClick onRightClick;
    public OnPointerEnter onPointerEnter;
    public OnPointerEnter onPointerExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
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
