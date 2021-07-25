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

        public override ConfigControls OpenControls(Transform parent)
        {
            vector2Controls = Instantiate(vector2ControlsPrefab, parent);
            vector2Controls.onVector2Inputted.AddListener(SetPosition);
            doItObject.dragHandle.onDrag.AddListener(vector2Controls.UpdateDisplayedVector);
            vector2Controls.UpdateDisplayedVector(doItObject.RectTransform.anchoredPosition);
            return vector2Controls;
        }

        private void SetPosition(Vector2 position)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                return;

            Apply(position);
        }

        private void Apply(Vector2 position)
        {
            rectTransform.anchoredPosition = position;
            vector2Controls?.UpdateDisplayedVector(position);
        }
    }
}