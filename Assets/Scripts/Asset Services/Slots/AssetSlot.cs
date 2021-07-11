using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public abstract class AssetSlot<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler where T : GenericAsset<T>
    {
        [SerializeField] protected TMP_Text fileNameText = null;
        [SerializeField] protected Image fileThumbnail = null;
        [SerializeField] protected RectTransform thumbnailRectTransform = null;
        [SerializeField] protected Vector2 thumbnailSize = new Vector2(115, 115f);

        [SerializeField] private Image hoverBox = null;
        [SerializeField] private Image selectionBox = null;
        [SerializeField] private Button deleteButton = null;
        [SerializeField] private AssetComponent<T> assetComponent = null;

        [HideInInspector] public AssetSelector<T> assetSelector;

        public T Asset { get { return asset; } }
        private T asset;

        private bool slotSelected;
         
        private void Awake()
        {
            assetComponent.onAssetAssigned.AddListener(UpdateAsset);
        }

        public virtual void UpdateAsset(T asset)
        {
            this.asset = asset;
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

        public void SelectSlot()
        {
            slotSelected = true;
            hoverBox.color = Color.clear;
            selectionBox.color = Color.white;
            assetSelector.SlotSelected(this);
        }

        public void DeselectSlot()
        {
            slotSelected = false;
            selectionBox.color = Color.clear;
        }

        public void TrashButtonClicked()
        {
            assetSelector.ConfirmDeleteSlot(this);
        }

        public void FileSlotDoubleClicked()
        {
            SelectSlot();
            assetSelector.ChooseSlot();
        }
    }
}