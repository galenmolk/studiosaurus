using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public abstract class AssetSlot<TAsset> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
        where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] protected TMP_Text fileNameText = null;

        [SerializeField] private Image hoverBox = null;
        [SerializeField] private Image selectionBox = null;
        [SerializeField] private Button deleteButton = null;

        [HideInInspector] public AssetGallery<TAsset> assetGallery;

        public abstract TAsset Asset { get; set; }

        private bool slotSelected;

        public virtual void UpdateSlot(TAsset asset)
        {
            Asset = asset;
            fileNameText.text = asset.assetName;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!slotSelected)
                hoverBox.color = Color.black;

            deleteButton.gameObject.SetActive(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectSlot();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoverBox.color = Color.clear;
            deleteButton.gameObject.SetActive(false);
        }

        public virtual void SelectSlot()
        {
            slotSelected = true;
            hoverBox.color = Color.clear;
            selectionBox.color = Color.white;
            assetGallery.SlotSelected(this);
        }

        public void DeselectSlot()
        {
            slotSelected = false;
            selectionBox.color = Color.clear;
        }

        public void TrashButtonClicked()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
                assetGallery.DeleteSlot(this);
            else
                assetGallery.ConfirmDeleteSlot(this);
        }

        public void FileSlotDoubleClicked()
        {
            SelectSlot();
            assetGallery.ChooseSlot();
        }
    }
}