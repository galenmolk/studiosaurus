using UnityEngine;

namespace Studiosaurus
{
    public abstract class ConfigComponent : MonoBehaviour
    {
        protected DoItObject doItObject;

        public string JSONKey;

        protected virtual void Awake()
        {
            doItObject = transform.parent.GetComponent<DoItObject>();
        }

        public abstract void OpenControls(ContextMenu contextMenu);
    }
}
