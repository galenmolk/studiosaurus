using UnityEngine;

public class SpriteAsset : GenericAsset
{
    public SpriteAsset(Sprite sprite, string assetName, string path) : base(assetName, path)
    {
        this.sprite = sprite;
    }

    public Sprite sprite;
}
