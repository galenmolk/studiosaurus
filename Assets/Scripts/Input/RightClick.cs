using UnityEngine.EventSystems;
using UnityEngine;

namespace Studiosaurus
{
    public class RightClick : MonoBehaviour, IPointerClickHandler
    {
        public PointerDataEvent onRightClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                onRightClick.Invoke(eventData);
        }
    }
}