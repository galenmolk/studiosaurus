using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public class AssetGallery<T> : ScriptableObject where T : GenericAsset<T>
    {
        [SerializeField] private AssetSelector<T> assetSelectorPrefab = null;

        [HideInInspector] public AssetSlot<T> selectedSlot;

        public Dictionary<string, T> assetDictionary = new Dictionary<string, T>();
        public int AssetCount { get { return assetList.Count; } }
        public List<T> AssetList { get { return assetList; } }
        public Dictionary<T, AssetSlot<T>> slotDictionary = new Dictionary<T, AssetSlot<T>>();

        private readonly List<T> assetList = new List<T>();
        private AssetSelector<T> assetSelector;

        public void OpenGallery(AssetComponent<T> assetComponent)
        {
            assetSelector = Instantiate(assetSelectorPrefab, StudioCanvas.Instance.transform);
            assetSelector.Open(this, assetComponent);
        }

        public bool ContainsAsset(T asset)
        {
            return assetDictionary.TryGetValue(asset.path, out _);
        }

        public void AddAsset(T asset)
        {
            if (assetDictionary.ContainsKey(asset.path) && assetList.Contains(asset))
            {
                assetDictionary[asset.path].ReplaceAssetWith(asset);
                assetDictionary.Remove(asset.path);
                Debug.Log("Replacing sprite " + asset.path);
            }

            assetDictionary.Add(asset.path, asset);
            assetList.Add(asset);
            assetSelector.AddSlot(asset).SelectSlot();
        }

        public void DeleteAsset(AssetSlot<T> slot)
        {
            assetDictionary.Remove(slot.Asset.path);
            assetList.Remove(slot.Asset);
            slotDictionary.Remove(slot.Asset);
            slot.Asset.ReplaceAssetWith();
            Destroy(slot.gameObject);
        }

        public T GetAdjacentAsset(T asset)
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
}