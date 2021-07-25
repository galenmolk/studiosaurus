using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Studiosaurus
{
    public class FileUploadService : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {

#if !UNITY_EDITOR
    //[DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);
    [DllImport("__Internal")] private static extern void OpenUrlWindow(string objectName);
#endif

        [SerializeField] private Animator animator = null;
        [SerializeField] private string filePanelTitle = string.Empty;
        [SerializeField] private string[] fileExtensions = null;
        [SerializeField] private TMP_Text urlUploadPromptText = null;
        [SerializeField] private TMP_InputField urlInputField = null;
        [SerializeField] private Button urlUploadButton = null;

        public StringEvent onUrlReceived = new StringEvent();

        private string lastDirectory = string.Empty;
        private string extensionsParameter = string.Empty;

        private const string EDITOR_FILE_EXTENSION_DELIMITER = ",";
        private const string JAVASCRIPT_FILE_EXTENSION_DELIMITER = ", ";

        private string currentPlatformDelimiter = EDITOR_FILE_EXTENSION_DELIMITER;

        private void Awake()
        {
            animator.speed = 0f;
            CreateExtensionsParamater();
#if !UNITY_EDITOR
            currentPlatformDelimiter = JAVASCRIPT_FILE_EXTENSION_DELIMITER;
            urlUploadPromptText.gameObject.SetActive(false);
            urlInputField.gameObject.SetActive(false);
            urlUploadButton.interactable = true;
#endif
        }

        private void CreateExtensionsParamater()
        {
            if (!extensionsParameter.Equals(string.Empty))
                return;

            for (int i = 0, length = fileExtensions.Length; i < length; i++)
            {
                string extension = fileExtensions[i];
#if !UNITY_EDITOR
                extension = $".{extension}";

#endif
                extensionsParameter += extension;

                if (i < length - 1)
                    extensionsParameter += currentPlatformDelimiter;
            }
        }

        public void OnUrlFieldValueChanged()
        {
            urlUploadButton.interactable = !string.IsNullOrWhiteSpace(urlInputField.text);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_EDITOR
            string path = UnityEditor.EditorUtility.OpenFilePanel(filePanelTitle, lastDirectory, extensionsParameter);
            if (!string.IsNullOrWhiteSpace(path))
            {
                lastDirectory = new FileInfo(path).DirectoryName;
                FileSelected("file:///" + path);
            }
#else
Debug.Log("Upload: " + gameObject.name);
UploadFile(gameObject.name, "FileSelected", extensionsParameter, true);
        //FileUploaderCaptureClick(gameObject.name);
#endif
        }

        public void UploadFromURL()
        {
#if UNITY_EDITOR
            FileSelected(urlInputField.text);
#else
Debug.Log("URL WINDOW: " + gameObject.name);

        OpenUrlWindow(gameObject.name);
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
