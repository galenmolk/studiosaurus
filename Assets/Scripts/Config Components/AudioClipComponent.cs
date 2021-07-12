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
            base.AssignAsset(newAsset);
            audioSource.clip = newAsset.audioClip;
        }

        public void PlayAudio()
        {
            audioSource.Play();
        }
    }
}