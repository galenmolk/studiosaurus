using UnityEngine;
using UnityEngine.EventSystems;

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

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            audioSource.Play();
        }

        public override void UpdateSlot(AudioClipAsset asset)
        {
            base.UpdateSlot(asset);
            audioSource.clip = asset.audioClip;
        }
    }
}
