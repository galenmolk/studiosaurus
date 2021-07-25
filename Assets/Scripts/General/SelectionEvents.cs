using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class SelectionEvents : MonoBehaviour
    {
        public static bool selectionEventsEnabled = false;

        private GameObject lastSelectedGameObject = null;

        private void Update()
        {
            if (!selectionEventsEnabled)
                return;

            if (Input.GetKeyDown(KeyCode.Tab))
                Tab.SelectNext();

            GameObject gameObject = EventSystem.current.currentSelectedGameObject;
            if (lastSelectedGameObject != gameObject)
            {
                lastSelectedGameObject = gameObject;
                Tab.NewObjectSelected(gameObject);
            }
        }
    }
}