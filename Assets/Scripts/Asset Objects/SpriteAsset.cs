using System.Collections.Generic;
using UnityEngine;

public class SpriteAsset
{
    public SpriteAsset(string assetName, Sprite sprite, string path)
    {
        this.assetName = assetName;
        this.sprite = sprite;
        this.path = path;
    }

    public Sprite sprite;
    public string assetName;
    public string path;

    // List of all objects this SpriteAsset belongs to.
    // Use this to update all objects if a SpriteAsset is replaced/updated with a new version.
    public List<SpriteAssetObject> assetObjects = new List<SpriteAssetObject>();

    public void DisconnectObjectFromAsset(SpriteAssetObject assetObject)
    {
        if (assetObjects.Contains(assetObject))
            assetObjects.Remove(assetObject);
    }
}
