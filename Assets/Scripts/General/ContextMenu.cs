using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    public class ContextMenu : Window
    {
        [SerializeField] private ConfigControlsSection configControlsSectionPrefab = null;

        private RectTransform rectTransform;

        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        private ConfigSection activeConfigSection;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = transform as RectTransform;
        }

        public IEnumerator Open(DoItObject doItObject, Vector2 clickPos)
        {
            OpenWindow(doItObject.transform);
            yield return OpenConfigControls(doItObject);
            PositionMenu(clickPos);
        }

        private IEnumerator OpenConfigControls(DoItObject doItObject)
        {
            foreach (ConfigSection section in doItObject.configSections)
            {
                Instantiate(configControlsSectionPrefab, transform).SetSection(section);
                foreach (ConfigComponent configComponent in section.configComponents)
                {
                    section.configControls.Add(configComponent.OpenControls(transform));
                }
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
            rectTransform.SetAsLastSibling();
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }

        public override void Close()
        {
            base.Close();
            Destroy(gameObject);
        }

        public void ToggleConfigSection(ConfigSection section)
        {
            bool settingToActive = !section.isSectionActive;
            section.SetSectionAsActive(settingToActive);

            activeConfigSection?.SetSectionAsActive(false);

            activeConfigSection = settingToActive ? section : null;
        }
    }
}
