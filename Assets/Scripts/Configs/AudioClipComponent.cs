using UnityEngine;

namespace Studiosaurus
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioClipComponent : AssetComponent<AudioClipAsset>
    {
        [HideInInspector] public AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        public override void AssignAsset(AudioClipAsset newAsset)
        {
            if (audioSource != null)
                audioSource.clip = newAsset?.audioClip;

            base.AssignAsset(newAsset);
        }

        [UnityEngine.ContextMenu("Json")]
        public override string GetComponentAsJSON()
        {
            if (Asset == null)
            {
                Debug.LogWarning("No AudioClip Found Assigned to AudioClipComponent");
                return string.Empty;
            }

            string path = $"{Application.persistentDataPath}/{GetInstanceID()}.wav";
            SavWav.Save(path, Asset.audioClip);
            Debug.Log(path);
            string uploadName = CloudinaryUploader.UploadAudioClip(path);
            Debug.Log(uploadName);
            string json = JsonSerializer.GetAsset(configKey.key, uploadName);
            Debug.Log(json);
            return json;
        }
    }
}
