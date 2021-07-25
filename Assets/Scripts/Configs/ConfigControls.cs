using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public abstract class ConfigControls : MonoBehaviour
    {
        public Selectable[] selectables;

        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
