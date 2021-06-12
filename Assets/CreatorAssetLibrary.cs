using System.Collections.Generic;
using UnityEngine;

public class CreatorAssetLibrary : MonoBehaviour
{
    static CreatorAssetLibrary sharedInstance;
    public static CreatorAssetLibrary Instance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType<CreatorAssetLibrary>();

            return sharedInstance;
        }
    }

    private void Awake()
    {
        sharedInstance = this;
    }

    private Dictionary<string, Sprite> spritesInMemory = new Dictionary<string, Sprite>();
    private Dictionary<string, AudioClip> audioClipsInMemory = new Dictionary<string, AudioClip>();

    public bool HasSpriteBeenLoaded(string url)
    {
        return spritesInMemory.ContainsKey(url);
    }
    
    public bool HasAudioClipBeenLoaded(string url)
    {
        return audioClipsInMemory.ContainsKey(url);
    }

    public void AddNewSprite(Sprite sprite, string url)
    {
        if (spritesInMemory.ContainsKey(url))
            spritesInMemory.Remove(url);

        spritesInMemory.Add(url, sprite);
    }

    public void AddNewAudioClip(AudioClip audioClip, string url)
    {
        if (audioClipsInMemory.ContainsKey(url))
            audioClipsInMemory.Remove(url);

        audioClipsInMemory.Add(url, audioClip);
    }
}
