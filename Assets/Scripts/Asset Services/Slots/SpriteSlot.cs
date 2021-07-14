using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class SpriteSlot : AssetSlot<SpriteAsset>
    {
        [SerializeField] private Image fileThumbnail = null;

        public override SpriteAsset Asset { get; set; }

        private readonly Vector2 thumbnailSize = new Vector2(115, 115f);
        private RectTransform thumbnailRectTransform;

        private void Awake()
        {
            thumbnailRectTransform = fileThumbnail.rectTransform;
        }

        public override void UpdateSlot(SpriteAsset asset)
        {
            base.UpdateSlot(asset);
            fileThumbnail.sprite = asset.sprite;
            fileThumbnail.SetNativeSize();
            Utils.ConstrainRectTransformToSize(thumbnailRectTransform, thumbnailSize);
        }
    }
}