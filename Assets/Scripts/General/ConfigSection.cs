using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus {

    [Serializable]
    public class ConfigSection
    {
        public string sectionName;

        public ConfigComponent[] configComponentPrefabs;

        [HideInInspector] public bool isSectionActive = false;
        [HideInInspector] public bool wasLastSectionActive = false;

        [HideInInspector] public List<ConfigComponent> configComponents = new List<ConfigComponent>();
        [HideInInspector] public List<ConfigControls> configControls = new List<ConfigControls>();

        private int ConfigCount
        {
            get
            {
                if (configCount == -1)
                    configCount = configComponents.Count;

                return configCount;
            }
        }

        private int configCount = -1;

        public void SetSectionAsActive(bool isActive)
        {
            isSectionActive = isActive;
            for (int i = 0; i < ConfigCount; i++)
            {
                configControls[i].gameObject.SetActive(isActive);
                configComponents[i].displayChangesOnObject = isActive;
            }

            if (isActive)
                Tab.SetCurrentSelectables(configControls);
        }

        public void ResetSection()
        {
            configControls.Clear();
            configCount = -1;

            if (isSectionActive)
                wasLastSectionActive = true;

            isSectionActive = false;

            for (int i = 0; i < ConfigCount; i++)
            {
                configComponents[i].displayChangesOnObject = false;
            }
        }
    }
}
