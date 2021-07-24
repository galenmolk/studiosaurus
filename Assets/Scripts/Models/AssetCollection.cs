using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetSlotCollection<TAsset> where TAsset : GenericAsset<TAsset>
    {
        private readonly Dictionary<string, AssetSlot<TAsset>> urlSlotPairs = new Dictionary<string, AssetSlot<TAsset>>();
        private readonly RectTransform assetSlotContainer;

        public AssetSlotCollection(RectTransform assetSlotContainer)
        {
            this.assetSlotContainer = assetSlotContainer;
        }

        public int GetSize()
        {
            return urlSlotPairs.Count;
        }

        public bool Contains(TAsset asset)
        {
            bool containsSlot = urlSlotPairs.TryGetValue(asset.path, out AssetSlot<TAsset> slot);

            if (containsSlot)
                UpdateSlotAsset(slot, asset);

            return containsSlot;
        }

        private void UpdateSlotAsset(AssetSlot<TAsset> slot, TAsset newAsset)
        {
            TAsset oldAsset = slot.Asset;
            slot.UpdateSlot(newAsset);
            oldAsset.ReplaceAssetWith(newAsset);
            urlSlotPairs.Remove(oldAsset.path);
            urlSlotPairs.Add(newAsset.path, slot);
        }

        public void AddSlot(string url, AssetSlot<TAsset> slot)
        {
            urlSlotPairs.Add(url, slot);
        }

        public void RemoveSlot(AssetSlot<TAsset> slot)
        {
            urlSlotPairs.Remove(slot.Asset.path);
            slot.Asset.ReplaceAssetWith();
        }

        public AssetSlot<TAsset> GetAdjacentSlot(AssetSlot<TAsset> slot)
        {
            AssetSlot<TAsset>[] assetSlots = assetSlotContainer.GetComponentsInChildren<AssetSlot<TAsset>>();
            int numberOfSlots = assetSlots.Length;

            if (numberOfSlots <= 1)
                return null;

            int slotIndex = -1;
            for (int i = 0; i < numberOfSlots; i++)
            {
                if (slot == assetSlots[i])
                {
                    slotIndex = i;
                    break;
                }
            }

            if (slotIndex == -1)
                Debug.LogWarning("Slot not found.");

            if (slotIndex < numberOfSlots - 1)
                return assetSlots[slotIndex + 1];

            if (slotIndex >= 1)
                return assetSlots[slotIndex - 1];

            return null;
        }
    }
}