using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnPointerEnter : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnPointerExit : UnityEvent<PointerEventData> { }

[System.Serializable]
public class OnDrag : UnityEvent<PointerEventData> { }

public class HandleInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public OnPointerEnter onPointerEnter;
    public OnPointerEnter onPointerExit;
    public OnDrag onDrag;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag.Invoke(eventData);
    }
}
