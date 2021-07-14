using UnityEngine;

namespace Studiosaurus
{
    public struct CursorAsset
    {
        public Texture2D texture;
        public Vector2 size;
    }

    public static class CursorState
    {
        private static CursorAsset currentAsset;

        public static void SetCursor(CursorAsset asset)
        {
            Cursor.SetCursor(asset.texture, asset.size, CursorMode.Auto);
            currentAsset = asset;
        }

        public static void ResetCursor(CursorAsset asset)
        {
            if (asset.texture == currentAsset.texture)
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}

