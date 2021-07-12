namespace Studiosaurus
{
    public class SpriteSelector : AssetSelector<SpriteAsset>
    {
        public override void Open(AssetGallery<SpriteAsset> gallery, AssetComponent<SpriteAsset> assetComponent)
        {
            base.Open(gallery, assetComponent);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadSprite);
        }
    }
}
