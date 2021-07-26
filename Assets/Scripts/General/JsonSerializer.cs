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
            string jsonVector = $"{{\n{GetKey("x")} {vector2.x},\n {GetKey("y")} {vector2.y}\n}}";
            return jsonKey + jsonVector;
        }

        public static string GetTweenSizeScale(TweenSizeComponent tweenSizeComponent, Vector2 startSize)
        {
            string jsonKey = $"{GetKey(tweenSizeComponent.configKey.key)}";
            Vector2 tweenToSize = tweenSizeComponent.currentVector.Value;

            float scaleX = tweenToSize.x / startSize.x;
            float scaleY = tweenToSize.y / startSize.y;

            float scaleAverage = (scaleX + scaleY) * 0.5f;

            return jsonKey + scaleAverage;
        }

        public static string GetAsset(string key, string path)
        {
            string jsonKey = $"{GetKey(key)}";
            string assetPath = $"{{\n{GetKey("awsInternalPath")} \"{path}\"\n}}"; // unfinished

            return jsonKey + assetPath;
        }
    }
}
