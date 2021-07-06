using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset Gallery", menuName = "New Asset Gallery")]
public class AssetGallery : ScriptableObject
{
    public Dictionary<string, GenericAsset> assetDictionary = new Dictionary<string, GenericAsset>();
    public int AssetCount { get { return assetList.Count; } }
    public List<GenericAsset> AssetList { get { return assetList; } }
    public Dictionary<GenericAsset, AssetSlot> slotDictionary = new Dictionary<GenericAsset, AssetSlot>();

    private readonly List<GenericAsset> assetList = new List<GenericAsset>();

    public bool ContainsAsset(GenericAsset asset)
    {
        return assetDictionary.TryGetValue(asset.path, out _);
    }

    public void AddAsset(GenericAsset asset)
    {
        if (assetDictionary.ContainsKey(asset.path) && assetList.Contains(asset))
        {
            assetDictionary[asset.path].ReplaceAssetWith(asset);
            assetDictionary.Remove(asset.path);
            Debug.Log("Replacing sprite " + asset.path);
        }

        assetDictionary.Add(asset.path, asset);
        assetList.Add(asset);
        AssetSelector.Instance.AddSlot(asset);
    }

    public void DeleteAsset(AssetSlot slot)
    {
        assetDictionary.Remove(slot.Asset.path);
        assetList.Remove(slot.Asset);
        slotDictionary.Remove(slot.Asset);
        slot.Asset.ReplaceAssetWith();
    }

    public GenericAsset GetAdjacentAsset(GenericAsset asset)
    {
        int slotCount = assetList.Count;

        if (slotCount <= 1)
            return null;

        int index = assetList.IndexOf(asset);

        if (index < slotCount - 1)
            return assetList[index + 1];

        if (index >= 1)
            return assetList[index - 1];

        return null;
    }
}
