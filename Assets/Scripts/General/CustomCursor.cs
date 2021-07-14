using UnityEngine;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class CustomCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D cursorTexture = null;
        [SerializeField] private Vector2 hotspot;

        private float resizeFactor = 0.5818181818f;

        private CursorAsset asset;
        public CursorAsset Asset { get { return asset; } }

        private void Awake()
        {
            //cursorTexture.Resize((int)(cursorTexture.width * resizeFactor), (int)(cursorTexture.height * resizeFactor));
            //cursorTexture.Apply();
            asset = new CursorAsset() { texture = cursorTexture, size = new Vector2(cursorTexture.width/2, cursorTexture.height/2) };
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorState.SetCursor(asset);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorState.ResetCursor(asset);
        }
    }
}
