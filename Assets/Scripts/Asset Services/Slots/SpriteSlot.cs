namespace Studiosaurus
{
    public class SpriteSlot : AssetSlot<SpriteAsset>
    {
        public override void UpdateAsset(SpriteAsset asset)
        {
            base.UpdateAsset(asset);
            fileThumbnail.sprite = asset.sprite;
            fileThumbnail.SetNativeSize();
            Utils.ConstrainRectTransformToSize(thumbnailRectTransform, thumbnailSize);
        }
    }
}