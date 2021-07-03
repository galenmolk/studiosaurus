using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPositionChanged : UnityEvent<Vector2> { }
public class OnScaleChanged : UnityEvent<Vector2> { }

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class DoItObject : SpriteAssetObject
{
    [SerializeField] private Image image = null;
    [SerializeField] private ContextMenu contextMenuPrefab = null;

    [SerializeField] private string doItObjectName = null;
    public string DoItObjectName { get { return doItObjectName; } }
    
    [SerializeField] private Handles handlesPrefab = null;

    public OnPositionChanged onPositionChanged = new OnPositionChanged();
    public OnScaleChanged onScaleChanged = new OnScaleChanged();

    public ControlSection[] controlSections = null;

    private ContextMenu contextMenu;

    private RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = transform as RectTransform;

            return rectTransform;
        }
    }


    private Vector2 clampBuffer = new Vector2(40f, 40f);

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
            onScaleChanged?.Invoke(newSize);
        }
    }

    private void Awake()
    {
        Instantiate(handlesPrefab, transform);
    }

    public override void UpdateAsset(SpriteAsset spriteAsset)
    {
        if (spriteAsset == null)
        {
            RemoveAssetFromObject();
            return;
        }    

        base.UpdateAsset(spriteAsset);
        image.sprite = spriteAsset.sprite;
        image.SetNativeSize();
    }

    public override void RemoveAssetFromObject()
    {
        base.RemoveAssetFromObject();
        image.sprite = null;
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
}
