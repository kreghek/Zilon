using System;

using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.Spatial;

public class CombatTerrainNode : MonoBehaviour, ITerrainNodeClientModel
{
    public ITerrainNode Node { get; private set; }

    public event EventHandler Clicked;

    public void Init(ITerrainNode node)
    {
        Node = node;
    }

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }
}
