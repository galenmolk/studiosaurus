using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class DoItObject : SpriteAssetObject
{
    [SerializeField] private string doItObjectName = null;
    public string DoItObjectName { get { return doItObjectName; } }

    

    [SerializeField] private Handles handlesPrefab = null;

    private Image image;
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
}
