using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public enum ResizingState
    {
        Corners,
        Sides,
        None
    }

    public class ResizeHandles : MonoBehaviour
    {
        public static ResizingState state = ResizingState.None;

        public static bool ResizingCorners { get { return state == ResizingState.Corners; } }
        public static bool ResizingSides { get { return state == ResizingState.Sides; } }

        public Vector2Event onPositionChanged = new Vector2Event();
        public Vector2Event onSizeChanged = new Vector2Event();

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

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
            state = ResizingState.Sides;
            newPostion = CurrentPosition + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) * 0.5f;
            newSize = CurrentSize + new Vector2(-eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
        }

        public void AdjustRightEdge(PointerEventData eventData)
        {
            state = ResizingState.Sides;
            newPostion = CurrentPosition + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f) * 0.5f;
            newSize = CurrentSize + new Vector2(eventData.delta.x / StudioCanvas.Instance.ScaleFactor, 0f);
        }

        public void AdjustTopEdge(PointerEventData eventData)
        {
            state = ResizingState.Sides;
            newPostion = CurrentPosition + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) * 0.5f;
            newSize = CurrentSize + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
        }

        public void AdjustBottomEdge(PointerEventData eventData)
        {
            state = ResizingState.Sides;
            newPostion = CurrentPosition + new Vector2(0f, eventData.delta.y / StudioCanvas.Instance.ScaleFactor) * 0.5f;
            newSize = CurrentSize + new Vector2(0f, -eventData.delta.y / StudioCanvas.Instance.ScaleFactor);
        }

        public void AdjustTopLeftCorner(PointerEventData eventData)
        {
            state = ResizingState.Corners;
            newPostion = CurrentPosition + eventData.delta/ StudioCanvas.Instance.ScaleFactor * 0.5f;
            newSize = CurrentSize + new Vector2(-eventData.delta.x, eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustTopRightCorner(PointerEventData eventData)
        {
            state = ResizingState.Corners;
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor * 0.5f;
            newSize = CurrentSize + eventData.delta / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustBottomLeftCorner(PointerEventData eventData)
        {
            state = ResizingState.Corners;
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor * 0.5f;
            newSize = CurrentSize - eventData.delta / StudioCanvas.Instance.ScaleFactor;
        }

        public void AdjustBottomRightCorner(PointerEventData eventData)
        {
            state = ResizingState.Corners;
            newPostion = CurrentPosition + eventData.delta / StudioCanvas.Instance.ScaleFactor * 0.5f;
            newSize = CurrentSize + new Vector2(eventData.delta.x, -eventData.delta.y) / StudioCanvas.Instance.ScaleFactor;
        }

        public void SetHandlesVisibility(bool isVisible)
        {
            canvasGroup.alpha = isVisible || state != ResizingState.None ? 1f : 0.25f;
        }

        public void EndResizing()
        {
            state = ResizingState.None;
        }

        public void BroadcastResize()
        {
            onPositionChanged?.Invoke(newPostion);
            onSizeChanged?.Invoke(newSize);
        }
    }
}
