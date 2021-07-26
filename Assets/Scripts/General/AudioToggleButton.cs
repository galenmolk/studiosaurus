using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AudioToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite playSprite = null;
    [SerializeField] private Sprite stopSprite = null;

    private Button button;

    private Sprite defaultSprite;

    public AudioSource audioSource;

    private Image image;
    private Coroutine resetSpriteCoroutine;

    private SpriteState playingState = new SpriteState();
    private SpriteState stoppedState = new SpriteState();

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        defaultSprite = image.sprite;
        SetSpriteStates();
    }

    private void SetSpriteStates()
    {
        playingState.highlightedSprite = stopSprite;
        playingState.pressedSprite = stopSprite;
        stoppedState.highlightedSprite = playSprite;
        stoppedState.pressedSprite = playSprite;
    }

    public void Switch()
    {
        if (!audioSource.isPlaying)
            Play();
        else
        {
            if (resetSpriteCoroutine != null)
                StopCoroutine(resetSpriteCoroutine);

            Stop();
        }
    }

    private void Play()
    {
        if (audioSource.clip == null)
            return;

        image.sprite = stopSprite;
        button.spriteState = playingState;
        audioSource.Play();

        if (resetSpriteCoroutine != null)
            StopCoroutine(resetSpriteCoroutine);

        resetSpriteCoroutine = StartCoroutine(ResetSpriteOnClipFinish(audioSource.clip.length));
    }

    public void Stop()
    {
        button.spriteState = stoppedState;
        image.sprite = defaultSprite;

        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    private IEnumerator ResetSpriteOnClipFinish(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Stop();
    }

    private void OnDisable()
    {
        Stop();
    }

    private void OnDestroy()
    {
        Stop();
    }
}
