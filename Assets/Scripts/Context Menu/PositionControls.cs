using UnityEngine;
using TMPro;
using System;

public class PositionControls : ControlSection
{
    [SerializeField] private TMP_InputField xInputText = null;
    [SerializeField] private TMP_InputField yInputText = null;

    public void OnEndEdit()
    {
        float.TryParse(xInputText.text, out float x);
        float.TryParse(yInputText.text, out float y);

        doItObject.AnchoredPosition = new Vector2(x, y);
    }

    public override void InitializeControls(DoItObject doItObject)
    {
        base.InitializeControls(doItObject);
        UpdatePosition(doItObject.AnchoredPosition);
        doItObject.onPositionChanged.AddListener((position) => UpdatePosition(position));
    }

    private void UpdatePosition(Vector2 position)
    {
        xInputText.text = position.x.ToString("0.##");
        yInputText.text = position.y.ToString("0.##");
    }
}
