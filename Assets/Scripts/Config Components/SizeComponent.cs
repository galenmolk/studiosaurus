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

        private void SetSize(Vector2 size)
        {
            Vector2 newSize = new Vector2(Mathf.Clamp(size.x, 0f, Mathf.Infinity), Mathf.Clamp(size.y, 0f, Mathf.Infinity));

            if (Input.GetKey(KeyCode.LeftShift) && CursorState.handleType == HandleType.Corners)
                newSize = ScaleProportionally(newSize);

            rectTransform.sizeDelta = newSize;
            vector2Controls?.UpdateDisplayedVector(newSize);
        }

        private Vector2 ScaleProportionally(Vector2 newSize)
        {
            Vector2 currentSize = rectTransform.sizeDelta;

            if (currentSize.x < currentSize.y)
                newSize.y = newSize.x / doItObject.SizeRatio;

            if (currentSize.y < currentSize.x)
                newSize.x = newSize.y * doItObject.SizeRatio;

            return newSize;
        }
    }
}