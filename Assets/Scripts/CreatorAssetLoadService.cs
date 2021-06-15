using System.Collections;
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

    private void Awake()
    {
        sharedInstance = this;
    }

    public IEnumerator LoadSprite(string url)
    {
        if (url == null || string.IsNullOrEmpty(url))
            yield break;

        if (CreatorAssetLibrary.Instance.HasSpriteBeenLoaded(url))
            yield break;

        using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
            yield return www.error;
        }
        else
        {
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0, 0), 100F, 0, SpriteMeshType.Tight);

            CreatorAssetLibrary.Instance.AddNewSprite(sprite, url);
        }
    }

    public IEnumerator LoadAudioClip(string url)
    {
        yield return null;
        Debug.Log("Audio Loaded: " + url);
    }
}
