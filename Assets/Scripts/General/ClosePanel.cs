using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    public class ClosePanel : MonoBehaviour, IPointerDownHandler
    {
        public Window CurrentWindow { get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            CloseWindow();
        }

        public void CloseWindow()
        {
            CurrentWindow.Close();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                CurrentWindow.Close();
        }
    }
}