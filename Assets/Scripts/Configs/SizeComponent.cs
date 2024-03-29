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

        public override ConfigControls OpenControls(Transform parent)
        {
            vector2Controls = Instantiate(vector2ControlsPrefab, parent);
            vector2Controls.onVector2Inputted.AddListener(SetSize);

            if (currentVector.HasValue)
                SetSize(currentVector.Value);
            else
                Apply(doItObject.RectTransform.sizeDelta, true);

            doItObject.onNewSpriteAssigned.AddListener(SetNewSizeParameters);
            return vector2Controls;
        }

        private void SetSize(Vector2 newSize)
        {
            if (Input.GetKey(KeyCode.LeftShift) && CursorState.handleType == HandleType.Corners)
                newSize = ScaleProportionally(newSize);

            rectTransform.sizeDelta = newSize;

            SetNegativeWarningLinesEnabled(rectTransform.sizeDelta.x < 0 || rectTransform.sizeDelta.y < 0);
            Apply(newSize);
        }

        public override void Activate()
        {
            base.Activate();
            SetSize(currentVector.Value);
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