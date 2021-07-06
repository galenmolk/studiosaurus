public class SpriteControls : ControlSection
{
    public void OpenSpritesGallery()
    {
        AssetSelector.Instance.Open(doItObject, CreatorAssetLibrary.Instance.sprites);
    }
}
