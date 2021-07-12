namespace Studiosaurus
{
    public class SpriteSlot : AssetSlot<SpriteAsset>
    {
        public override void UpdateSlot(SpriteAsset asset)
        {
            base.UpdateSlot(asset);
            Utils.ConstrainRectTransformToSize(thumbnailRectTransform, thumbnailSize);
        }
    }
}