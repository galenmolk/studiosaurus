using UnityEngine;
using TMPro;

public class SizeControls : ControlSection
{
    [SerializeField] private TMP_InputField widthInputText = null;
    [SerializeField] private TMP_InputField heightInputText = null;

    public void OnEndEdit()
    {
        float.TryParse(widthInputText.text, out float width);
        float.TryParse(heightInputText.text, out float height);

        doItObject.SizeDelta = new Vector2(width, height);
    }

    public override void InitializeControls(DoItObject doItObject)
    {
        base.InitializeControls(doItObject);
        UpdateSize(doItObject.SizeDelta);
        doItObject.onSizeChanged.AddListener((size) => UpdateSize(size));
    }

    private void UpdateSize(Vector2 size)
    {
        widthInputText.text = size.x.ToString("0.##");
        heightInputText.text = size.y.ToString("0.##");
    }
}
