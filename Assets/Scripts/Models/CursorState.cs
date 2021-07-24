using UnityEngine;

namespace Studiosaurus
{
    public enum HandleType
    {
        Corners,
        Sides,
        Body,
        None
    }

    public static class CursorState
    {
        public static HandleType handleType = HandleType.None;

        public static CustomCursor hoveringCursor;
        public static CustomCursor interactingCursor;

        private static bool hovering;
        private static bool interacting;

        public static void SetHoveringCursor(CustomCursor cursor)
        {
            hoveringCursor = cursor;
            hovering = true;

            if (!interacting)
                SetCursor(hoveringCursor);
        }

        public static void SetInteractingCursor(CustomCursor cursor)
        {
            interacting = true;
            interactingCursor = cursor;
            SetCursor(interactingCursor);
        }

        public static void ResetHoveringCursor(CustomCursor cursor)
        {
            if (hovering && hoveringCursor?.name == cursor.name)
            {
                hovering = false;
                hoveringCursor = null;
            }

            TryReset();
        }

        public static void ResetInteractingCursor(CustomCursor cursor)
        {
            if (interacting && interactingCursor?.name == cursor.name)
            {
                interacting = false;
                interactingCursor = null;
            }

            if (hovering && !interacting)
                SetCursor(hoveringCursor);

            TryReset();
        }

        private static void TryReset()
        {
            if (!hovering && !interacting)
                ResetCursor();
        }

        private static void SetCursor(CustomCursor cursor)   
        {
            handleType = cursor.handleAsset.handleType;
            Cursor.SetCursor(cursor.handleAsset.texture, cursor.handleAsset.size, CursorMode.Auto);
        }

        private static void ResetCursor()
        {
            hoveringCursor = null;
            interactingCursor = null;
            handleType = HandleType.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
