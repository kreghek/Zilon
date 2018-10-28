using System;

using UnityEngine;

using Zilon.Core.Spatial;

public class CombatTerrainNode : MonoBehaviour
{
    public ITerrainNode Node { get; private set; }

    public event EventHandler Clicked;

    public void Init(ITerrainNode node)
    {
        Node = node;
    }
}
