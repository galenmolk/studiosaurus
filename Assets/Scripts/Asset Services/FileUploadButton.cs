using UnityEngine;
using UnityEngine.EventSystems;

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Studiosaurus
{
    public class FileUploadButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {

#if !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);
#endif

        [SerializeField] private Animator animator = null;
        [SerializeField] private string filePanelTitle = string.Empty;
        [SerializeField] private string[] fileExtensions = null;

        [SerializeField] private string extensionsParameter = string.Empty;

        public StringEvent onUrlReceived;

        private void Awake()
        {
            animator.speed = 0f;
            CreateExtensionsParamater();
        }

        private void CreateExtensionsParamater()
        {
            if (!extensionsParameter.Equals(string.Empty))
                return;

            for (int i = 0, length = fileExtensions.Length; i < length; i++)
            {
                extensionsParameter += fileExtensions[i];

                if (i < length - 1)
                    extensionsParameter += ",";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_EDITOR
            string path = UnityEditor.EditorUtility.OpenFilePanel(filePanelTitle, "", extensionsParameter);
            if (!string.IsNullOrWhiteSpace(path))
                FileSelected("file:///" + path);
#else
        Debug.Log("Sending image to " + gameObject.name);
        FileUploaderCaptureClick(gameObject.name);
#endif
        }

        public void FileSelected(string url)
        {
            onUrlReceived?.Invoke(url);
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
}