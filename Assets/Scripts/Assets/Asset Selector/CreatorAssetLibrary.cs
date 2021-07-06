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

    public AssetGallery sprites;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void AddNewSprite(Sprite sprite, string filePath)
    {
        string fileName = Path.GetFileName(filePath);

        if (fileName == string.Empty)
            fileName = filePath;

        SpriteAsset newSpriteAsset = new SpriteAsset(fileName, filePath, sprite);

        sprites.AddAsset(newSpriteAsset);
        
    }
}
