using UnityEngine;

namespace Studiosaurus
{
    public class SizeComponent : Vector2Component
    {
        private float sizeRatio;
        private Vector2 originalSize;

        protected override void Awake()
        {
            base.Awake();
            doItObject.resizeHandles.onSizeChanged.AddListener(SetSize);
            SetNewSizeParameters();
            doItObject.fixNegativeSize.AddListener(() => SetSize(originalSize));
        }

        private void SetNewSizeParameters()
        {
            originalSize = rectTransform.sizeDelta;
            sizeRatio = originalSize.x / originalSize.y;

            if (vector2Controls != null)
                vector2Controls.UpdateDisplayedVector(originalSize);
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            vector2Controls = Instantiate(vector2ControlsPrefab, contextMenu.transform);
            vector2Controls.onVector2Inputted.AddListener(SetSize);
            vector2Controls.UpdateDisplayedVector(doItObject.RectTransform.sizeDelta);
            doItObject.onNewSpriteAssigned.AddListener(SetNewSizeParameters);
        }

        private void SetSize(Vector2 newSize)
        {
            if (Input.GetKey(KeyCode.LeftShift) && CursorState.handleType == HandleType.Corners)
                newSize = ScaleProportionally(newSize);

            rectTransform.sizeDelta = newSize;

            SetNegativeWarningLinesEnabled(rectTransform.sizeDelta.x < 0 || rectTransform.sizeDelta.y < 0);
            vector2Controls?.UpdateDisplayedVector(newSize);
        }

        private void SetNegativeWarningLinesEnabled(bool isEnabled)
        {
            doItObject.negativeSizeWarning.SetEnabled(isEnabled);
        }

        // Scale to the smaller size 
        private Vector2 ScaleProportionally(Vector2 newSize)
        {
            if (newSize.x < newSize.y)
                newSize.y = newSize.x / sizeRatio;

            if (newSize.y < newSize.x)
                newSize.x = newSize.y * sizeRatio;

            return newSize;
        }
    }
}