using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetSlotCollection<TAsset> where TAsset : GenericAsset<TAsset>
    {
        private readonly Dictionary<string, AssetSlot<TAsset>> urlSlotPairs = new Dictionary<string, AssetSlot<TAsset>>();
        private readonly List<AssetSlot<TAsset>> slots = new List<AssetSlot<TAsset>>();

        public int GetSize()
        {
            int dictionaryCount = urlSlotPairs.Count;
            int listCount = slots.Count;

            if (dictionaryCount != listCount)
                Debug.LogWarning("AssetSlotCollection: List and Dictionary are off from each other");

            Debug.Log((dictionaryCount + listCount) / 2);
            return (dictionaryCount + listCount) / 2;
        }

        public bool ContainsSlot(TAsset asset)
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

            if (insertionIndex == -1)
            {
                slots.Add(slot);
                return;
            }
        }

        public void RemoveSlot(AssetSlot<TAsset> slot)
        {
            urlSlotPairs.Remove(slot.Asset.path);
            slots.Remove(slot);
            slot.Asset.ReplaceAssetWith();
        }

        public AssetSlot<TAsset> GetAdjacentSlot(AssetSlot<TAsset> slot)
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
}