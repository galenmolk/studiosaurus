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

    private Dictionary<string, SpriteAsset> spritesInMemory = new Dictionary<string, SpriteAsset>();

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

        if (spritesInMemory.ContainsKey(filePath))
        {
            ReplaceSpriteAsset(spritesInMemory[filePath], newSpriteAsset);
            spritesInMemory.Remove(filePath);
            Debug.Log("Replacing sprite " + filePath);
        }
        
        spritesInMemory.Add(filePath, newSpriteAsset);
        FileGallery.Instance.AddFile(newSpriteAsset);
    }

    public void DeleteSprite(SpriteAsset spriteAsset)
    {
        if (spritesInMemory.ContainsKey(spriteAsset.path))
        {
            spritesInMemory.Remove(spriteAsset.path);
            Debug.Log("Deleting sprite " + spriteAsset.assetName);

            ReplaceSpriteAsset(spriteAsset);
        }
    }

    private void ReplaceSpriteAsset(SpriteAsset oldSprite, SpriteAsset newSprite = null)
    {
        foreach (SpriteAssetObject spriteAssetObject in oldSprite.spriteAssetObjects)
        {
            spriteAssetObject.UpdateSpriteAsset(newSprite);
        }
    }
}
