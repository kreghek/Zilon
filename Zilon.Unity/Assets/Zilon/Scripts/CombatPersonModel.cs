using System;

using UnityEngine;

public class CombatPersonModel : MonoBehaviour
{
    public event EventHandler Clicked;

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }
}
