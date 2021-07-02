using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FileSlot : SpriteAssetObject, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private TMP_Text fileNameText = null;
    [SerializeField] private Image fileThumbnail = null;
    [SerializeField] private Image slotOutline = null;
    [SerializeField] private Image slotSelectionBox = null;
    [SerializeField] private Button deleteButton = null;

    private RectTransform thumbnailRectTransform;
    [SerializeField] private Vector2 thumbnailSize = new Vector2(115, 115f);
    private bool slotSelected;

    private void Awake()
    {
        thumbnailRectTransform = fileThumbnail.transform as RectTransform;
    }

    public override void UpdateSpriteAsset(SpriteAsset spriteAsset)
    {
        if (spriteAsset == null)
        {
            DeleteSpriteAsset();
            return;
        }

        base.UpdateSpriteAsset(spriteAsset);
        DisplayFile();
    }

    public override void DeleteSpriteAsset()
    {
        base.DeleteSpriteAsset();
        Destroy(gameObject);
    }

    private void DisplayFile()
    {
        gameObject.SetActive(false);
        fileNameText.text = spriteAsset.assetName;
        fileThumbnail.sprite = spriteAsset.sprite;
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

    public void OnPointerExit(PointerEventData eventData)
    {
        slotOutline.color = Color.clear;
        deleteButton.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectSlot();
    }

    private void SelectSlot()
    {
        slotSelected = true;
        slotOutline.color = Color.clear;
        slotSelectionBox.color = Color.white;
        FileGallery.Instance.FileSelected(this);
    }

    public void DeselectSlot()
    {
        slotSelected = false;
        slotSelectionBox.color = Color.clear;
    }

    public void TrashButtonClicked()
    {
        FileGallery.Instance.ConfirmDeleteSlot(this);
    }
}
