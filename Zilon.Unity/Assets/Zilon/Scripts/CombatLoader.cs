using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zilon.Core.Combat;
using Zilon.Core.Spatial;

public class CombatLoader : MonoBehaviour
{
    public CombatTerrainNode HexPrefab;
    public CombatSquadModel CombatSquadPrefab;
    public CombatPersonModel CompatPersonPrefab;
    public Transform Parent;

    private readonly List<CombatTerrainNode> nodeModels;
    private readonly List<CombatSquadModel> squadModels;

    public CombatLoader()
    {
        nodeModels = new List<CombatTerrainNode>();
        squadModels = new List<CombatSquadModel>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var map = new FixedMap(100);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0] * 20, position[1] / 2 * 20, 0);

            hexObject.Init(node);

            nodeModels.Add(hexObject);

            hexObject.Clicked += HexObject_Clicked;
        }

        for (var i = 0; i < 3; i++)
        {
            var node = map.Nodes.Skip(i * 3 + 1).First();
            var personList = new List<ICombatPerson>();

            for (var j = 0; j < 5; j++)
            {
                var person = new CombatPerson();
                personList.Add(person);
            }

            var squad = new CombatSquad(node, personList.ToArray());

            var squadObject = Instantiate(CombatSquadPrefab, Parent);
            var personModelList = new List<CombatPersonModel>();
            var formationSize = Mathf.Sqrt(squad.Persons.Length) + 1;
            var personX = 0;
            var personY = 0;
            foreach (var combatPerson in squad.Persons)
            {
                var combatPersonModel = Instantiate(CompatPersonPrefab, squadObject.transform);
                personModelList.Add(combatPersonModel);

                personX++;
                if (personX >= formationSize)
                {
                    personX = 0;
                    personY++;
                }

                combatPersonModel.transform.position = new Vector3(personX * 1.5f, personY * 1.5f);
            }
            squadObject.Init(squad, personModelList.ToArray());
            squadModels.Add(squadObject);
        }
    }

    private void HexObject_Clicked(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var squad in squadModels)
        {
            var nodeModel = nodeModels.Single(x => x.Node == squad.Squad.Node);
            squad.transform.position = nodeModel.transform.position;
        }
    }
}
