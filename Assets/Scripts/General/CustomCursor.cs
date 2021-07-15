using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    public class CustomCursor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IEndDragHandler
    {
        public HandleAsset handleAsset = null;

        private bool hovering = false;
        public bool interacting = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovering = true;
            CursorState.SetCursor(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovering = false;

            if (interacting == false)
                CursorState.ResetCursor(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            interacting = true;
            CursorState.SetCursor(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            interacting = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            interacting = false;

            if (hovering == false)
                CursorState.ResetCursor(this);
        }
    }
}