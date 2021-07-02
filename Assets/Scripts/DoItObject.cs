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
    [SerializeField] private ContextMenu contextMenuPrefab = null;

    [SerializeField] private string doItObjectName = null;
    public string DoItObjectName { get { return doItObjectName; } }
    
    [SerializeField] private Handles handlesPrefab = null;

    public OnPositionChanged onPositionChanged = new OnPositionChanged();
    public OnScaleChanged onScaleChanged = new OnScaleChanged();

    public ControlSection[] controlSections = null;

    private Image image;
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

    public Vector2 AnchoredPosition
    {
        get
        {
            return RectTransform.anchoredPosition;
        }
        set
        {
            RectTransform.anchoredPosition = value;
            onPositionChanged?.Invoke(value);
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
            RectTransform.sizeDelta = value;
            onScaleChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        CacheReferences();
        Instantiate(handlesPrefab, transform);
    }

    private void CacheReferences()
    {
        image = GetComponent<Image>();
    }

    public override void UpdateSpriteAsset(SpriteAsset spriteAsset)
    {
        if (spriteAsset == null)
        {
            DeleteSpriteAsset();
            return;
        }    

        base.UpdateSpriteAsset(spriteAsset);
        image.sprite = spriteAsset.sprite;
        image.SetNativeSize();
    }

    public override void DeleteSpriteAsset()
    {
        base.DeleteSpriteAsset();
        image.sprite = null;
    }

    public void OpenContextMenu(PointerEventData eventData)
    {
        if (contextMenu != null)
        {
            contextMenu.PositionMenu(eventData.position);
            return;
        }

        contextMenu = Instantiate(contextMenuPrefab, transform);
        StartCoroutine(contextMenu.Open(this, eventData.position));
    }
}
