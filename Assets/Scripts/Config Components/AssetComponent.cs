using UnityEngine;

namespace Studiosaurus
{
    public abstract class AssetComponent<T> : ConfigComponent where T : GenericAsset<T>
    {
        [SerializeField] private AssetControls<T> assetControlsPrefab = null;

        public AssetEvent<T> onAssetAssigned = new AssetEvent<T>();

        public abstract T Asset { get; protected set; }

        public virtual void AssignAsset(T newAsset)
        {
            if (Asset != null)
                Asset.associatedComponents.Remove(this);

            Asset = newAsset;
            onAssetAssigned?.Invoke(newAsset);

            if (newAsset != null)
                newAsset.associatedComponents.Add(this);
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            AssetControls<T> assetControls = Instantiate(assetControlsPrefab, contextMenu.transform);
            assetControls.assetComponent = this;
        }
    }
}