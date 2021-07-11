using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Studiosaurus
{
    public class AssetLoadService : MonoBehaviour
    {
        [SerializeField] private AssetConstructor assetConstructor = null;

        public StringEvent broadcastLoadMessage = new StringEvent();

        private static AssetLoadService sharedInstance;
        public static AssetLoadService Instance
        {
            get
            {
                if (sharedInstance == null)
                    sharedInstance = FindObjectOfType<AssetLoadService>();

                return sharedInstance;
            }
        }

        private const string LOAD_SUCCESS_MESSAGE = "Asset Loaded: ";
        private const string WWW_ERROR_MESSAGE = "Failed to Load Asset: ";
        private const string INVALID_URL_MESSAGE = "Asset URL is invalid!";

        private Texture2D texture;

        private void Awake()
        {
            sharedInstance = this;
        }

        public IEnumerator LoadSprite(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                broadcastLoadMessage?.Invoke(INVALID_URL_MESSAGE);
                yield break;
            }

            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                broadcastLoadMessage?.Invoke($"{WWW_ERROR_MESSAGE}{www.error}");
                yield return www.error;
            }
            else
            {
                texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Sprite sprite = Sprite.Create(texture, rect, new Vector2(0, 0), 100F, 0, SpriteMeshType.Tight);

                assetConstructor.AddNewSprite(sprite, url);
                broadcastLoadMessage?.Invoke($"{LOAD_SUCCESS_MESSAGE}{url}");
            }
        }

        public IEnumerator LoadAudioClip(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                broadcastLoadMessage?.Invoke(INVALID_URL_MESSAGE);
                yield break;
            }

            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                broadcastLoadMessage?.Invoke($"{WWW_ERROR_MESSAGE}{www.error}");
                yield return www.error;
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

                assetConstructor.AddNewAudioClip(clip, url);
                broadcastLoadMessage?.Invoke($"{LOAD_SUCCESS_MESSAGE}{url}");
            }
        }
    }
}