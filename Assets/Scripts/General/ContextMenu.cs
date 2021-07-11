using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(RectTransform))]
    public class ContextMenu : Window
    {
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        public IEnumerator Open(DoItObject doItObject)
        {
            transform.SetAsLastSibling();
            ActivateClosePanel(doItObject.transform);
            yield return OpenConfigControls(doItObject);
        }

        private IEnumerator OpenConfigControls(DoItObject doItObject)
        {
            for (int i = 0, count = doItObject.configComponents.Count; i < count; i++)
            {
                doItObject.configComponents[i].OpenControls(this);
            }
            yield return endOfFrame;
        }

        public void PositionMenu(Vector2 clickPos)
        {
            Vector2 menuSize = rectTransform.sizeDelta * StudioCanvas.Instance.ScaleFactor;

            float xOffset = Screen.width - clickPos.x > menuSize.x ? menuSize.x * 0.5f : menuSize.x * -0.5f;
            float yOffset = clickPos.y > menuSize.y ? menuSize.y * -0.5f : menuSize.y * 0.5f;
            Vector2 offset = new Vector2(xOffset, yOffset);

            rectTransform.anchoredPosition = StudioCanvas.Instance.RectTransform.InverseTransformPoint(clickPos + offset);
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }
    }
}


