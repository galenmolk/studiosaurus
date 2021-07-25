using UnityEngine;
using TMPro;

namespace Studiosaurus
{
    public class ConfigControlsSection : MonoBehaviour
    {
        [SerializeField] private TMP_Text sectionNameText = null;

        public ConfigSection configSection;

        private ContextMenu contextMenu;

        private void Awake()
        {
            contextMenu = GetComponentInParent<ContextMenu>();
        }

        public void SetSection(ConfigSection configSection)
        {
            this.configSection = configSection;
            sectionNameText.text = configSection.sectionName;
        }

        public void OnControlSectionClicked()
        {
            contextMenu.ToggleConfigSection(configSection);
        }

        public void OnDisable()
        {
            configSection.ResetSection();
        }
    }
}
