using UnityEngine;

public abstract class AssetComponent : MonoBehaviour
{
    private GenericAsset asset;
    public GenericAsset Asset { get { return asset; } }

    public virtual void UpdateAsset(GenericAsset asset)
    {
        asset.assetComponents.Add(this);

        if (asset != null)
            asset.SeverComponentAssetConnection(this);

        this.asset = asset;
    }

    public void RemoveAssetFromObject()
    {
        asset = null;
    }
}
