using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AudioToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite playSprite = null;
    [SerializeField] private Sprite stopSprite = null;

    public AudioSource audioSource;

    private Image image;
    private Coroutine resetSpriteCoroutine;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = playSprite;
    }

    public void Switch()
    {
        if (!audioSource.isPlaying)
            Play();
        else
            Stop();
    }

    private void Play()
    {
        if (audioSource.clip == null)
            return;

        image.sprite = stopSprite;

        audioSource.Play();

        if (resetSpriteCoroutine != null)
            StopCoroutine(resetSpriteCoroutine);

        resetSpriteCoroutine = StartCoroutine(ResetSpriteOnClipFinish(audioSource.clip.length));
    }

    private void Stop()
    {
        if (resetSpriteCoroutine != null)
            StopCoroutine(resetSpriteCoroutine);

        image.sprite = playSprite;
        audioSource.Stop();
    }

    private IEnumerator ResetSpriteOnClipFinish(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        image.sprite = playSprite;
    }

    private void OnDisable()
    {
        if (resetSpriteCoroutine != null)
            StopCoroutine(resetSpriteCoroutine);
    }
}
