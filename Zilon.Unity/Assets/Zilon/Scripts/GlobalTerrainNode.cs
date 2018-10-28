using System;

using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.Spatial;

public class GlobalTerrainNode : MonoBehaviour, ITerrainNodeClientModel
{
    public ITerrainNode Node { get; private set; }

    public event EventHandler Clicked;

    public void Init(ITerrainNode node)
    {
        Node = node;
    }

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
}
