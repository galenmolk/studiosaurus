using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPositionChanged : UnityEvent<Vector2> { }
public class OnSizeChanged : UnityEvent<Vector2> { }

[RequireComponent(typeof(Image))]
public class DoItObject : AssetComponent
{
    [SerializeField] private ContextMenu contextMenuPrefab = null;
    [SerializeField] private Handles handlesPrefab = null;
    [SerializeField] private Image image = null;

    public OnPositionChanged onPositionChanged = new OnPositionChanged();
    public OnSizeChanged onSizeChanged = new OnSizeChanged();
    public ControlSection[] controlSections = null;

    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = transform as RectTransform;

            return rectTransform;
        }
    }

    public Vector2 AnchoredPosition
    {
        get
        {
            return RectTransform.anchoredPosition;
        }
        set
        {
            if (SizeDelta.x == 0 || SizeDelta.y == 0)
                return;

            Rect rect = StudioCanvas.Instance.RectTransform.rect;
            Vector2 halfSize = (SizeDelta - clampBuffer) * 0.5f;
            float newX = Mathf.Clamp(value.x, rect.xMin - halfSize.x, rect.xMax + halfSize.x);
            float newY = Mathf.Clamp(value.y, rect.yMin - halfSize.y, rect.yMax + halfSize.y);
            Vector2 newPosition = new Vector2(newX, newY);

            RectTransform.anchoredPosition = newPosition;
            onPositionChanged?.Invoke(newPosition);
        }
    }

    public Vector2 SizeDelta
    {
        get
        {
            return RectTransform.sizeDelta;
        }
        set
        {
            Vector2 newSize = new Vector2(Mathf.Clamp(value.x, 0f, Mathf.Infinity), Mathf.Clamp(value.y, 0f, Mathf.Infinity));
            RectTransform.sizeDelta = newSize;
            onSizeChanged?.Invoke(newSize);
        }
    }

    private ContextMenu contextMenu;
    private readonly Vector2 clampBuffer = new Vector2(40f, 40f);
    private RectTransform rectTransform;
    
    private void Awake()
    {
        Instantiate(handlesPrefab, transform);
    }

    public void OpenContextMenu(PointerEventData eventData)
    {
        if (contextMenu != null)
        {
            contextMenu.transform.SetParent(transform);
            contextMenu.PositionMenu(eventData.position);
            return;
        }

        contextMenu = Instantiate(contextMenuPrefab, transform);
        StartCoroutine(contextMenu.Open(this, eventData.position));
    }

    public override void UpdateAsset(GenericAsset asset)
    {
        base.UpdateAsset(asset);

        if (asset == null)
        {
            image.sprite = null;
            return;
        }

        image.sprite = asset.sprite;
        image.SetNativeSize();

        // Clamp position to on screen
        AnchoredPosition = AnchoredPosition;
    }
}
