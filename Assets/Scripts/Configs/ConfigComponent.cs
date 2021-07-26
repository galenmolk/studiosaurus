using UnityEngine;

namespace Studiosaurus
{
    public abstract class ConfigComponent : MonoBehaviour
    {
        protected DoItObject doItObject;

        public ConfigKey configKey;

        [HideInInspector] public bool componentIsActive = false;

        public virtual void Activate()
        {
            componentIsActive = true;
        }

        protected virtual void Awake()
        {
            doItObject = transform.parent.GetComponent<DoItObject>();
        }

        public abstract ConfigControls OpenControls(Transform parent);

        public abstract string GetComponentAsJSON();
    }
}
