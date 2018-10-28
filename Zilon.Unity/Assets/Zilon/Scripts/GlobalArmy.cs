using System;

using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.GlobalMap;

public class GlobalArmy : MonoBehaviour, IArmyClientModel
{

    public Army Army { get; private set; }

    public event EventHandler Clicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(Zilon.Core.GlobalMap.Army army)
    {
        Army = army;
    }
}
