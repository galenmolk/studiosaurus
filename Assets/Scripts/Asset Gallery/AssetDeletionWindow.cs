using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssetDeletionWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text assetTitleText = null;
    [SerializeField] private Image assetImage = null;
    [SerializeField] private RectTransform assetImageRectTransform;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private FileSlot fileSlot;
    private Vector2 thumbnailSize;

    private void Awake()
    {
        thumbnailSize = assetImageRectTransform.sizeDelta;
        CloseWindow();
    }

    public void OpenWindow(FileSlot fileSlot)
    {
        this.fileSlot = fileSlot;
        assetTitleText.text = fileSlot.SpriteAsset.assetName;
        assetImage.sprite = fileSlot.SpriteAsset.sprite;
        assetImage.SetNativeSize();
        Utils.ConstrainRectTransformToSize(assetImageRectTransform, thumbnailSize);
        Utils.SetCanvasGroupEnabled(canvasGroup, true);
    }

    public void CloseWindow()
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, false);
        fileSlot = null;
        assetImage.sprite = null;
        assetTitleText.text = null;
    }

    public void DeleteAsset()
    {
        FileGallery.Instance.DeleteSlot(fileSlot);
        CloseWindow();
    }
}
