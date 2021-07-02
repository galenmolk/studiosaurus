using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public class URLUploadService : MonoBehaviour
{
#if !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void PasteHereWindow(string getText);
#endif

    [SerializeField] private TMP_Text urlUploadPromptText = null;
    [SerializeField] private TMP_InputField urlInputField = null;
    [SerializeField] private Button uploadButton = null;


#if !UNITY_EDITOR
    private void Awake()
    {
        urlUploadPromptText.gameObject.SetActive(false);
        urlInputField.gameObject.SetActive(false);
        uploadButton.interactable = true;
    }
#endif

    public void OnValueChanged()
    {
        uploadButton.interactable = !string.IsNullOrWhiteSpace(urlInputField.text);
    }

    public void UploadFromURL()
    {
#if UNITY_EDITOR
        StartCoroutine(CreatorAssetLoadService.Instance.LoadSprite(urlInputField.text));
#else
        PasteHereWindow(gameObject.name);
#endif
    }

    public void GetPastedText(string pastedUrl)
    {
        StartCoroutine(CreatorAssetLoadService.Instance.LoadSprite(pastedUrl));
    }
}
