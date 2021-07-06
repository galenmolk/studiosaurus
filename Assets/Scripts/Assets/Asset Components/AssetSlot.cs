using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AssetSlot : AssetComponent, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private TMP_Text fileNameText = null;
    [SerializeField] private Image fileThumbnail = null;
    [SerializeField] private Image slotOutline = null;
    [SerializeField] private Image slotSelectionBox = null;
    [SerializeField] private Button deleteButton = null;
    [SerializeField] private Vector2 thumbnailSize = new Vector2(115, 115f);

    private RectTransform thumbnailRectTransform;
    private bool slotSelected;

    private void Awake()
    {
        thumbnailRectTransform = fileThumbnail.transform as RectTransform;
    }

    public override void UpdateAsset(GenericAsset asset)
    {
        base.UpdateAsset(asset);

        if (asset == null)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
        fileNameText.text = asset.assetName;
        fileThumbnail.sprite = asset.sprite;
        fileThumbnail.SetNativeSize();
        Utils.ConstrainRectTransformToSize(thumbnailRectTransform, thumbnailSize);
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slotSelected)
            slotOutline.color = Color.black;

        deleteButton.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectSlot();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotOutline.color = Color.clear;
        deleteButton.gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        slotSelected = true;
        slotOutline.color = Color.clear;
        slotSelectionBox.color = Color.white;
        AssetSelector.Instance.SlotSelected(this);
    }

    public void DeselectSlot()
    {
        slotSelected = false;
        slotSelectionBox.color = Color.clear;
    }

    public void TrashButtonClicked()
    {
        AssetSelector.Instance.ConfirmDeleteSlot(this);
    }

    public void FileSlotDoubleClicked()
    {
        SelectSlot();
        AssetSelector.Instance.ChooseSlot();
    }
}
