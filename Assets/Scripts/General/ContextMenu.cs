using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class ContextMenu : Window
    {
        [SerializeField] private ControlsSectionDropdown dropdownPrefab = null;
        [SerializeField] private Button deleteButtonPrefab = null;

        private Button deleteButton = null;

        private RectTransform rectTransform;
        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        private ConfigSection activeConfigSection;

        private Vector2 lastClickPos;

        private DoItObject doItObject;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = transform as RectTransform;
        }

        public void Open(DoItObject doItObject, Vector2 clickPos)
        {

            this.doItObject = doItObject;
            SelectionEvents.selectionEventsEnabled = true;
            lastClickPos = clickPos;
            OpenWindow(doItObject.transform);
            StartCoroutine(OpenConfigControls(doItObject));
        }

        private IEnumerator OpenConfigControls(DoItObject doItObject)
        {
            ControlsSectionDropdown dropdown = Instantiate(dropdownPrefab, transform);

            SectionIndex startingSection = new SectionIndex() { section = doItObject.configSections[0], index = 0 };

            for (int sectionIndex = 0;  sectionIndex < doItObject.configSections.Length; sectionIndex++)
            {
                ConfigSection section = doItObject.configSections[sectionIndex];

                if (section.wasLastSectionActive)
                {
                    startingSection.index = sectionIndex;
                    startingSection.section = section;
                    section.wasLastSectionActive = false;
                }

                dropdown.AddSection(section);

                for (int componentIndex = 0; componentIndex < section.configComponents.Count; componentIndex++)
                {
                    ConfigComponent component = section.configComponents[componentIndex];
                    section.configControls.Add(component.OpenControls(transform));
                }
            }

            dropdown.Create(startingSection.index);
            yield return StartCoroutine(ChangeConfigSectionTo(startingSection.section));
            PositionMenu(lastClickPos);
        }

        public IEnumerator ChangeConfigSectionTo(ConfigSection section)
        {
            bool settingToActive = !section.isSectionActive;
            section.SetSectionAsActive(settingToActive);

            if (activeConfigSection != null)
                activeConfigSection.SetSectionAsActive(false);

            activeConfigSection = settingToActive ? section : null;

            CreateDeleteButton();

            yield return endOfFrame;
        }

        private void CreateDeleteButton()
        {
            if (deleteButton != null)
                return;

            deleteButton = Instantiate(deleteButtonPrefab, transform);
            deleteButton.onClick.AddListener(() => doItObject.onConfirmDelete.Invoke(doItObject));
        }

        public void PositionMenu(Vector2 clickPos)
        {
            Vector2 size = rectTransform.sizeDelta * StudioCanvas.Instance.ScaleFactor;

            // ContextMenu Pivot Y is set to 1 so LayoutGroup only expands downward.
            // Because Pivot is not in the center (0.5, 0.5), click position must be modified here for offset calculation.
            clickPos.y += size.y * 0.5f;
            float heightWithPivotCompensation = size.y * 1.5f;

            // Compare the margins between the click position and the edge of screen against the size of the menu
            // Default to a standard menu placement of below and to the right of the mouse click
            // If either of those axes would place menu offscreen, go in the other direction
            float xOffset = Screen.width - clickPos.x > size.x ? size.x * 0.5f : size.x * -0.5f;

            float yOffset = 0f;
            if (clickPos.y < heightWithPivotCompensation)
                yOffset = size.y * 0.5f;
            else
                yOffset = size.y * -0.5f;
            //float yOffset = clickPos.y > heightWithPivotCompensation ? size.y * -0.5f : size.y * 0.5f;

            Vector2 offset = new Vector2(xOffset, yOffset);

            rectTransform.anchoredPosition = StudioCanvas.Instance.RectTransform.InverseTransformPoint(clickPos + offset);
            
            rectTransform.SetAsLastSibling();
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }

        public override void Close()
        {
            base.Close();
            SelectionEvents.selectionEventsEnabled = false;
            Destroy(gameObject);
        }
    }
}
