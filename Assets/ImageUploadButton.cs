public class ImageUploadButton : WebGLFileUploadButton
{
    public override void FileSelected(string url)
    {
        StartCoroutine(CreatorAssetLoadService.Instance.LoadSprite(url));
    }
}
