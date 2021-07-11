using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class StudioCanvas : MonoBehaviour
{
    [SerializeField] private float edgeBuffer = 5f;
    [SerializeField] private RectTransform rectTransform = null;

    private readonly Vector2 objectClampBuffer = new Vector2(40f, 40f);

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

    private static StudioCanvas sharedInstance;
    private Canvas canvas;

    public Vector2 ConstrainPositionToCanvas(Vector2 position)
    {
        float x = Mathf.Clamp(position.x, rectTransform.rect.xMin, rectTransform.rect.xMax);
        float y = Mathf.Clamp(position.y, rectTransform.rect.yMin, rectTransform.rect.yMax);
        return new Vector2(x, y);
    }

    public Vector2 ClampObjectPositionToCanvas(Vector2 size, Vector2 position)
    {
        Rect rect = RectTransform.rect;
        Vector2 halfSize = (size - objectClampBuffer) * 0.5f;
        float newX = Mathf.Clamp(position.x, rect.xMin - halfSize.x, rect.xMax + halfSize.x);
        float newY = Mathf.Clamp(position.y, rect.yMin - halfSize.y, rect.yMax + halfSize.y);
        return new Vector2(newX, newY);
    }

    public struct MouseBounds
    {
        public bool OffScreen { get { return offScreenX || offScreenY; } }
        public bool offScreenX;
        public bool offScreenY;
    }

    public MouseBounds GetMouseBoundsInfo()
    {
        Vector2 mousePos = Input.mousePosition;
        MouseBounds bounds = new MouseBounds
        {
            offScreenX = mousePos.x < -edgeBuffer || mousePos.x > Screen.width + edgeBuffer,
            offScreenY = mousePos.y < -edgeBuffer || mousePos.y > Screen.height + edgeBuffer
        };
        return bounds;
    }
}
