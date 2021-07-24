namespace Studiosaurus
{
    public class SpriteGallery : AssetGallery<SpriteAsset>
    {
        public override void Open(AssetComponent<SpriteAsset> assetComponent)
        {
            base.Open(assetComponent);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadSprite);
        }
    }
}
