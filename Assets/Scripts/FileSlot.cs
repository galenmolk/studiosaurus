using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FileSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private TMP_Text fileNameText = null;
    [SerializeField] private Image fileThumbnail = null;
    [SerializeField] private Image slotOutline = null;
    [SerializeField] private Image slotSelectionBox = null;

    private RectTransform thumbnailRT;
    [SerializeField] private Vector2 constrainingSize = new Vector2(115, 115f);

    private void Awake()
    {
        thumbnailRT = fileThumbnail.transform as RectTransform;
    }

    private SpriteAsset spriteAsset;
    private bool slotSelected;

    public void DisplayFile(SpriteAsset spriteAsset)
    {
        gameObject.SetActive(false);
        this.spriteAsset = spriteAsset;
        fileNameText.text = spriteAsset.assetName;
        fileThumbnail.sprite = spriteAsset.sprite;
        fileThumbnail.SetNativeSize();
        ScaleToSlotSize();
        gameObject.SetActive(true);
    }

    private void ScaleToSlotSize()
    {
        float xScaleFactor = 1f, yScaleFactor = 1f;
        Vector2 imageSize = thumbnailRT.sizeDelta;
        if (imageSize.x > constrainingSize.x)
            xScaleFactor = constrainingSize.x / imageSize.x;

        if (imageSize.y > constrainingSize.y)
            yScaleFactor = constrainingSize.y / imageSize.y;

        thumbnailRT.sizeDelta = xScaleFactor < yScaleFactor ? imageSize * xScaleFactor : imageSize * yScaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slotSelected)
            slotOutline.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotOutline.color = Color.clear;
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
}
