using UnityEngine;

public static class Utils
{
    public static void SetCanvasGroupEnabled(CanvasGroup canvasGroup, bool isEnabled)
    {
        canvasGroup.alpha = isEnabled ? 1f : 0f;
        canvasGroup.interactable = isEnabled;
        canvasGroup.blocksRaycasts = isEnabled;
    }

    public static void ConstrainRectTransformToSize(RectTransform rectTransform, Vector2 constrainingSize)
    {
        float xScaleFactor = 1f, yScaleFactor = 1f;

        Vector2 startingSize = rectTransform.sizeDelta;
        if (startingSize.x > constrainingSize.x)
            xScaleFactor = constrainingSize.x / startingSize.x;

        if (startingSize.y > constrainingSize.y)
            yScaleFactor = constrainingSize.y / startingSize.y;

        float scaleFactor = xScaleFactor < yScaleFactor ? xScaleFactor : yScaleFactor;
        rectTransform.sizeDelta = scaleFactor * startingSize;
    }
}
