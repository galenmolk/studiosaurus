using UnityEngine;

public class ControlSection : MonoBehaviour
{
    protected DoItObject doItObject;

    public virtual void InitializeControls(DoItObject doItObject)
    {
        this.doItObject = doItObject;
    }
}
