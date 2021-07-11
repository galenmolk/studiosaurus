using System.Collections.Generic;

namespace Studiosaurus
{
    public abstract class GenericAsset<T> where T : GenericAsset<T>
    {
        public GenericAsset(string assetName, string path)
        {
            this.assetName = assetName;
            this.path = path;
        }

        public string assetName;
        public string path;

        public List<AssetComponent<T>> associatedComponents = new List<AssetComponent<T>>();

        public void ReplaceAssetWith(T newAsset = null)
        {
            for (int i = associatedComponents.Count - 1; i >= 0; i--)
            {
                associatedComponents[i].AssignAsset(newAsset);
            }

            associatedComponents.Clear();
        }
    }
}
