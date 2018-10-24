using JetBrains.Annotations;

using UnityEngine;

using Zilon.Core.Spatial;
// ReSharper disable CheckNamespace

[UsedImplicitly]
public class GameLoader : MonoBehaviour
{
    public GameObject HexPrefab;
    public Transform Parent;

    [UsedImplicitly]
    void Start()
    {
        var map = new LazyHexMap(100);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0], position[1] / 2, 0);
        }
    }
}
