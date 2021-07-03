using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssetDeletionWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text assetTitleText = null;
    [SerializeField] private Image assetImage = null;
    [SerializeField] private RectTransform assetImageRectTransform;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private AssetSlot fileSlot;
    private Vector2 thumbnailSize;

    private void Awake()
    {
        thumbnailSize = assetImageRectTransform.sizeDelta;
        CloseWindow();
    }

    public void OpenWindow(AssetSlot fileSlot)
    {
        this.fileSlot = fileSlot;
        assetTitleText.text = fileSlot.Asset.assetName;
        assetImage.sprite = fileSlot.Asset.sprite;
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
        AssetSelector.Instance.DeleteSlot(fileSlot);
        CloseWindow();
    }
}
