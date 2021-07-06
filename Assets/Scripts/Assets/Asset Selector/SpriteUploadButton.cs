using UnityEngine;
using UnityEngine.EventSystems;

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public class SpriteUploadButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
#if !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);
#endif

    [SerializeField] private Animator animator = null;

    private void Awake()
    {
        animator.speed = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("Open file", "", "jpg,png");
        if (!string.IsNullOrEmpty(path))
            FileSelected("file:///" + path);
#else
        Debug.Log("Sending image to " + gameObject.name);
        FileUploaderCaptureClick(gameObject.name);
#endif
    }

    public void FileSelected(string url)
    {
        StartCoroutine(CreatorAssetLoadService.Instance.LoadSprite(url));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.speed = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.speed = 0f;
    }
}
