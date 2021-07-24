namespace Studiosaurus
{
    public class SpriteConfirmationWindow : GalleryConfirmationWindow<SpriteAsset>
    {
        public override void OpenWindow(AssetSlot<SpriteAsset> slot)
        {
            assetImage.sprite = slot.Asset.sprite;
            assetImage.SetNativeSize();
            Utils.ConstrainRectTransformToSize(assetImageRectTransform, thumbnailSize);
            base.OpenWindow(slot);
        }
    }
}
