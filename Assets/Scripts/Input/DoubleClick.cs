using UnityEngine.EventSystems;
using UnityEngine;

namespace Studiosaurus
{
    public class DoubleClick : MonoBehaviour, IPointerClickHandler
    {
        public PointerDataEvent onDoubleClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
                onDoubleClick.Invoke(eventData);
        }
    }
}
