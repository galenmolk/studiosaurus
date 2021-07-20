using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetGallery<T> : ScriptableObject where T : GenericAsset<T>
    {
        [SerializeField] private AssetSelector<T> assetSelectorPrefab = null;

        [HideInInspector] public AssetSlot<T> selectedSlot;
        public int AssetCount { get { return assetList.Count; } }
        public List<T> AssetList { get { return assetList; } }
        public T lastSelectedAsset;

        private readonly Dictionary<string, T> assetDictionary = new Dictionary<string, T>();
        private readonly Dictionary<T, AssetSlot<T>> slotDictionary = new Dictionary<T, AssetSlot<T>>();
        private readonly List<T> assetList = new List<T>();

        private AssetSelector<T> assetSelector;

        public void OpenGallery(AssetComponent<T> assetComponent)
        {
            assetSelector = Instantiate(assetSelectorPrefab, StudioCanvas.Instance.transform);
            assetSelector.Open(this, assetComponent);
        }

        public void Close()
        {
            slotDictionary.Clear();

            if (selectedSlot != null)
                lastSelectedAsset = selectedSlot.Asset;
        }

        public void AddAsset(T asset)
        {
            if (assetDictionary.ContainsKey(asset.path))
            {
                UpdateAssetWithNewVersion(asset);
                return;
            }

            assetList.Add(asset);
            assetDictionary.Add(asset.path, asset);
            assetSelector.AddAsset(asset).SelectSlot();
        }

        public void AddSlot(T asset, AssetSlot<T> slot)
        {
            slotDictionary.Add(asset, slot);
        }

        public bool SlotExists(T asset, out AssetSlot<T> slot)
        {
            return slotDictionary.TryGetValue(asset, out slot);
        }

        private void UpdateAssetWithNewVersion(T newAsset)
        {
            T oldAsset = assetDictionary[newAsset.path];
            Debug.Log(oldAsset);
            AssetSlot<T> slot = slotDictionary[oldAsset];
            slot.UpdateSlot(newAsset);
            int index = assetList.IndexOf(oldAsset);
            assetList.Remove(oldAsset);
            assetList.Insert(index, newAsset);
            assetDictionary[newAsset.path].ReplaceAssetWith(newAsset);
            assetDictionary.Remove(newAsset.path);
            assetDictionary.Add(newAsset.path, newAsset);
            
            Debug.Log("Replacing sprite " + newAsset.path);
        }

        public void DeleteAsset(T asset)
        {
            Debug.Log("Delete Asset");
            assetDictionary.Remove(asset.path);
            assetList.Remove(asset);
            slotDictionary.Remove(asset);
            asset.ReplaceAssetWith();
        }

        public AssetSlot<T> GetAdjacentSlot(T asset)
        {
            int slotCount = assetList.Count;

            if (slotCount <= 1)
                return null;

            int index = assetList.IndexOf(asset);

            if (index < slotCount - 1)
                return slotDictionary[assetList[index + 1]];

            if (index >= 1)
                return slotDictionary[assetList[index - 1]];

            return null;
        }
    }
}