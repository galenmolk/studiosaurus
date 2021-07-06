using UnityEngine;

public abstract class AssetComponent : MonoBehaviour
{
    public GenericAsset Asset { get { return asset; } }
    protected GenericAsset asset;

    public virtual void UpdateAsset(GenericAsset newAsset)
    {
        if (newAsset == null && asset != null)
        {
            asset.assetComponents.Remove(this);
            asset = null;
            return;
        }

        Debug.Log("Adding comp " + name);
        asset = newAsset;
        newAsset.assetComponents.Add(this);
    }
}
