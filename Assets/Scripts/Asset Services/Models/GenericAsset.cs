using System.Collections.Generic;
using UnityEngine;

namespace Studiosaurus
{
    public abstract class GenericAsset<TAsset> where TAsset : GenericAsset<TAsset>
    {
        public GenericAsset(string assetName, string path)
        {
            this.assetName = assetName;
            this.path = path;
        }

        public string assetName;
        public string path;

        public List<AssetComponent<TAsset>> associatedComponents = new List<AssetComponent<TAsset>>();

        public void ReplaceAssetWith(TAsset newAsset = null)
        {
            for (int i = associatedComponents.Count - 1; i >= 0; i--)
            {
                associatedComponents[i]?.AssignAsset(newAsset);
            }

            associatedComponents.Clear();
        }
    }
}
