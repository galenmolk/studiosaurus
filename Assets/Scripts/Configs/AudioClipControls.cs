using UnityEngine;

namespace Studiosaurus
{
    public class AudioClipControls : AssetControls<AudioClipAsset>
    {
        [SerializeField] private AudioToggleButton audioToggle = null;

        private AudioClipComponent audioClipComponent;

        public override void Initialize(AssetComponent<AudioClipAsset> audioClipComponent)
        {
            this.audioClipComponent = (AudioClipComponent)audioClipComponent;
            audioToggle.audioSource = this.audioClipComponent.audioSource;
        }

        public override void OpenGallery()
        {
            AudioClipGallery.Instance.Open(audioClipComponent);
        }
    }
}
