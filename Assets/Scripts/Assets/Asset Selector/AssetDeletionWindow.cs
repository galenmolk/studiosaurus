using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssetDeletionWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text assetTitleText = null;
    [SerializeField] private Image assetImage = null;
    [SerializeField] private RectTransform assetImageRectTransform;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private AssetSlot slot;
    private Vector2 thumbnailSize;

    private void Awake()
    {
        thumbnailSize = assetImageRectTransform.sizeDelta;
        CloseWindow();
    }

    public void OpenWindow(AssetSlot slot)
    {
        this.slot = slot;
        assetTitleText.text = slot.Asset.assetName;
        assetImage.sprite = slot.Asset.sprite;
        assetImage.SetNativeSize();
        Utils.ConstrainRectTransformToSize(assetImageRectTransform, thumbnailSize);
        Utils.SetCanvasGroupEnabled(canvasGroup, true);
    }

    public void CloseWindow()
    {
        Utils.SetCanvasGroupEnabled(canvasGroup, false);
        slot = null;
        assetImage.sprite = null;
        assetTitleText.text = null;
    }

    public void DeleteAsset()
    {
        AssetSelector.Instance.DeleteSlot(slot);
        CloseWindow();
    }
}
