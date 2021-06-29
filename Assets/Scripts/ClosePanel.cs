using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public Window CurrentWindow { get; set; }

    public void CloseWindow()
    {
        if (CurrentWindow != null)
            CurrentWindow.Close();
    }
}
