using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    public abstract class Vector2Component : ConfigComponent
    {
        [SerializeField] protected Vector2Controls vector2ControlsPrefab = null;

        protected Vector2Controls vector2Controls;
        protected RectTransform rectTransform;

        public Vector2? currentVector = null;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = transform.parent as RectTransform;
        }

        [UnityEngine.ContextMenu("Size")]
        public override string GetComponentAsJSON()
        {
            return JsonSerializer.GetVector2(this);
        }

        protected void Apply(Vector2 vector, bool forceApply = false)
        {
            if (!componentIsActive && !forceApply)
                return;

            vector2Controls?.UpdateDisplayedVector(vector);
            currentVector = vector;
        }
    }
}
