namespace Studiosaurus
{
    public class AudioClipSelector : AssetSelector<AudioClipAsset>
    {
        public override void Open(AssetGallery<AudioClipAsset> gallery, AssetComponent<AudioClipAsset> assetComponent)
        {
            base.Open(gallery, assetComponent);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadAudioClip);
        }
    }
}