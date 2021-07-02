using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class StudioCanvas : MonoBehaviour
{
    private static StudioCanvas sharedInstance;
    public static StudioCanvas Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<StudioCanvas>();

            return sharedInstance;
        }
    }

    private Canvas canvas;
    [SerializeField] private RectTransform rectTransform = null;

    public float ScaleFactor
    {
        get
        {
            if (canvas == null)
                canvas = GetComponent<Canvas>();

            return canvas.scaleFactor;
        }
    }

    public Vector2 ConstrainPositionToCanvas(Vector2 position)
    {
        float x = Mathf.Clamp(position.x, rectTransform.rect.xMin, rectTransform.rect.xMax);
        float y = Mathf.Clamp(position.y, rectTransform.rect.yMin, rectTransform.rect.yMax);
        return new Vector2(x, y);
    }

    public Vector2 GetCanvasBounds()
    {
        return new Vector2(rectTransform.rect.xMax, rectTransform.rect.yMax);
    }
}
