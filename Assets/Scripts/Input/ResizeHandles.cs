using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    public class ResizeHandles : MonoBehaviour
    {
        [SerializeField] private float handleFadeSpeed = 1f;

        [HideInInspector] public Vector2Event onPositionChanged = new Vector2Event();
        [HideInInspector] public Vector2Event onSizeChanged = new Vector2Event();

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private Vector2 CurrentPosition { get { return rectTransform.anchoredPosition; } }
        private Vector2 CurrentSize { get { return rectTransform.sizeDelta; } }

        private Vector2 newPosition;
        private Vector2 newSize;

        private Coroutine fadeInCoroutine;
        private Coroutine fadeOutCoroutine;

        private bool isResizing = false;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = transform.parent as RectTransform;
        }

        public void AdjustLeftEdge(HandleDelta delta)
        {
            SetEdgePositionAlongX(delta.position);
            newSize = CurrentSize + new Vector2(-delta.size.x, 0f);
        }
        
        public void AdjustRightEdge(HandleDelta delta)
        {
            SetEdgePositionAlongX(delta.position);
            newSize = CurrentSize + new Vector2(delta.size.x, 0f);
        }

        public void AdjustTopEdge(HandleDelta delta)
        {
            SetEdgePositionAlongY(delta.position);
            newSize = CurrentSize + new Vector2(0f, delta.size.y);
        }

        public void AdjustBottomEdge(HandleDelta delta)
        {
            SetEdgePositionAlongY(delta.position);
            newSize = CurrentSize + new Vector2(0f, -delta.size.y);
        }

        public void AdjustTopLeftCorner(HandleDelta delta)
        {
            SetCornerPosition(delta.position);
            newSize = CurrentSize + new Vector2(-delta.size.x, delta.size.y);
        }

        public void AdjustTopRightCorner(HandleDelta delta)
        {
            SetCornerPosition(delta.position);
            newSize = CurrentSize + delta.size;
        }

        public void AdjustBottomLeftCorner(HandleDelta delta)
        {
            SetCornerPosition(delta.position);
            newSize = CurrentSize + -delta.size;
        }

        public void AdjustBottomRightCorner(HandleDelta delta)
        {
            SetCornerPosition(delta.position);
            newSize = CurrentSize + new Vector2(delta.size.x, -delta.size.y);
        }

        private void SetEdgePositionAlongX(Vector2 delta)
        {
            newPosition = CurrentPosition + new Vector2(delta.x, 0f);
        }

        private void SetCornerPosition(Vector2 delta)
        {
            newPosition = CurrentPosition + delta;
        }

        private void SetEdgePositionAlongY(Vector2 delta)
        {
            newPosition = CurrentPosition + new Vector2(0f, delta.y);
        }

        public void SetHandlesVisibility(bool isVisible)
        {
            StopCoroutines();
            if (isVisible || isResizing)
                fadeInCoroutine = StartCoroutine(FadeInHandles());
            else
                fadeOutCoroutine = StartCoroutine(FadeOutHandles());
        }

        private void StopCoroutines()
        {
            if (fadeInCoroutine != null)
                StopCoroutine(fadeInCoroutine);

            if (fadeOutCoroutine != null)
                StopCoroutine(fadeOutCoroutine);
        }

        public void SetIsResizing(bool isResizing)
        {
            this.isResizing = isResizing;
        }

        public void BroadcastResize()
        {
            onPositionChanged?.Invoke(newPosition);
            onSizeChanged?.Invoke(newSize);
        }

        private IEnumerator FadeInHandles()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime * handleFadeSpeed;
                yield return null;
            }
        }

        private IEnumerator FadeOutHandles()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= Time.deltaTime * handleFadeSpeed;
                yield return null;
            }
        }
    }
}
