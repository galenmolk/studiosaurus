using UnityEngine;

namespace Studiosaurus
{
    public struct MouseBounds
    {
        public bool OffScreen { get { return offScreenX || offScreenY; } }
        public bool offScreenX;
        public bool offScreenY;
    }

    [RequireComponent(typeof(Canvas))]
    public class StudioCanvas : MonoBehaviour
    {
        public static StudioCanvas Instance
        {
            get
            {
                if (sharedInstance == null)
                    sharedInstance = FindObjectOfType<StudioCanvas>();

                return sharedInstance;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        public float ScaleFactor
        {
            get
            {
                if (canvas == null)
                    canvas = GetComponent<Canvas>();

                return canvas.scaleFactor;
            }
        }

        [SerializeField] private float screenEdgeBuffer = 5f;

        private static StudioCanvas sharedInstance;
        private Canvas canvas;
        private RectTransform rectTransform;
        private readonly Vector2 objectClampBuffer = new Vector2(40f, 40f);

        private void Awake()
        {
            sharedInstance = this;
        }

        // Remove this once we have a toolbar for objects. That way you will never be able to "lose" an object off screen.
        public Vector2 ClampObjectPositionToCanvas(Vector2 size, Vector2 position)
        {
            Rect rect = RectTransform.rect;
            Vector2 halfSize = (size - objectClampBuffer) * 0.5f;
            float newX = Mathf.Clamp(position.x, rect.xMin - halfSize.x, rect.xMax + halfSize.x);
            float newY = Mathf.Clamp(position.y, rect.yMin - halfSize.y, rect.yMax + halfSize.y);
            return new Vector2(newX, newY);
        }

        public MouseBounds GetMouseBoundsInfo()
        {
            Vector2 mousePos = Input.mousePosition;
            MouseBounds bounds = new MouseBounds
            {
                offScreenX = mousePos.x < -screenEdgeBuffer || mousePos.x > Screen.width + screenEdgeBuffer,
                offScreenY = mousePos.y < -screenEdgeBuffer || mousePos.y > Screen.height + screenEdgeBuffer
            };
            return bounds;
        }
    }
}