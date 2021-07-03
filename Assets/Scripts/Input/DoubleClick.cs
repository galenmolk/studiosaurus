using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

[System.Serializable]
public class OnDoubleClick : UnityEvent<PointerEventData> { }

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    public OnDoubleClick onDoubleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
            onDoubleClick.Invoke(eventData);
    }
}
