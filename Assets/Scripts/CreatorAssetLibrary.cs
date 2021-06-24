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

    private const string SPRITE_FILENAME = "Image";

    private void Awake()
    {
        sharedInstance = this;
    }

    public bool HasSpriteBeenLoaded(string url)
    {
        return spritesInMemory.ContainsKey(url);
    }

    public void AddNewSprite(Sprite sprite, string filePath)
    {
        string fileName = Path.GetFileName(filePath);

        if (fileName == string.Empty)
            fileName = SPRITE_FILENAME + spritesInMemory.Count;

        if (spritesInMemory.ContainsKey(fileName))
        {
            spritesInMemory.Remove(fileName);
            Debug.Log("Replacing sprite " + fileName);
        }

        SpriteAsset spriteAsset = new SpriteAsset(fileName, sprite);

        spritesInMemory.Add(fileName, spriteAsset);

        FileGallery.Instance.AddNewFile(spriteAsset);
    }

    public void DeleteSprite(SpriteAsset spriteAsset)
    {
        if (spritesInMemory.ContainsKey(spriteAsset.assetName))
        {
            spritesInMemory.Remove(spriteAsset.assetName);
            Debug.Log("Deleting sprite " + spriteAsset.assetName);
        }
    }
}
