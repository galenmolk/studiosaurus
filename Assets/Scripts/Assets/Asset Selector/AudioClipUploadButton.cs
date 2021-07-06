using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class AudioClipUploadButton : MonoBehaviour, IPointerDownHandler
{
    [DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("Open file", "", "");
        if (!string.IsNullOrEmpty(path))
            FileSelected("file:///" + path);
#else
        Debug.Log("Sending image to " + gameObject.name);
        FileUploaderCaptureClick(gameObject.name);
#endif
    }

    public void FileSelected(string url)
    {
        StartCoroutine(CreatorAssetLoadService.Instance.LoadAudioClip(url));
    }
}
