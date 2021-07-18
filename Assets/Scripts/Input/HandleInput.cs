using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class HandleInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
    {
        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;
        public UnityEvent onEndDrag;
        public HandleDeltaEvent onDrag;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.Invoke(new HandleDelta(eventData));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke();
        }
    }

    public class HandleDelta
    {
        public HandleDelta(PointerEventData eventData)
        {
            eventData.delta /= StudioCanvas.Instance.ScaleFactor;
            size = eventData.delta;
            position = size * 0.5f;
        }

        public Vector2 size;
        public Vector2 position;
    }
}