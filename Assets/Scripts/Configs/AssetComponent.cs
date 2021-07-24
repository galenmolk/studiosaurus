using UnityEngine;

namespace Studiosaurus
{
    public abstract class AssetComponent<TAsset> : ConfigComponent where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] private AssetControls<TAsset> assetControlsPrefab = null;

        public AssetEvent<TAsset> onAssetAssigned = new AssetEvent<TAsset>();

        public abstract TAsset Asset { get; protected set; }

        public virtual void AssignAsset(TAsset newAsset = null)
        {
            if (Asset != null)
                Asset.associatedComponents.Remove(this);

            if (newAsset != null && !newAsset.associatedComponents.Contains(this))
                newAsset.associatedComponents.Add(this);

            Asset = newAsset;

            onAssetAssigned.Invoke(Asset);
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            Instantiate(assetControlsPrefab, contextMenu.transform).Initialize(this);
        }
    }
}
