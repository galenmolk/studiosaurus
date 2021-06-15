using System;
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
        if (spritesInMemory.ContainsKey(filePath))
        {
            spritesInMemory.Remove(filePath);
            Debug.Log("Replacing sprite " + filePath);
        }

        string fileName = GetFileName(filePath);
        if (fileName == string.Empty)
            fileName = GenerateFileName(SPRITE_FILENAME, spritesInMemory.Count);

        SpriteAsset spriteAsset = new SpriteAsset(fileName, sprite);

        spritesInMemory.Add(filePath, spriteAsset);

        FileGallery.Instance.AddNewFile(spriteAsset);
    }

    private string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    private string GenerateFileName(string fileType, int fileIndex)
    {
        return fileType + fileIndex;
    }
}
