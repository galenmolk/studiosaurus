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
    [DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);
    [DllImport("__Internal")] private static extern void OpenUrlWindow(string objectName);
#endif

        [SerializeField] private Animator animator = null;
        [SerializeField] private string filePanelTitle = string.Empty;
        [SerializeField] private string[] fileExtensions = null;
        [SerializeField] private TMP_Text urlUploadPromptText = null;
        [SerializeField] private TMP_InputField urlInputField = null;
        [SerializeField] private Button uploadButton = null;

        [SerializeField] private string extensionsParameter = string.Empty;

        public StringEvent onUrlReceived = new StringEvent();

        private void Awake()
        {
            animator.speed = 0f;
            CreateExtensionsParamater();
#if !UNITY_EDITOR
            urlUploadPromptText.gameObject.SetActive(false);
            urlInputField.gameObject.SetActive(false);
            uploadButton.interactable = true;
#endif
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

        public void OnUrlFieldValueChanged()
        {
            uploadButton.interactable = !string.IsNullOrWhiteSpace(urlInputField.text);
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

        public void UploadFromURL()
        {
#if UNITY_EDITOR
            FileSelected(urlInputField.text);
#else
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

        private void OnDisable()
        {
            onUrlReceived.RemoveAllListeners();
        }
    }
}