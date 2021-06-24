using UnityEngine;

public class SpriteAsset
{
    public SpriteAsset(string assetName, Sprite sprite)
    {
        this.assetName = assetName;
        this.sprite = sprite;
    }

    public Sprite sprite;
    public string assetName;
}