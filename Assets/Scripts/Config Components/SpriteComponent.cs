using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus
{
    public class SpriteComponent : AssetComponent<SpriteAsset>
    {
        public override SpriteAsset Asset { get; protected set; }

        [SerializeField] private Image image = null;

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
                doItObject.SizeRatio = image.rectTransform.sizeDelta.x / image.rectTransform.sizeDelta.y;
                Debug.Log(doItObject.SizeRatio);
            }

            base.AssignAsset(newAsset);
        }
    }
}