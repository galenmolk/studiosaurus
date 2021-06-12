public class AudioUploadButton : WebGLFileUploadButton
{
    public override void FileSelected(string url)
    {
        StartCoroutine(CreatorAssetLoadService.Instance.LoadAudioClip(url));
    }
}
