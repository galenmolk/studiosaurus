namespace Studiosaurus
{
    public class SpriteAssetUI : AssetUI<SpriteAsset>
    {
        public override void Open(AssetGallery<SpriteAsset> gallery)
        {
            base.Open(gallery);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadSprite);
        }
    }
}
