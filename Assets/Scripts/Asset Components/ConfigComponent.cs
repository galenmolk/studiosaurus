using UnityEngine;

namespace Studiosaurus
{
    public abstract class ConfigComponent : MonoBehaviour
    {
        public string JSONKey;

        public abstract ConfigControls OpenControlSection(); 
    }
}
