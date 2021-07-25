using UnityEngine;

namespace Studiosaurus
{
    [CreateAssetMenu(menuName = "Custom Assets/HandleAsset", fileName = "New HandleAsset")]
    public class HandleAsset : ScriptableObject
    {
        public Texture2D texture;
        public Vector2 size;
        public HandleType handleType;
    }
}
