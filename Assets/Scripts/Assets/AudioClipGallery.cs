using UnityEngine;

namespace Studiosaurus
{
    public class AudioClipGallery : AssetGallery<AudioClipAsset>
    {
        [SerializeField] private GameObject localDriveUploadArea = null;

        // Uploading audio from the local file system via WebGL does not work yet.
        // Only URL uploads for Audio.
#if !UNITY_EDITOR
        private void Start()
        {
            localDriveUploadArea.SetActive(false);
        }
#endif
    }
}