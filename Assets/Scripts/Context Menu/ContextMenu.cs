using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(RectTransform))]
    public class ContextMenu : Window
    {
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        private DoItObject doItObject;
        private readonly WaitForEndOfFrame endOfFrame;

        public IEnumerator Open(DoItObject doItObject, Vector2 clickPosition)
        {
            this.doItObject = doItObject;
            OpenControlSections();
            transform.SetAsLastSibling();
            ActivateClosePanel(doItObject.transform);

            yield return endOfFrame;

            PositionMenu(clickPosition);
        }

        private void OpenControlSections()
        {
            for (int i = 0, length = doItObject.configComponents.Length; i < length; i++)
            {
                doItObject.configComponents[i].OpenControlSection().transform.SetParent(transform);
            }
        }

        public void PositionMenu(Vector2 clickPos)
        {
            Vector2 menuSize = rectTransform.sizeDelta * StudioCanvas.Instance.ScaleFactor;

            float xOffset = Screen.width - clickPos.x > menuSize.x ? menuSize.x * 0.5f : menuSize.x * -0.5f;
            float yOffset = clickPos.y > menuSize.y ? menuSize.y * -0.5f : menuSize.y * 0.5f;
            Vector2 offset = new Vector2(xOffset, yOffset);

            rectTransform.anchoredPosition = doItObject.transform.InverseTransformPoint(clickPos + offset); // does this work? or should we switch back to RectTransform
            transform.SetParent(StudioCanvas.Instance.transform);
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }
    }
}


