using UnityEngine;

namespace Studiosaurus
{
    public class SpriteAsset : GenericAsset<SpriteAsset>
    {
        public SpriteAsset(string assetName, string path, Sprite sprite) : base(assetName, path)
        {
            this.sprite = sprite;
        }

        public Sprite sprite;
    }
}