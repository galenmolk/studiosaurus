using UnityEngine;

namespace Studiosaurus
{
    public abstract class AssetControls<TAsset> : ConfigControls where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] protected AssetGallery<TAsset> assetGallery = null;

        public abstract void OpenGallery();

        public abstract void Initialize(AssetComponent<TAsset> assetComponent);
    }
}