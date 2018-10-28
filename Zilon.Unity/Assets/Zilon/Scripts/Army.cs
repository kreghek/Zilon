using System;

using UnityEngine;

public class Army : MonoBehaviour
{

    public Zilon.Core.GlobalMap.Army ArmyModel { get; private set; }

    public event EventHandler Clicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(Zilon.Core.GlobalMap.Army army)
    {
        ArmyModel = army;
    }
}
