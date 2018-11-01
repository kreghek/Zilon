using System;

using JetBrains.Annotations;

using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.Spatial;
// ReSharper disable CheckNamespace

public class CombatTerrainNode : MonoBehaviour, ITerrainNodeClientModel
{
    public ITerrainNode Node { get; private set; }

    public event EventHandler Clicked;
    public event EventHandler Hover;

    public void Init(ITerrainNode node)
    {
        Node = node;
    }

    [UsedImplicitly]
    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    [UsedImplicitly]
    public void OnMouseEnter()
    {
        Hover?.Invoke(this, new EventArgs());
    }
}
