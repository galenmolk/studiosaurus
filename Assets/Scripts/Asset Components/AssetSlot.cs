using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Studiosaurus
{
    public class AssetSlot<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler where T : GenericAsset<T>
    {
        [SerializeField] private TMP_Text fileNameText = null;
        [SerializeField] private Image fileThumbnail = null;
        [SerializeField] private RectTransform thumbnailRectTransform = null;
        [SerializeField] private Image hoverBox = null;
        [SerializeField] private Image selectionBox = null;
        [SerializeField] private Button deleteButton = null;
        [SerializeField] private Vector2 thumbnailSize = new Vector2(115, 115f);
        [SerializeField] private AssetComponent<T> assetComponent = null;

        public T Asset { get { return assetComponent.Asset; } }

        [HideInInspector] public AssetSelector<T> assetSelector;

        private bool slotSelected;
         
        private void Awake()
        {
            assetComponent.onAssetAssigned.AddListener(UpdateAsset);
        }

        public void UpdateAsset(T asset)
        {
            if (asset == null)
            {
                Destroy(gameObject);
                return;
            }

            fileNameText.text = asset.assetName;
            fileThumbnail.sprite = GetThumbnail();
            fileThumbnail.SetNativeSize();
            Utils.ConstrainRectTransformToSize(thumbnailRectTransform, thumbnailSize);
        }

        public Sprite GetThumbnail()
        {
            return (Asset as SpriteAsset).sprite ?? assetSelector.gallery.icon;
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