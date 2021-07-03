using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset Slot Gallery", menuName = "New Asset Slot Gallery")]
public class AssetSlotGallery : ScriptableObject
{
    private readonly Dictionary<string, AssetSlot> assetSlotPairs = new Dictionary<string, AssetSlot>();
    private readonly List<AssetSlot> slots = new List<AssetSlot>();
    public List<AssetSlot> Slots { get { return slots; } }

    public int SlotCount
    {
        get
        {
            int listCount = slots.Count;
            int dictCount = assetSlotPairs.Count;
            if (listCount != dictCount)
                Debug.LogWarning("AssetSlotGallery List/Dictionary are out of sync!");

            return listCount;
        }
    }

    public bool Contains(SpriteAsset asset, out AssetSlot slot)
    {
        return assetSlotPairs.TryGetValue(asset.path, out slot) && slots.Contains(slot);
    }

    public void AddSlot(SpriteAsset asset, AssetSlot slot)
    {
        assetSlotPairs.Add(asset.path, slot);
        slots.Add(slot);
    }

    public void DeleteSlot(AssetSlot assetSlot)
    {
        assetSlotPairs.Remove(assetSlot.Asset.path);
        slots.Remove(assetSlot);
    }

    public AssetSlot GetAdjacentSlot(AssetSlot slot)
    {
        int slotCount = slots.Count;

        if (slotCount <= 1)
            return null;

        int index = slots.IndexOf(slot);

        if (index < slotCount - 1)
            return slots[index + 1];

        if (index >= 1)
            return slots[index - 1];
    
        return null;
    }
}
