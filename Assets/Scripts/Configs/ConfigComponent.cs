using UnityEngine;

namespace Studiosaurus
{
    public abstract class ConfigComponent : MonoBehaviour
    {
        protected DoItObject doItObject;

        public string JSONKey;

        [HideInInspector] public bool displayChangesOnObject = false;

        protected virtual void Awake()
        {
            doItObject = transform.parent.GetComponent<DoItObject>();
        }

        public abstract ConfigControls OpenControls(Transform parent);
    }
}
