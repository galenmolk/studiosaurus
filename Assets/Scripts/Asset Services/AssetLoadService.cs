using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Studiosaurus
{
    public class AssetLoadService : MonoBehaviour
    {
        [SerializeField] private AssetConstructor assetConstructor = null;

        public StringEvent broadcastLoadMessage = new StringEvent();
        public FloatEvent onDownloadInProgress = new FloatEvent();

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
        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        private void Awake()
        {
            sharedInstance = this;
        }

        public void LoadSprite(string url)
        {
            if (!UrlContainsText(url))
                return;

            StartCoroutine(SendTextureRequest(url));
        }

        public void LoadAudioClip(string url)
        {
            if (!UrlContainsText(url))
                return;

            StartCoroutine(SendAudioClipRequest(url));
        }

        private bool UrlContainsText(string url)
        {
            bool urlIsValid = !string.IsNullOrWhiteSpace(url);

            if (!urlIsValid)
                broadcastLoadMessage?.Invoke(INVALID_URL_MESSAGE);

            return urlIsValid;
        }

        private IEnumerator SendTextureRequest(string url)
        {
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            www.SendWebRequest();

            yield return WaitForDownload(www);

            if (RequestFailed(www))
                yield break;

            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0, 0), 100F, 0, SpriteMeshType.Tight);
            assetConstructor.AddNewSprite(sprite, url);
            broadcastLoadMessage?.Invoke($"{LOAD_SUCCESS_MESSAGE}{url}");
        }

        private IEnumerator SendAudioClipRequest(string url)
        {
            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN);

            www.SendWebRequest();

            yield return WaitForDownload(www);

            if (RequestFailed(www))
                yield break;

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            assetConstructor.AddNewAudioClip(clip, url);
            broadcastLoadMessage?.Invoke($"{LOAD_SUCCESS_MESSAGE}{url}");
        }

        private IEnumerator WaitForDownload(UnityWebRequest www)
        {
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
                yield return endOfFrame;
            }
        }

        private bool RequestFailed(UnityWebRequest request)
        {
            bool didRequestFail = request.result != UnityWebRequest.Result.Success;

            if (didRequestFail)
                broadcastLoadMessage?.Invoke($"{WWW_ERROR_MESSAGE}{request.error}");

            return didRequestFail;
        }
    }
}