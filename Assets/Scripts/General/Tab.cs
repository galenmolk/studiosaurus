using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus {
    public static class Tab
    {
        private static readonly List<Selectable> currentSelectables = new List<Selectable>();

        private static int selectableIndex = -1;

        public static void SetCurrentSelectables(List<ConfigControls> configControls)
        {
            currentSelectables.Clear();
            for (int controlsIndex = 0, count = configControls.Count; controlsIndex < count; controlsIndex++)
            {
                ConfigControls controls = configControls[controlsIndex];
                for (int i = 0, length = controls.selectables.Length; i < length; i++)
                {
                    currentSelectables.Add(controls.selectables[i]);
                }
            }
        }

        public static void SelectNext()
        {
            int currentCount = currentSelectables.Count;

            if (currentCount <= 0)
                return;

            int direction = Input.GetKey(KeyCode.LeftShift) ? -1 : 1;
            selectableIndex += direction;

            if (selectableIndex >= currentCount)
                selectableIndex = 0;

            if (selectableIndex < 0)
                selectableIndex = currentCount - 1;

            currentSelectables[selectableIndex]?.Select();
        }
    }
}
