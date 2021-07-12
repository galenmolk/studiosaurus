namespace Studiosaurus
{
    public class AudioClipSlot : AssetSlot<AudioClipAsset>
    {
        private AudioClipComponent audioClipComponent;

        protected override void Awake()
        {
            audioClipComponent = GetComponentInChildren<AudioClipComponent>();
        }

        public override void SelectSlot()
        {
            base.SelectSlot();
            audioClipComponent.PlayAudio();
        }
    }
}