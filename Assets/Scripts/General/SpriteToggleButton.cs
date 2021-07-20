using UnityEngine;
using UnityEngine.UI;

public class SpriteToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite firstStateSprite = null;
    [SerializeField] private Sprite secondStateSprite = null;

    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = firstStateSprite;
        GetComponent<Button>().onClick.AddListener(Switch);
    }

    private void Switch()
    {
        image.sprite = image.sprite == firstStateSprite ? secondStateSprite : firstStateSprite;
    }
}
