using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class AssetGallery<TAsset> : Window where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] private AssetUI<TAsset> assetUIPrefab = null;
        [SerializeField] protected AssetSlot<TAsset> assetSlotPrefab = null;

        private readonly Dictionary<string, TAsset> assetDictionary = new Dictionary<string, TAsset>();
        private readonly Dictionary<TAsset, AssetSlot<TAsset>> slotDictionary = new Dictionary<TAsset, AssetSlot<TAsset>>();
        private readonly List<TAsset> assetList = new List<TAsset>();
        private readonly List<AssetSlot<TAsset>> slots = new List<AssetSlot<TAsset>>();

        private AssetSlot<TAsset> selectedSlot;
        private TAsset lastSelectedAsset;
        private AssetUI<TAsset> assetUI;
        private AssetComponent<TAsset> assetComponent;
        public AssetDeletionWindow<TAsset> deletionWindow = null;
        [SerializeField] protected Button chooseButton = null;
        [SerializeField] protected TMP_Text chooseButtonText = null;
        [SerializeField] protected FileUploadService fileUploadService = null;
        public Transform assetSlotContainer = null;
        public TMP_Text uploadPromptText = null;

        [HideInInspector] public AssetGallery<TAsset> gallery;

        protected const string CHOOSE_BUTTON_TEXT = "Choose";
        protected const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

        public void UpdateChooseButton(string selectionName = null)
        {
            chooseButtonText.text = CHOOSE_BUTTON_TEXT;
            chooseButton.interactable = selectionName != null;

            if (selectionName != null)
                chooseButtonText.text = $"{chooseButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{selectionName}</color></b>";
        }

        public void Open(AssetComponent<TAsset> assetComponent)
        {
            this.assetComponent = assetComponent;
            assetUI = Instantiate(assetUIPrefab, StudioCanvas.Instance.transform);
            CreateSlots();
            transform.SetAsLastSibling();
            ActivateClosePanel(transform);
            UpdateChooseButton(selectedSlot?.Asset.assetName);
        }

        public override void Close()
        {
            slotDictionary.Clear();

            if (selectedSlot != null)
                lastSelectedAsset = selectedSlot.Asset;
        }

        protected void CreateSlots()
        {
            for (int i = 0, count = assetList.Count; i < count; i++)
            {
                slots.Add(GetSlot(assetList[i]));

                if (assetList[i] == lastSelectedAsset)
                    slots[i].SelectSlot();

                if (i == count - 1 && selectedSlot == null)
                    slots[i].SelectSlot();
            }
        }

        public void AddAsset(TAsset asset)
        {
            if (assetDictionary.ContainsKey(asset.path))
            {
                UpdateAssetWithNewVersion(asset);
                return;
            }

            assetList.Add(asset);
            assetDictionary.Add(asset.path, asset);
            GetSlot(asset).SelectSlot();
        }

        public AssetSlot<TAsset> GetSlot(TAsset asset)
        {
            uploadPromptText.gameObject.SetActive(false);

            if (!slotDictionary.TryGetValue(asset, out AssetSlot<TAsset> slot))
                slot = Instantiate(assetSlotPrefab, assetSlotContainer);

            slot.assetGallery = this;
            slotDictionary.Add(asset, slot);
            slot.UpdateSlot(asset);
            return slot;
        }


        private void UpdateAssetWithNewVersion(TAsset newAsset)
        {
            TAsset oldAsset = assetDictionary[newAsset.path];
            Debug.Log(oldAsset);
            AssetSlot<TAsset> slot = slotDictionary[oldAsset];
            slot.UpdateSlot(newAsset);
            int index = assetList.IndexOf(oldAsset);
            assetList.Remove(oldAsset);
            assetList.Insert(index, newAsset);
            assetDictionary[newAsset.path].ReplaceAssetWith(newAsset);
            assetDictionary.Remove(newAsset.path);
            assetDictionary.Add(newAsset.path, newAsset);
            
            Debug.Log("Replacing sprite " + newAsset.path);
        }

        public void DeleteSlot(AssetSlot<TAsset> slot)
        {
            AssetSlot<TAsset> adjacentSlot = GetAdjacentSlot(slot.Asset);

            Debug.Log("Delete Asset");
            assetDictionary.Remove(slot.Asset.path);
            assetList.Remove(slot.Asset);
            slotDictionary.Remove(slot.Asset);
            slot.Asset.ReplaceAssetWith();

            if (selectedSlot != null && selectedSlot == slot)
            {
                selectedSlot = null;
                UpdateChooseButton();
            }

            Destroy(slot.gameObject);

            if (adjacentSlot != null)
                adjacentSlot.SelectSlot();

            if (assetList.Count == 0)
                uploadPromptText.gameObject.SetActive(true);
        }

        public AssetSlot<TAsset> GetAdjacentSlot(TAsset asset)
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

        public void ChooseSlot()
        {
            if (assetComponent != null)
                assetComponent.AssignAsset(selectedSlot != null ? selectedSlot.Asset : null);

            Close();
        }

        public void SlotSelected(AssetSlot<TAsset> assetSlot)
        {
            if (selectedSlot != null && selectedSlot != assetSlot)
                selectedSlot.DeselectSlot();

            selectedSlot = assetSlot;
            UpdateChooseButton(assetSlot.Asset.assetName);
        }

        public void ConfirmDeleteSlot(AssetSlot<TAsset> fileSlot)
        {
            deletionWindow.OpenWindow(fileSlot);
        }
    }
}