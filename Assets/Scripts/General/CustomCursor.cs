using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    public class CustomCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
    {
        public HandleAsset handleAsset = null;

        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorState.SetHoveringCursor(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorState.ResetHoveringCursor(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CursorState.SetInteractingCursor(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CursorState.ResetInteractingCursor(this);
        }
    }
}