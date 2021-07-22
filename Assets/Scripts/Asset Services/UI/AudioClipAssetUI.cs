namespace Studiosaurus
{
    public class AudioClipAssetUI : AssetUI<AudioClipAsset>
    {
        public override void Open(AssetGallery<AudioClipAsset> gallery)
        {
            base.Open(gallery);
            fileUploadService.onUrlReceived.AddListener(AssetLoadService.Instance.LoadAudioClip);
        }
    }
}