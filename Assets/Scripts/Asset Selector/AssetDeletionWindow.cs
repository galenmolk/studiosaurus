using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{


    public class AssetDeletionWindow<T> : MonoBehaviour where T : GenericAsset<T>
    {
        [SerializeField] protected TMP_Text assetTitleText = null;
        [SerializeField] protected Image assetImage = null;
        [SerializeField] protected RectTransform assetImageRectTransform;
        [SerializeField] protected CanvasGroup canvasGroup = null;

        protected AssetSlot<T> slot;
        protected AssetSelector<T> assetSelector;
        protected Vector2 thumbnailSize;

        protected void Awake()
        {
            thumbnailSize = assetImageRectTransform.sizeDelta;
            CloseWindow();
        }

        public void OpenWindow(AssetSlot<T> slot, AssetSelector<T> assetSelector)
        {
            this.slot = slot;
            this.assetSelector = assetSelector;
            assetTitleText.text = slot.Asset.assetName;
            assetImage.sprite = slot.GetThumbnail();
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
            assetSelector.DeleteSlot(slot);
            CloseWindow();
        }
    }
}