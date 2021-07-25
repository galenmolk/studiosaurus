using UnityEngine;

namespace Studiosaurus
{
    public abstract class ConfigControls : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
