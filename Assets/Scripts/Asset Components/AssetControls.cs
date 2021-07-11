using UnityEngine;

namespace Studiosaurus
{
    public class AssetControls<T> : ConfigControls where T : GenericAsset<T>
    {
        [SerializeField] private AssetGallery<T> assetGallery = null;

        [HideInInspector] public AssetComponent<T> assetComponent;

        public void OpenGallery()
        {
            assetGallery.OpenGallery();
        }
    }
}