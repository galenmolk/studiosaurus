using UnityEngine;

namespace Studiosaurus
{
    public enum Handle
    {
        Corners,
        Sides,
        Body,
        None
    }

    public static class CursorState
    {
        public static Handle state;

        private static CustomCursor currentCursor;

        public static void SetCursor(CustomCursor cursor)
        {
            if (currentCursor != null && currentCursor.interacting)
                return;

            currentCursor = cursor;
            state = cursor.handleAsset.handleState;
            Cursor.SetCursor(cursor.handleAsset.texture, cursor.handleAsset.size, CursorMode.Auto);
        }

        public static void ResetCursor(CustomCursor cursor)
        {
            if (currentCursor == null || currentCursor != cursor)
                return;

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            state = Handle.None;
            currentCursor = null;
        }
    }
}

