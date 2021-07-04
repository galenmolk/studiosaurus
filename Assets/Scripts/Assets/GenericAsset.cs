using System.Collections.Generic;

public class GenericAsset
{
    public GenericAsset(string assetName, string path)
    {
        this.assetName = assetName;
        this.path = path;
    }

    public string assetName;
    public string path;

    public List<AssetComponent> assetComponents = new List<AssetComponent>();
    
    public void SeverComponentAssetConnection(AssetComponent assetComponent)
    {
        if (assetComponents.Contains(assetComponent))
            assetComponents.Remove(assetComponent);
    }
}
