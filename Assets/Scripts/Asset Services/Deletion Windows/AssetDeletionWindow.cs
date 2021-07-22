using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class AssetDeletionWindow<TAsset> : MonoBehaviour where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] protected Image assetImage = null;
        [SerializeField] protected RectTransform assetImageRectTransform;

        [SerializeField] private AssetGallery<TAsset> assetGallery;

        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private TMP_Text assetTitleText = null;

        private AssetSlot<TAsset> slot;
        protected Vector2 thumbnailSize;

        protected void Awake()
        {
            thumbnailSize = assetImageRectTransform.sizeDelta;
            CloseWindow();
        }

        public virtual void OpenWindow(AssetSlot<TAsset> slot)
        {
            this.slot = slot;
            assetTitleText.text = slot.Asset.assetName;
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
            assetGallery.DeleteSlot(slot);
            CloseWindow();
        }
    }
}