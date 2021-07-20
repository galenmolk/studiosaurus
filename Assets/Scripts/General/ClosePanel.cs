using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus {
    public class ClosePanel : MonoBehaviour, IPointerDownHandler
    {
        public Window currentWindow;

        private static readonly List<ClosePanel> closePanels = new List<ClosePanel>();

        private void Awake()
        {
            closePanels.Add(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && closePanels.Count > 0)
                closePanels[closePanels.Count - 1].Close();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Close();
        }

        private void Close()
        {
            currentWindow.Close();
        }

        private void OnDestroy()
        {
            closePanels.Remove(this);
        }
    }
}
