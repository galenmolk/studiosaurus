using UnityEngine;
using UnityEngine.Events;

namespace Studiosaurus
{
    public abstract class AssetComponent<T> : ConfigComponent where T : GenericAsset<T>
    {
        [SerializeField] private AssetControls<T> assetControlsPrefab = null;

        public UnityEvent onAssetCleared = new UnityEvent();

        public abstract T Asset { get; protected set; }

        public virtual void AssignAsset(T newAsset = null)
        {
            if (Asset != null)
                Asset.associatedComponents.Remove(this);

            if (newAsset != null)
                newAsset.associatedComponents.Add(this);

            Asset = newAsset;

            if (Asset == null)
                onAssetCleared.Invoke();
        }

        public override void OpenControls(ContextMenu contextMenu)
        {
            AssetControls<T> assetControls = Instantiate(assetControlsPrefab, contextMenu.transform);
            assetControls.assetComponent = this;
        }
    }
}