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

        public override void SelectSlot()
        {
            base.SelectSlot();
            AudioClipGallery.Instance.SlotSelected(this);
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

        public override void TrashButtonClicked()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
                AudioClipGallery.Instance.DeleteSlot(this);
            else
                AudioClipGallery.Instance.ConfirmDeleteSlot(this);
        }

        public override void FileSlotDoubleClicked()
        {
            base.FileSlotDoubleClicked();
            AudioClipGallery.Instance.ChooseSlot();
        }
    }
}
