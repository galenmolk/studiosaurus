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

        public override ConfigControls OpenControlSection()
        {
            vector2Controls = Instantiate(vector2ControlsPrefab);
            vector2Controls.onVector2Inputted.AddListener(SetSize);
            return vector2Controls;
        }

        private void SetSize(Vector2 size)
        {
            Vector2 clampedSize = new Vector2(Mathf.Clamp(size.x, 0f, Mathf.Infinity), Mathf.Clamp(size.y, 0f, Mathf.Infinity));
            rectTransform.sizeDelta = clampedSize;
            vector2Controls?.UpdateDisplayedVector(clampedSize);
        }
    }
}