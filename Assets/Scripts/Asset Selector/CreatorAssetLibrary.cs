using System.IO;
using UnityEngine;

namespace Studiosaurus
{
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

        [SerializeField] private Sprite audioClipThumbnail = null;

        public AssetGallery<SpriteAsset> sprites;
        public AssetGallery<AudioClipAsset> audioClips;

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

        public void AddNewAudioClip(AudioClip audioClip, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == string.Empty)
                fileName = filePath;

            AudioClipAsset newAudioClipAsset = new AudioClipAsset(fileName, filePath, audioClip);

            audioClips.AddAsset(newAudioClipAsset);
        }
    }
}