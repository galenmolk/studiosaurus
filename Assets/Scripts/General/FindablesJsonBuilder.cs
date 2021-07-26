using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public static class FindablesJsonBuilder
    {
        private static string findablesArrayJsonString;
        private static List<DoItObject> currentFindables;
        private static List<ConfigComponent> allComponents;

        public static string CreateFindablesJsonArray(List<DoItObject> findables)
        {
            currentFindables = findables;
            findablesArrayJsonString = string.Empty;
            findablesArrayJsonString += JsonSerializer.GetKey("findables");
            findablesArrayJsonString += "[\n";

            for (int i = 0; i < findables.Count; i++)
            {
                CreateFindable(findables[i]);
            }
            findablesArrayJsonString += "],";
            return findablesArrayJsonString;
        }

        private static void CreateFindable(DoItObject findable)
        {
            allComponents = GetAllComponents(findable.configSections);

            findablesArrayJsonString += "{\n";

            for (int i = 0; i < allComponents.Count; i++)
            {
                CreateComponent(allComponents[i]);

                if (i < allComponents.Count - 1)
                    findablesArrayJsonString += ",\n";
            }

            findablesArrayJsonString += "}";

            int findableIndex = currentFindables.IndexOf(findable);
            int count = currentFindables.Count;

            if (findableIndex < count - 1)
                findablesArrayJsonString += ",\n";
        }

        private static void CreateComponent(ConfigComponent component)
        {
            string componentConfig = component.GetComponentAsJSON();
            findablesArrayJsonString += componentConfig;
        }

        private static List<ConfigComponent> GetAllComponents(ConfigSection[] allSections)
        {
            List<ConfigComponent> components = new List<ConfigComponent>();
            foreach (ConfigSection section in allSections)
            {
                foreach (ConfigComponent component in section.configComponents)
                {
                    components.Add(component);
                }
            }
            return components;
        }
    }
}