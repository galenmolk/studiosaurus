using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class StudioCanvas : MonoBehaviour
{
    [SerializeField] private float edgeBuffer = 5f;
    [SerializeField] private RectTransform rectTransform = null;

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

    public bool CanvasContainsMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        bool containsXPos = mousePos.x > -edgeBuffer && mousePos.x < Screen.width + edgeBuffer;
        bool containsYPos = mousePos.y > -edgeBuffer && mousePos.y < Screen.height + edgeBuffer;
        return containsXPos && containsYPos;
    }
}
