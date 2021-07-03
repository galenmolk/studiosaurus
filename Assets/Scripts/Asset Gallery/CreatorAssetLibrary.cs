using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatorAssetLibrary : MonoBehaviour
{
    private static CreatorAssetLibrary sharedInstance;
    public static CreatorAssetLibrary Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<CreatorAssetLibrary>();

            return sharedInstance;
        }
    }

    private Dictionary<string, SpriteAsset> assetsInMemory = new Dictionary<string, SpriteAsset>();

    private void Awake()
    {
        sharedInstance = this;
    }

    public void AddNewSprite(Sprite sprite, string filePath)
    {
        string fileName = Path.GetFileName(filePath);

        if (fileName == string.Empty)
            fileName = filePath;

        SpriteAsset newSpriteAsset = new SpriteAsset(fileName, sprite, filePath);

        if (assetsInMemory.ContainsKey(filePath))
        {
            ReplaceAsset(assetsInMemory[filePath], newSpriteAsset);
            assetsInMemory.Remove(filePath);
            Debug.Log("Replacing sprite " + filePath);
        }
        
        assetsInMemory.Add(filePath, newSpriteAsset);
        AssetSelector.Instance.AddFile(newSpriteAsset);
    }

    public void DeleteAsset(SpriteAsset asset)
    {
        if (assetsInMemory.ContainsKey(asset.path))
        {
            assetsInMemory.Remove(asset.path);
            Debug.Log("Deleting sprite " + asset.assetName);

            ReplaceAsset(asset);
        }
    }

    private void ReplaceAsset(SpriteAsset oldAsset, SpriteAsset newAsset = null)
    {
        foreach (SpriteAssetObject assetObject in oldAsset.assetObjects)
        {
            assetObject.UpdateAsset(newAsset);
        }
    }
}
