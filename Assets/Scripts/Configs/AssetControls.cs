using UnityEngine;

namespace Studiosaurus
{
    public abstract class AssetControls<T> : ConfigControls where T : GenericAsset<T>
    {
        [SerializeField] protected AssetGallery<T> assetGallery = null;

        public abstract void OpenGallery();

        public abstract void Initialize(AssetComponent<T> assetComponent);
    }
}