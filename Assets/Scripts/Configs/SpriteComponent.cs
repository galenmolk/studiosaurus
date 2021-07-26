using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class SpriteComponent : AssetComponent<SpriteAsset>
    {
        private Image image = null;

        protected override void Awake()
        {
            base.Awake();
            image = GetComponentInParent<Image>();
        }

        public override void AssignAsset(SpriteAsset newAsset = null)
        {
            if (image != null)
            {
                image.sprite = newAsset?.sprite;
                image.SetNativeSize();
                doItObject.onNewSpriteAssigned.Invoke();
            }

            base.AssignAsset(newAsset);
        }

        [UnityEngine.ContextMenu("Json")]
        public override string GetComponentAsJSON()
        {
            if (Asset == null)
            {
                Debug.LogWarning("No Sprite Found Assigned to SpriteComponent");
                return string.Empty;
            }

            string path = $"{Application.persistentDataPath}/{GetInstanceID()}.png";
            File.WriteAllBytes(path, Asset.sprite.texture.EncodeToPNG());
            string uploadName = CloudinaryUploader.UploadImage(path);
            Debug.Log(uploadName);
            string json = JsonSerializer.GetAsset(configKey.key, uploadName);
            Debug.Log(json);
            return json;
        }
    }
}
