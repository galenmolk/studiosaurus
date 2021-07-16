using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class AssetSelector<TAsset> : Window where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] protected AssetSlot<TAsset> assetSlotPrefab = null;
        [SerializeField] protected TMP_Text uploadPromptText = null;
        [SerializeField] protected AssetDeletionWindow<TAsset> deletionWindow = null;
        [SerializeField] protected Button chooseButton = null;
        [SerializeField] protected TMP_Text chooseButtonText = null;
        [SerializeField] protected Transform assetSlotContainer = null;
        [SerializeField] protected FileUploadService fileUploadService = null;

        protected AssetComponent<TAsset> assetComponent;
        [HideInInspector] public AssetGallery<TAsset> gallery;

        protected const string CHOOSE_BUTTON_TEXT = "Choose";
        protected const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

        private List<AssetSlot<TAsset>> openSlots = new List<AssetSlot<TAsset>>(); 

        public virtual void Open(AssetGallery<TAsset> gallery, AssetComponent<TAsset> assetComponent)
        {
            transform.SetAsLastSibling();
            ActivateClosePanel(transform);
            this.assetComponent = assetComponent;
            this.gallery = gallery;
            CreateSlots();
        }

        public override void Close()
        {
            gallery.Close();
            base.Close();
        }

        protected void CreateSlots()
        {
            for (int i = 0, count = gallery.AssetList.Count; i < count; i++)
            {
                openSlots.Add(CreateSlot(gallery.AssetList[i]));

                if (gallery.AssetList[i] == gallery.lastSelectedAsset)
                    openSlots[i].SelectSlot();
                
                if (i == count - 1 && gallery.selectedSlot == null)
                    openSlots[i].SelectSlot();
            }
            UpdateChooseButton();
        }

        public AssetSlot<TAsset> CreateSlot(TAsset asset)
        {
            uploadPromptText.gameObject.SetActive(true);

            if (!gallery.slotDictionary.TryGetValue(asset, out AssetSlot<TAsset> slot))
            {
                slot = Instantiate(assetSlotPrefab, assetSlotContainer);
                gallery.slotDictionary.Add(asset, slot);
            }

            slot.assetSelector = this;
            slot.UpdateSlot(asset);
            return slot;
        }

        public void SlotSelected(AssetSlot<TAsset> assetSlot)
        {
            if (gallery.selectedSlot != null && gallery.selectedSlot != assetSlot)
                gallery.selectedSlot.DeselectSlot();

            gallery.selectedSlot = assetSlot;
            UpdateChooseButton();
        }

        public void ChooseSlot()
        {
            if (assetComponent != null)
                assetComponent.AssignAsset(gallery.selectedSlot != null ? gallery.selectedSlot.Asset : null);

            Close();
        }

        public void ConfirmDeleteSlot(AssetSlot<TAsset> fileSlot)
        {
            deletionWindow.OpenWindow(fileSlot);
        }

        public void DeleteSlot(AssetSlot<TAsset> assetSlot)
        {
            TAsset adjacentAsset = gallery.GetAdjacentAsset(assetSlot.Asset);
            gallery.DeleteAsset(assetSlot.Asset);

            if (gallery.selectedSlot != null && gallery.selectedSlot == assetSlot)
            {
                gallery.selectedSlot = null;
                UpdateChooseButton();
            }

            Destroy(assetSlot.gameObject);

            if (adjacentAsset != null && gallery.slotDictionary.TryGetValue(adjacentAsset, out AssetSlot<TAsset> newSlot))
                newSlot.SelectSlot();

            if (gallery.AssetCount == 0)
                uploadPromptText.gameObject.SetActive(true);
        }

        protected void UpdateChooseButton()
        {
            chooseButtonText.text = CHOOSE_BUTTON_TEXT;
            chooseButton.interactable = gallery != null && gallery.selectedSlot != null;

            if (gallery != null && gallery.selectedSlot != null)
                chooseButtonText.text = $"{chooseButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{gallery.selectedSlot.Asset.assetName}</color></b>";
        }
    }
}