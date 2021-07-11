using UnityEngine;

namespace Studiosaurus
{
    public class AudioClipAsset : GenericAsset<AudioClipAsset>
    {
        public AudioClipAsset(string assetName, string path, AudioClip audioClip) : base(assetName, path)
        {
            this.audioClip = audioClip;
        }

        public AudioClip audioClip;
    }
}