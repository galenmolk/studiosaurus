namespace Studiosaurus
{
    public class SpriteControls : AssetControls<SpriteAsset>
    {
        private SpriteComponent spriteComponent = null;

        public override void Initialize(AssetComponent<SpriteAsset> spriteComponent)
        {
            this.spriteComponent = (SpriteComponent)spriteComponent;
        }

        public override void OpenGallery()
        {
            SpriteGallery.Instance.Open(spriteComponent);
        }
    }
}
