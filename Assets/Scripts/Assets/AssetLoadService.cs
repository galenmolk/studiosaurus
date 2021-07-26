using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

        private const string LOAD_SUCCESS_MESSAGE = "<b>Asset Loaded:</b> ";
        private const string WWW_ERROR_MESSAGE = "Failed to Load Asset: ";
        private const string INVALID_URL_MESSAGE = "Asset URL is invalid!";

        private Texture2D texture;
        private readonly WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        private readonly List<string> urls = new List<string>();

        private void Awake()
        {
            sharedInstance = this;
        }

        public void LoadSprite(string urlText)
        {
            if (!UrlContainsText(urlText))
                return;

            ReadyUrls(urlText);

            StartCoroutine(SendAllTextureRequests());
        }
        
        public void LoadAudioClip(string urlText)
        {
            if (!UrlContainsText(urlText))
                return;

            ReadyUrls(urlText);

            StartCoroutine(SendAllAudioClipRequests());
        }

        private bool UrlContainsText(string url)
        {
            bool urlIsValid = !string.IsNullOrWhiteSpace(url);

            if (!urlIsValid)
                broadcastLoadMessage?.Invoke(INVALID_URL_MESSAGE);

            return urlIsValid;
        }

        private void ReadyUrls(string urlText)
        {
            urls.Clear();
            string[] newUrls = urlText.Split(',');
            for (int i = 0, length = newUrls.Length; i < length; i++)
            {
                urls.Add(newUrls[i]);
            }
        }

        private IEnumerator SendAllTextureRequests()
        {
            while (urls.Count > 0)
            {
                string currentRequestUrl = urls[urls.Count - 1];
                yield return StartCoroutine(SendTextureRequest(currentRequestUrl));
                urls.Remove(currentRequestUrl);
            }
        }

        private IEnumerator SendAllAudioClipRequests()
        {
            while (urls.Count > 0)
            {
                string currentRequestUrl = urls[urls.Count - 1];
                yield return StartCoroutine(SendAudioClipRequest(currentRequestUrl));
                urls.Remove(currentRequestUrl);
            }
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
            //WWW data = new WWW(url);
            //yield return data;
            //AudioClip clip = data.GetAudioClip(,) as AudioClip;
            //AudioType audioType = TryGetAudioType(url);
            //Debug.Log(audioType.ToString());
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
                onDownloadInProgress?.Invoke(www.downloadProgress);
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

        private AudioType TryGetAudioType(string url)
        {
            string extension = new FileInfo(url).Extension;
            extension = extension.ToLower();

            AudioType audioType = AudioType.UNKNOWN;

            switch (extension)
            {
                case ".mp3":
                    audioType = AudioType.MPEG;
                    break;
                case ".ogg":
                    audioType = AudioType.OGGVORBIS;
                    break;
                case ".wav":
                    audioType = AudioType.WAV;
                    break;
            }

            return audioType;
        }
    }
}