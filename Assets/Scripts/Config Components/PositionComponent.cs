using UnityEngine;

namespace Studiosaurus
{
    public class PositionComponent : Vector2Component
    {
        protected override void Awake()
        {
            base.Awake();
            doItObject.resizeHandles.onPositionChanged.AddListener(SetPosition);
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            vector2Controls = Instantiate(vector2ControlsPrefab, contextMenu.transform);
            vector2Controls.onVector2Inputted.AddListener(SetPosition);
            doItObject.dragHandle.onDrag.AddListener(vector2Controls.UpdateDisplayedVector);
            vector2Controls.UpdateDisplayedVector(doItObject.RectTransform.anchoredPosition);
        }

        private void SetPosition(Vector2 position)
        {
            if (rectTransform.sizeDelta.x == 0 || rectTransform.sizeDelta.y == 0)
                return;

            Vector2 clampedPosition = StudioCanvas.Instance.ClampObjectPositionToCanvas(rectTransform.sizeDelta, position);

            rectTransform.anchoredPosition = clampedPosition;
            vector2Controls?.UpdateDisplayedVector(clampedPosition);
        }
    }
}