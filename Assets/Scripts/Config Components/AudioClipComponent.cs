using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioClipComponent : AssetComponent<AudioClipAsset>
    {
        private AudioSource audioSource;

        public override AudioClipAsset Asset { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        public override void AssignAsset(AudioClipAsset newAsset)
        {
            if (audioSource != null)
                audioSource.clip = newAsset?.audioClip;

            base.AssignAsset(newAsset);
        }

        public void PlayStopAudio()
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }
}