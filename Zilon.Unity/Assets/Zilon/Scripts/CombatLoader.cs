using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zilon.Core.Spatial;

public class CombatLoader : MonoBehaviour
{
    public CombatTerrainNode HexPrefab;
    public CombatSquadModel CombatSquad;
    public Transform Parent;

    private readonly List<CombatTerrainNode> nodeModels;
    private readonly List<CombatSquadModel> armyModels;

    // Start is called before the first frame update
    void Start()
    {
        var map = new FixedMap(100);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0], position[1] / 2, 0);

            hexObject.Init(node);

            nodeModels.Add(hexObject);

            hexObject.Clicked += HexObject_Clicked;
        }

        var army = new Army(map.Nodes.First());
        var armyObject = Instantiate(ArmyPrefab, Parent);
        armyObject.Init(army);
        armyObject.Clicked += ArmyObject_Clicked;
    }

    private void HexObject_Clicked(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
