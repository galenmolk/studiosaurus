namespace Studiosaurus
{
    public class AudioClipGallery : AssetGallery<AudioClipAsset>
    {
        public override void Open(AssetComponent<AudioClipAsset> assetComponent)
        {
            base.Open(assetComponent);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadAudioClip);
        }
    }
}