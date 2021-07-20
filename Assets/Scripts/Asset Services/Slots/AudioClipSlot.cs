using UnityEngine;

namespace Studiosaurus
{
    public class AudioClipSlot : AssetSlot<AudioClipAsset>
    {
        private AudioSource audioSource;

        public override AudioClipAsset Asset { get; set; }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void UpdateSlot(AudioClipAsset asset)
        {
            base.UpdateSlot(asset);
            audioSource.clip = asset.audioClip;
        }

        public void PlayStopButtonClicked()
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }
}
