using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public Window CurrentWindow { get; set; }

    public void CloseWindow()
    {
        CurrentWindow.Close();
    }
}
