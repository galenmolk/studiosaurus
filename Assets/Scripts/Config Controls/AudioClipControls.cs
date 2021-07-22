namespace Studiosaurus
{
    public class AudioClipControls : AssetControls<AudioClipAsset>
    {
        private AudioClipComponent audioClipComponent = null;

        public override void Initialize(AssetComponent<AudioClipAsset> audioClipComponent)
        {
            this.audioClipComponent = (AudioClipComponent)audioClipComponent;
        }

        public override void OpenGallery()
        {
            assetGallery.Open(audioClipComponent);
        }

        public void PlayStopAudio()
        {
            audioClipComponent.PlayStopAudio();
        }
    }
}
