using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioClipComponent : AssetComponent<AudioClipAsset>
    {
        private AudioSource audioSource;

        public override AudioClipAsset Asset { get; set; }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void AssignAsset(AudioClipAsset newAsset)
        {
            base.AssignAsset(newAsset);

            audioSource.clip = newAsset?.audioClip;
        }
    }
}