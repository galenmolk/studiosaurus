using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class HandleInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
    {
        public PointerDataEvent onPointerEnter;
        public PointerDataEvent onPointerExit;
        public PointerDataEvent onDrag;
        public PointerDataEvent onDragEnd;

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

        public void OnEndDrag(PointerEventData eventData)
        {
            onDragEnd.Invoke(eventData);
        }
    }
}