using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreatorAssetLoadService : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    private Texture2D texture;

    private List<string> activeRequests = new List<string>();


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

    //public IEnumerator LoadSprite(string url)
    //{
    //    if (this.texture != null)
    //        Destroy(this.texture);

    //    WWW image = new WWW(url);

    //    yield return image;

    //    Texture2D texture = new Texture2D(1, 1);

    //    image.LoadImageIntoTexture(texture);

    //    Rect rect = new Rect(0, 0, texture.width, texture.height);

    //    targetImage.sprite = Sprite.Create(texture, rect, Vector2.one / 2);
    //    targetImage.SetNativeSize();

    //    this.texture = texture;
    //}

    public IEnumerator LoadSprite(string url)
    {
        if (url == null || string.IsNullOrEmpty(url))
            yield break;

        if (CreatorAssetLibrary.Instance.HasSpriteBeenLoaded(url))
            yield break;

        using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        activeRequests.Add(url);
        yield return www.SendWebRequest();
        activeRequests.Remove(url);

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
            targetImage.sprite = sprite;
            targetImage.SetNativeSize();
        }
    }

    public IEnumerator LoadAudioClip(string url)
    {
        yield return null;
        Debug.Log("Audio Loaded: " + url);
    }
}
