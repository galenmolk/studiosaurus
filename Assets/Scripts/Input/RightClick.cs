using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Studiosaurus
{
    [System.Serializable]
    public class OnRightClick : UnityEvent<PointerEventData> { }

    public class RightClick : MonoBehaviour, IPointerClickHandler
    {
        public OnRightClick onRightClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                onRightClick.Invoke(eventData);
        }
    }
}