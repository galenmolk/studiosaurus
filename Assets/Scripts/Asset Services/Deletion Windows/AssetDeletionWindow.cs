using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class AssetDeletionWindow<T> : MonoBehaviour where T : GenericAsset<T>
    {
        [SerializeField] protected Image assetImage = null;
        [SerializeField] protected RectTransform assetImageRectTransform;
        [SerializeField] private AssetSelector<T> assetSelector = null;

        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private TMP_Text assetTitleText = null;

        private AssetSlot<T> slot;
        protected Vector2 thumbnailSize;

        protected void Awake()
        {
            thumbnailSize = assetImageRectTransform.sizeDelta;
            CloseWindow();
        }

        public virtual void OpenWindow(AssetSlot<T> slot)
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
            assetSelector.DeleteSlot(slot);
            CloseWindow();
        }
    }
}