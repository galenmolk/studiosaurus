using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class DragHandle : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public Vector2Event onDrag = new Vector2Event();

        private bool mouseExitedScreen = false;
        private Vector2 dragOffset;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = transform as RectTransform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetOffset(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveObject(eventData);
        }

        public void SetOffset(PointerEventData eventData)
        {
            dragOffset = rectTransform.anchoredPosition - (Vector2)StudioCanvas.Instance.RectTransform.InverseTransformPoint(eventData.position);
        }

        public void MoveObject(PointerEventData eventData)
        {
            StudioCanvas.MouseBounds bounds = StudioCanvas.Instance.GetMouseBoundsInfo();

            if (mouseExitedScreen && !bounds.OffScreen)
            {
                mouseExitedScreen = false;
                rectTransform.anchoredPosition = (Vector2)StudioCanvas.Instance.RectTransform.InverseTransformPoint(eventData.position) + dragOffset;
            }

            Vector2 delta = eventData.delta;
            mouseExitedScreen = bounds.OffScreen;
            delta.x = bounds.offScreenX ? 0f : delta.x;
            delta.y = bounds.offScreenY ? 0f : delta.y;

            rectTransform.anchoredPosition += delta / StudioCanvas.Instance.ScaleFactor;
            onDrag?.Invoke(rectTransform.anchoredPosition);
        }
    }
}
