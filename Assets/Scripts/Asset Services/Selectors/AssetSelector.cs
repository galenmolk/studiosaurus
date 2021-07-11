using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class AssetSelector<T> : MonoBehaviour where T : GenericAsset<T>
    {
        [SerializeField] protected AssetSlot<T> assetSlotPrefab = null;
        [SerializeField] protected TMP_Text uploadPromptText = null;
        [SerializeField] protected AssetDeletionWindow<T> deletionWindow = null;
        [SerializeField] protected Button chooseButton = null;
        [SerializeField] protected TMP_Text chooseButtonText = null;
        [SerializeField] protected Transform assetSlotContainer = null;

        protected AssetComponent<T> assetComponent;
        [HideInInspector] public AssetGallery<T> gallery;

        protected const string CHOOSE_BUTTON_TEXT = "Choose";
        protected const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

        public void Open(AssetGallery<T> gallery, AssetComponent<T> assetComponent)
        {
            this.assetComponent = assetComponent;
            this.gallery = gallery;
            CreateSlots();
            transform.SetAsLastSibling();
        }

        public void Close()
        {
            gallery.slotDictionary.Clear();
            Destroy(gameObject);
        }

        protected void CreateSlots()
        {
            for (int i = 0, count = gallery.AssetList.Count; i < count; i++)
            {
                AssetSlot<T> slot = AddSlot(gallery.AssetList[i]);

                if (i == count - 1)
                    slot.SelectSlot();
            }
        }

        public AssetSlot<T> AddSlot(T asset)
        {
            uploadPromptText.gameObject.SetActive(true);

            if (!gallery.slotDictionary.TryGetValue(asset, out AssetSlot<T> slot))
            {
                slot = Instantiate(assetSlotPrefab, assetSlotContainer);
                gallery.slotDictionary.Add(asset, slot);
            }

            slot.assetSelector = this;
            slot.UpdateAsset(asset);
            return slot;
        }

        public void SlotSelected(AssetSlot<T> assetSlot)
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

        public void ConfirmDeleteSlot(AssetSlot<T> fileSlot)
        {
            deletionWindow.OpenWindow(fileSlot);
        }

        public void DeleteSlot(AssetSlot<T> assetSlot)
        {
            T adjacentAsset = gallery.GetAdjacentAsset(assetSlot.Asset);
            gallery.DeleteAsset(assetSlot);

            if (gallery.selectedSlot != null && gallery.selectedSlot == assetSlot)
            {
                gallery.selectedSlot = null;
                UpdateChooseButton();
            }

            if (adjacentAsset != null && gallery.slotDictionary.TryGetValue(adjacentAsset, out AssetSlot<T> newSlot))
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