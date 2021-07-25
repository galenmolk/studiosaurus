using UnityEngine;

namespace Studiosaurus
{
    [CreateAssetMenu(menuName = "Custom Assets/ConfigKey", fileName = "New ConfigKey", order = 0)]
    public class ConfigKey : ScriptableObject
    {
        public string key;
    }
}
