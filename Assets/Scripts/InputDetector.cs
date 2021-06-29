using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnPointerDown : UnityEvent<PointerEventData> {}

[System.Serializable]
public class OnPointerUp : UnityEvent<PointerEventData> {}

[System.Serializable]
public class OnDrag : UnityEvent<PointerEventData> {}

public class InputDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public OnPointerDown onPointerDown;
    public OnPointerUp onPointerUp;
    public OnDrag onDrag;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag.Invoke(eventData);
    }
}
