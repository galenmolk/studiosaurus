using System.Collections.Generic;
using UnityEngine;

public class GenericAsset
{
    public GenericAsset(string assetName, string path, Sprite sprite)
    {
        this.assetName = assetName;
        this.path = path;
        this.sprite = sprite;
    }

    public string assetName;
    public string path;
    public Sprite sprite;

    public List<AssetComponent> assetComponents = new List<AssetComponent>();

    public void ReplaceAssetWith(GenericAsset newAsset = null)
    {
        for (int i = assetComponents.Count - 1; i >= 0; i--)
        {
            assetComponents[i].UpdateAsset(newAsset);
        }
    }
}
