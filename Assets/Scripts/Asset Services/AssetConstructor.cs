using System.IO;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetConstructor : MonoBehaviour
    {
        [Header("Asset Galleries")]
        [SerializeField] private AssetGallery<SpriteAsset> sprites = null;
        [SerializeField] private AssetGallery<AudioClipAsset> audioClips = null;

        public void AddNewSprite(Sprite sprite, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == string.Empty)
                fileName = filePath;

            SpriteAsset newSpriteAsset = new SpriteAsset(fileName, filePath, sprite);

            sprites.GetSlot(newSpriteAsset);
        }

        public void AddNewAudioClip(AudioClip audioClip, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == string.Empty)
                fileName = filePath;

            AudioClipAsset newAudioClipAsset = new AudioClipAsset(fileName, filePath, audioClip);

            audioClips.GetSlot(newAudioClipAsset);
        }
    }
}