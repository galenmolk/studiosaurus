using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    public class ClosePanel : MonoBehaviour, IPointerDownHandler
    {
        public Window currentWindow;

        public void OnPointerDown(PointerEventData eventData)
        {
            Close();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Close();
        }

        private void Close()
        {
            currentWindow.Close();
        }
    }
}
