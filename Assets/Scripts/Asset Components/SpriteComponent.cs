using UnityEngine.UI;

namespace Studiosaurus
{
    public class SpriteComponent : AssetComponent<SpriteAsset>
    {
        private Image image;

        public override SpriteAsset Asset { get; set; }

        private void Awake()
        {
            image = transform.parent.GetComponent<Image>();
        }

        public override void AssignAsset(SpriteAsset newAsset)
        {
            base.AssignAsset(newAsset);

            image.sprite = newAsset?.sprite;
            image.SetNativeSize();
        }
    }
}