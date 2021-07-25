using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class SpriteComponent : AssetComponent<SpriteAsset>
    {
        public override SpriteAsset Asset { get; protected set; }

        private Image image = null;

        protected override void Awake()
        {
            base.Awake();
            image = GetComponentInParent<Image>();
        }

        public override void AssignAsset(SpriteAsset newAsset = null)
        {
            if (image != null)
            {
                image.sprite = newAsset?.sprite;
                image.SetNativeSize();
                doItObject.onNewSpriteAssigned.Invoke();
            }

            base.AssignAsset(newAsset);
        }

        public override string GetComponentAsJSON()
        {
            throw new System.NotImplementedException();
        }
    }
}