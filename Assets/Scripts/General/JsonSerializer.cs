using UnityEngine;

namespace Studiosaurus {
    public static class JsonSerializer
    {
        public static string GetKey(string key)
        {
            return $"\"{key}\": ";
        }

        public static string GetVector2(Vector2Component vector2Component)
        {
            string jsonKey = $"{GetKey(vector2Component.configKey.key)}";
            Vector2 vector2 = vector2Component.currentVector.Value;
            string jsonVector = $"{{{GetKey("x")} {vector2.x}, {GetKey("x")} {vector2.y}}}";
            return jsonKey + jsonVector;
        }
    }
}
