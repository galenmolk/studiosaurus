using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Studiosaurus
{
    public class FileUploadButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static FileUploadButton Instance;

        public enum FileType { SPRITE, AUDIOCLIP }

        private struct FilePanelStrings
        {
            public string title;
            public string extensions;
        }

#if !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void FileUploaderCaptureClick(string objectName);
#endif

        [SerializeField] private Animator animator = null;

        public static FileType fileType;

        private readonly Dictionary<FileType, FilePanelStrings> filePanelParams = new Dictionary<FileType, FilePanelStrings>();

        private const string SPRITE_TITLE = "Choose an image";
        private const string SPRITE_EXTENSIONS = "jpg,png";

        private const string AUDIOCLIP_TITLE = "Choose an audio file";
        private const string AUDIOCLIP_EXTENSIONS = "wav,mp3";

        private FilePanelStrings spriteStrings = new FilePanelStrings() { title = SPRITE_TITLE, extensions = SPRITE_EXTENSIONS };
        private FilePanelStrings audioclipStrings = new FilePanelStrings() { title = AUDIOCLIP_TITLE, extensions = AUDIOCLIP_EXTENSIONS };

        private void Awake()
        {
            Instance = this;
            animator.speed = 0f;
            BuildFilePanelParamsDictionary();
        }

        private void BuildFilePanelParamsDictionary()
        {
            filePanelParams.Add(FileType.SPRITE, spriteStrings);
            filePanelParams.Add(FileType.AUDIOCLIP, audioclipStrings);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            filePanelParams.TryGetValue(fileType, out FilePanelStrings strings);

#if UNITY_EDITOR
            string path = UnityEditor.EditorUtility.OpenFilePanel(strings.title, "", strings.extensions);
            if (!string.IsNullOrEmpty(path))
                FileSelected("file:///" + path);
#else
        Debug.Log("Sending image to " + gameObject.name);
        FileUploaderCaptureClick(gameObject.name);
#endif
        }

        public void FileSelected(string url)
        {
            switch (fileType)
            {
                case FileType.AUDIOCLIP:
                    StartCoroutine(CreatorAssetLoadService.Instance.LoadAudioClip(url));
                    break;
                case FileType.SPRITE:
                    StartCoroutine(CreatorAssetLoadService.Instance.LoadSprite(url));
                    break;
                default:
                    return;
            }
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