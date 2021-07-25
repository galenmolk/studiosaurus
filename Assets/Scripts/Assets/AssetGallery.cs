using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public abstract class AssetGallery<TAsset> : Window where TAsset : GenericAsset<TAsset>
    {
        public static AssetGallery<TAsset> Instance;

        [Header("Components")]
        [SerializeField] private Button chooseButton = null;
        [SerializeField] private TMP_Text chooseButtonText = null;
        [SerializeField] private TMP_Text uploadPromptText = null;
        [SerializeField] private RectTransform assetSlotContainer = null;
        [SerializeField] private GalleryConfirmationWindow<TAsset> confirmationWindow = null;

        [SerializeField] private AssetSlot<TAsset> assetSlotPrefab = null;

        private AssetSlot<TAsset> selectedSlot;
        private AssetComponent<TAsset> assetComponent;

        private AssetSlotCollection<TAsset> assetCollection;

        private const string CHOOSE_BUTTON_TEXT = "Choose";
        private const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

        protected override void Awake()
        {
            base.Awake();
            assetCollection = new AssetSlotCollection<TAsset>(assetSlotContainer);
            Instance = this;
        }

        public virtual void Open(AssetComponent<TAsset> assetComponent)
        {
            OpenWindow(transform);
            transform.SetAsLastSibling();
            this.assetComponent = assetComponent;
            UpdateChooseButton(selectedSlot?.Asset.assetName);
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }

        private void UpdateChooseButton(string selectionName = null)
        {
            chooseButtonText.text = CHOOSE_BUTTON_TEXT;
            chooseButton.interactable = selectionName != null;

            if (selectionName != null)
                chooseButtonText.text = $"{chooseButtonText.text}: <b><color={ASSET_NAME_TEXT_COLOR}>{selectionName}</color></b>";
        }

        public void AddAsset(TAsset asset)
        {
            if (assetCollection.Contains(asset))
                return;

            uploadPromptText.gameObject.SetActive(false);
            AssetSlot<TAsset> slot = Instantiate(assetSlotPrefab, assetSlotContainer);
            slot.UpdateSlot(asset);

            assetCollection.AddSlot(asset.path, slot);
            slot.SelectSlot();
        }

        public void DeleteSlot(AssetSlot<TAsset> slot)
        {
            AssetSlot<TAsset> adjacentSlot = assetCollection.GetAdjacentSlot(slot);
            assetCollection.RemoveSlot(slot);

            if (selectedSlot != null && selectedSlot == slot)
            {
                selectedSlot = null;
                UpdateChooseButton();
            }

            Destroy(slot.gameObject);

            if (adjacentSlot != null)
                adjacentSlot.SelectSlot();

            if (assetCollection.GetSize() == 0)
                uploadPromptText.gameObject.SetActive(true);
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
            confirmationWindow.OpenWindow(fileSlot);
        }
    }
}