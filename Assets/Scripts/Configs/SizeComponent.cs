using UnityEngine;

namespace Studiosaurus
{
    public class SizeComponent : Vector2Component
    {
        protected override void Awake()
        {
            base.Awake();
            doItObject.resizeHandles.onSizeChanged.AddListener(SetSize);
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            vector2Controls = Instantiate(vector2ControlsPrefab, contextMenu.transform);
            vector2Controls.onVector2Inputted.AddListener(SetSize);
            vector2Controls.UpdateDisplayedVector(doItObject.RectTransform.sizeDelta);
            doItObject.onImageNativeSizeSet.AddListener(vector2Controls.UpdateDisplayedVector);
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
                newSize.y = newSize.x / doItObject.SizeRatio;

            if (newSize.y < newSize.x)
                newSize.x = newSize.y * doItObject.SizeRatio;

            return newSize;
        }
    }
}