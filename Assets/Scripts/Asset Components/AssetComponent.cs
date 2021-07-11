using UnityEngine;
using UnityEngine.Events;

namespace Studiosaurus
{
    public class AssetEvent<T> : UnityEvent<T> where T : GenericAsset<T> { }

    public abstract class AssetComponent<T> : ConfigComponent where T : GenericAsset<T>
    {
        [SerializeField] protected AssetControls<T> assetControlsPrefab = null;

        public AssetEvent<T> onAssetAssigned = new AssetEvent<T>();

        public abstract T Asset { get; set; }

        protected AssetControls<T> assetControls;

        public virtual void AssignAsset(T newAsset)
        {
            if (Asset != null)
                Asset.associatedComponents.Remove(this);

            Asset = newAsset;
            onAssetAssigned?.Invoke(newAsset);

            if (newAsset != null)
                newAsset.associatedComponents.Add(this);
        }

        public override ConfigControls OpenControlSection()
        {
            assetControls = Instantiate(assetControlsPrefab);
            assetControls.assetComponent = this;
            return assetControls;
        }
    }
}