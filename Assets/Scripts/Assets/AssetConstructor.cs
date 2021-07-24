using System.IO;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetConstructor : MonoBehaviour
    {
        public void AddNewSprite(Sprite sprite, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == string.Empty)
                fileName = filePath;

            SpriteAsset newSpriteAsset = new SpriteAsset(fileName, filePath, sprite);

            SpriteGallery.Instance.AddAsset(newSpriteAsset);
        }

        public void AddNewAudioClip(AudioClip audioClip, string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == string.Empty)
                fileName = filePath;

            AudioClipAsset newAudioClipAsset = new AudioClipAsset(fileName, filePath, audioClip);

            AudioClipGallery.Instance.AddAsset(newAudioClipAsset);
        }
    }
}