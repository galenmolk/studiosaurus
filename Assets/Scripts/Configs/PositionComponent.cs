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
            doItObject.dragHandle.onDrag.AddListener((vector) => Apply(vector));

            if (currentVector.HasValue)
                SetPosition(currentVector.Value);
            else
                Apply(doItObject.RectTransform.anchoredPosition, true); 

            return vector2Controls;
        }

        public override void Activate()
        {
            base.Activate();
            SetPosition(currentVector.Value);
        }

        private void SetPosition(Vector2 position)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                return;

            rectTransform.anchoredPosition = position;

            Apply(position);
        }
    }
}