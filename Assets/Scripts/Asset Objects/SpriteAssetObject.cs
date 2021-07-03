using UnityEngine;

public abstract class SpriteAssetObject : MonoBehaviour
{
    protected SpriteAsset asset;
    public SpriteAsset Asset { get { return asset; } }

    public virtual void UpdateAsset(SpriteAsset asset)
    {
        asset.assetObjects.Add(this);

        if (this.asset != null)
            this.asset.DisconnectObjectFromAsset(this);

        this.asset = asset;
    }

    public virtual void RemoveAssetFromObject()
    {
        asset = null;
    }
}