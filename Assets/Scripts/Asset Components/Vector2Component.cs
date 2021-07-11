using UnityEngine;

namespace Studiosaurus
{
    public abstract class Vector2Component : ConfigComponent
    {
        [SerializeField] protected Vector2Controls vector2ControlsPrefab;

        protected Vector2Controls vector2Controls;
        protected DoItObject doItObject;
        protected RectTransform rectTransform;

        protected virtual void Awake()
        {
            rectTransform = transform.parent as RectTransform;
            doItObject = transform.parent.GetComponent<DoItObject>();
        }
    }
}