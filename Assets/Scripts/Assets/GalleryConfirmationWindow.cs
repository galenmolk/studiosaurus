using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class GalleryConfirmationWindow<TAsset> : Window where TAsset : GenericAsset<TAsset>
    {
        [SerializeField] private AssetGallery<TAsset> assetGallery;

        [Space, Header("Components")]
        [SerializeField] protected Image assetImage = null;
        [SerializeField] protected RectTransform assetImageRectTransform;
        [SerializeField] private TMP_Text assetTitleText = null;

        private AssetSlot<TAsset> slot;
        protected Vector2 thumbnailSize;

        protected override void Awake()
        {
            base.Awake();
            thumbnailSize = assetImageRectTransform.sizeDelta;
        }

        public virtual void OpenWindow(AssetSlot<TAsset> slot)
        {
            OpenWindow(transform);
            this.slot = slot;
            assetTitleText.text = slot.Asset.assetName;
            Utils.SetCanvasGroupEnabled(canvasGroup, true);
        }

        public void DeleteAsset()
        {
            assetGallery.DeleteSlot(slot);
            Close();
        }
    }
}