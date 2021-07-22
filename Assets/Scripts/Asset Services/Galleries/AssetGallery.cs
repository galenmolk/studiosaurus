using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public abstract class AssetGallery<TAsset> : Window where TAsset : GenericAsset<TAsset>
    {
        public static AssetGallery<TAsset> Instance;

        [SerializeField] protected FileUploadService fileUploadService = null;

        [Header("Components")]
        [SerializeField] private Button chooseButton = null;
        [SerializeField] private TMP_Text chooseButtonText = null;
        [SerializeField] private TMP_Text uploadPromptText = null;
        [SerializeField] private Transform assetSlotContainer = null;
        [SerializeField] private AssetDeletionWindow<TAsset> deletionWindow = null;

        [SerializeField] private AssetSlot<TAsset> assetSlotPrefab = null;

        private AssetSlot<TAsset> selectedSlot;
        private AssetComponent<TAsset> assetComponent;

        private readonly AssetSlotCollection<TAsset> assetCollection = new AssetSlotCollection<TAsset>();

        private const string CHOOSE_BUTTON_TEXT = "Choose";
        private const string ASSET_NAME_TEXT_COLOR = "#c0c0c0ff";

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public virtual void Open(AssetComponent<TAsset> assetComponent)
        {
            transform.SetAsLastSibling();
            this.assetComponent = assetComponent;
            base.Open(transform);
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

        public override void Close()
        {
            Utils.SetCanvasGroupEnabled(canvasGroup, false);
            base.Close();
        }

        public void AddAsset(TAsset asset)
        {
            //if (assetDictionary.ContainsKey(asset.path))
            //{
            //    UpdateAssetWithNewVersion(asset);
            //    return;
            //}
            uploadPromptText.gameObject.SetActive(false);
            AssetSlot<TAsset> slot = Instantiate(assetSlotPrefab, assetSlotContainer);
            slot.UpdateSlot(asset);

            assetCollection.AddSlot(asset.path, slot);
            slot.SelectSlot();
        }

        //private void UpdateAssetWithNewVersion(TAsset newAsset)
        //{
        //    TAsset oldAsset = assetDictionary[newAsset.path];
        //    AssetSlot<TAsset> slot = slotDictionary[oldAsset];
        //    slot.UpdateSlot(newAsset);
        //    int index = assetList.IndexOf(oldAsset);
        //    assetList.Remove(oldAsset);
        //    assetList.Insert(index, newAsset);
        //    assetDictionary[newAsset.path].ReplaceAssetWith(newAsset);
        //    assetDictionary.Remove(newAsset.path);
        //    assetDictionary.Add(newAsset.path, newAsset);
        //}

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
            deletionWindow.OpenWindow(fileSlot);
        }
    }
}