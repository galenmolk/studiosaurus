using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    public class ContextMenu : Window
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        private void Awake()
        {
            rectTransform = transform as RectTransform;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator Open(DoItObject doItObject)
        {
            doItObject.transform.SetAsLastSibling();
            transform.SetAsLastSibling();
            ActivateClosePanel(transform);
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

            // Always make sure the ContextMenu appears on screen
            float xOffset = Screen.width - clickPos.x > menuSize.x ? menuSize.x * 0.5f : menuSize.x * -0.5f;
            float yOffset = clickPos.y > menuSize.y ? menuSize.y * -0.5f : menuSize.y * 0.5f;
            Vector2 clickOffset = new Vector2(xOffset, yOffset);

            rectTransform.anchoredPosition = StudioCanvas.Instance.RectTransform.InverseTransformPoint(clickPos + clickOffset);
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }
    }
}
