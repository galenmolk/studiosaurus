using UnityEngine;

namespace Studiosaurus
{
    public abstract class Vector2Component : ConfigComponent
    {
        [SerializeField] protected Vector2Controls vector2ControlsPrefab = null;

        protected Vector2Controls vector2Controls;
        protected RectTransform rectTransform;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = transform.parent as RectTransform;
        }
    }
}