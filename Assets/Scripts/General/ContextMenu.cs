using System.Collections;
using UnityEngine;

namespace Studiosaurus
{
    public class ContextMenu : Window
    {
        [SerializeField] private ControlsSectionDropdown dropdownPrefab = null;

        private ControlsSectionDropdown dropdown;
        private RectTransform rectTransform;
        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        private ConfigSection activeConfigSection;

        private Vector2? lastClickPos = null;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = transform as RectTransform;
        }

        public IEnumerator Open(DoItObject doItObject, Vector2 clickPos)
        {
            lastClickPos = clickPos;
            OpenWindow(doItObject.transform);
            OpenConfigControls(doItObject);
            //PositionMenu(clickPos);
            yield return null;
        }

        private void OpenConfigControls(DoItObject doItObject)
        {
            dropdown = Instantiate(dropdownPrefab, transform);
            for (int sectionIndex = 0;  sectionIndex < doItObject.configSections.Length; sectionIndex++)
            {
                ConfigSection section = doItObject.configSections[sectionIndex];
                dropdown.AddSection(section);
                for (int componentIndex = 0; componentIndex < section.configComponents.Count; componentIndex++)
                {
                    ConfigComponent component = section.configComponents[componentIndex];
                    section.configControls.Add(component.OpenControls(transform));
                }
            }
            dropdown.Create();
        }

        public IEnumerator ChangeConfigSectionTo(ConfigSection section)
        {
            bool settingToActive = !section.isSectionActive;
            yield return StartCoroutine(section.SetSectionAsActive(settingToActive));

            if (activeConfigSection != null)
                yield return StartCoroutine(activeConfigSection.SetSectionAsActive(false));

            activeConfigSection = settingToActive ? section : null;

            if (lastClickPos.HasValue)
            {
                PositionMenu(lastClickPos.Value);
                lastClickPos = null;
            }
        }

        public void PositionMenu(Vector2 clickPos)
        {
            Vector2 menuSize = rectTransform.rect.size * StudioCanvas.Instance.ScaleFactor;
            Debug.Log(menuSize);
            Debug.Log(rectTransform.rect.size);
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
    }
}
