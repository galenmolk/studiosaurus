using UnityEngine;

namespace Studiosaurus
{
    [CreateAssetMenu(menuName = "CursorAsset", fileName = "New Cursor")]
    public class HandleAsset : ScriptableObject
    {
        public Texture2D texture;
        public Vector2 size;
        public Handle handleState;
    }
}
