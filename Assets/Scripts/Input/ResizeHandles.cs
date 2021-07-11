using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class ResizeHandles : MonoBehaviour
    {
        public Vector2Event onPositionChanged = new Vector2Event();
        public Vector2Event onSizeChanged = new Vector2Event();

        private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;

        private Vector2 CurrentPosition { get { return rectTransform.anchoredPosition; } }
        private Vector2 CurrentSize { get { return rectTransform.sizeDelta; } }

        private Vector2 newPostion;
        private Vector2 newSize;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = transform.parent as RectTransform;
        }

        public void AdjustLeftEdge(PointerEventData eventData)
        {
            newPostion = CurrentPosition + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) / 2f;
            newSize = CurrentSize + new Vector2(-eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
        }

        public void AdjustRightEdge(PointerEventData eventData)
        {
            newPostion = CurrentPosition + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) / 2f;
            newSize = CurrentSize + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
        }

        public void AdjustTopEdge(PointerEventData eventData)
        {
            newPostion = CurrentPosition + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) / 2f;
            newSize = CurrentSize + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
        }

        public void AdjustBottomEdge(PointerEventData eventData)
        {
            newPostion = CurrentPosition + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) / 2f;
            newSize = CurrentSize + new Vector2(0f, -eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
        }

        public void AdjustTopLeftCorner(PointerEventData eventData)
        {
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
            newSize = CurrentSize + new Vector2(-eventData.delta.x, eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustTopRightCorner(PointerEventData eventData)
        {
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
            newSize = CurrentSize + eventData.delta / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustBottomLeftCorner(PointerEventData eventData)
        {
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
            newSize = CurrentSize - eventData.delta / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustBottomRightCorner(PointerEventData eventData)
        {
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor / 2f;
            newSize = CurrentSize + new Vector2(eventData.delta.x, -eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
        }

        public void SetHandlesVisibility(bool isVisible)
        {
            canvasGroup.alpha = isVisible ? 1f : 0.25f;
        }

        public void BroadcastResize()
        {
            onPositionChanged?.Invoke(newPostion);
            onSizeChanged?.Invoke(newSize);
        }
    }
}
