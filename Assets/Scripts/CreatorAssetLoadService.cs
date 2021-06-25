using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CreatorAssetLoadService : MonoBehaviour
{
    private Texture2D texture;

    static CreatorAssetLoadService sharedInstance;
    public static CreatorAssetLoadService Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<CreatorAssetLoadService>();

            return sharedInstance;
        }
    }

    [SerializeField] private Animator loadResultAnimator = null;
    [SerializeField] private TMP_Text loadResultText = null;

    private const string LOAD_SUCCESS_MESSAGE = "Asset Loaded: ";
    private const string WWW_ERROR_MESSAGE = "Failed to Load Asset: ";
    private const string INVALID_URL_MESSAGE = "Asset URL is invalid!";
    private const string DISPLAY_ANIM_TRIGGER = "Display";

    private void Awake()
    {
        sharedInstance = this;
    }

    public IEnumerator LoadSprite(string url)
    {
        if (url == null || string.IsNullOrEmpty(url))
        {
            DisplayLoadMessage(INVALID_URL_MESSAGE);
            yield break;
        }

        using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            DisplayLoadMessage($"{WWW_ERROR_MESSAGE}{www.error}");
            yield return www.error;
        }
        else
        {
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0, 0), 100F, 0, SpriteMeshType.Tight);

            CreatorAssetLibrary.Instance.AddNewSprite(sprite, url);
            DisplayLoadMessage($"{LOAD_SUCCESS_MESSAGE}{url}");
        }
    }

    public IEnumerator LoadAudioClip(string url)
    {
        yield return null;
        Debug.Log("Audio Loaded: " + url);
    }

    private void DisplayLoadMessage(string message)
    {
        loadResultText.text = message;
        loadResultAnimator.SetTrigger(DISPLAY_ANIM_TRIGGER);
    }
}
