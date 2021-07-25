using UnityEngine;
using TMPro;
using System.Collections.Generic;
using static TMPro.TMP_Dropdown;

namespace Studiosaurus
{
    public class ControlsSectionDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown = null;

        private readonly List<ConfigSection> sections = new List<ConfigSection>();
        private readonly List<OptionData> optionDatas = new List<OptionData>();

        private ContextMenu contextMenu;

        private void Awake()
        {
            contextMenu = GetComponentInParent<ContextMenu>();
        }

        public void AddSection(ConfigSection section)
        {
            sections.Add(section);
        }

        public void Create(int startingIndex)
        {
            for (int i = 0; i < sections.Count; i++)
            {
                OptionData optionData = new OptionData(sections[i].sectionName);
                optionDatas.Add(optionData);
            }
            dropdown.AddOptions(optionDatas);
            dropdown.SetValueWithoutNotify(startingIndex);
        }

        public void OnDropdownSelected(int selection)
        {
            ConfigSection selectedSection = sections[selection];
            StartCoroutine(contextMenu.ChangeConfigSectionTo(selectedSection));
        }

        public void OnDisable()
        {
            for (int i = 0, count = sections.Count; i < count; i++)
            {
                sections[i].ResetSection();
            }
        }
    }
}
