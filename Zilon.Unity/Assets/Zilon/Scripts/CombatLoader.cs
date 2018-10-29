using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;
using Zilon.Core.Commands;
using Zilon.Core.Dices;
using Zilon.Core.Players;
using Zilon.Core.Spatial;

public class CombatLoader : MonoBehaviour
{
    public CombatTerrainNode HexPrefab;
    public CombatSquadModel CombatSquadPrefab;
    public CombatPersonModel CompatPersonPrefab;
    public Transform Parent;

    private readonly List<CombatTerrainNode> nodeModels;
    private readonly List<CombatSquadModel> squadModels;

    [Inject] private ICommandManager _commandManager;

    [Inject] public ICombatStateManager _combatStateManager;

    [Inject] private readonly ISquadMoveCommand _moveCommand;

    [Inject] private readonly ISquadAttackCommand _attackCommand;

    [Inject] private readonly IDice _dice;

    public CombatLoader()
    {
        nodeModels = new List<CombatTerrainNode>();
        squadModels = new List<CombatSquadModel>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var map = new FixedMap(25);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0] * 20, position[1] / 2 * 20, 0);

            hexObject.Init(node);

            nodeModels.Add(hexObject);

            hexObject.Clicked += HexObject_Clicked;
        }

        for (var i = 0; i < 6; i++)
        {
            var node = map.Nodes.Skip(i * 3 + 1).First();
            var personList = new List<ICombatPerson>();

            for (var j = 0; j < 5; j++)
            {
                var person = new CombatPerson(_dice);
                personList.Add(person);
            }

            var squad = new CombatSquad(node, personList.ToArray(), i < 2 ? Player.Human : Player.Cpu);

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

                combatPersonModel.Clicked += CombatPersonModelOnClicked;
            }
            squadObject.Init(squad, personModelList.ToArray());
            squadModels.Add(squadObject);
        }
    }

    private void CombatPersonModelOnClicked(object sender, EventArgs e)
    {
        ISquadClientModel clickedSquad = null;
        var combatPersonModel = (CombatPersonModel)sender;
        foreach (var combatSquadModel in squadModels)
        {
            foreach (var personModel in combatSquadModel.PersonModels)
            {
                if (personModel == combatPersonModel)
                {
                    clickedSquad = combatSquadModel;
                    break;
                }
            }

            if (clickedSquad != null)
            {
                break;
            }
        }

        if (_combatStateManager.SelectedSquad?.Squad.Player == Player.Human)
        {
            _combatStateManager.SelectedSquad = clickedSquad;
        }
        else
        {
            if (_combatStateManager.SelectedSquad != null)
            {
                _combatStateManager.HoverSquad = clickedSquad;

                _commandManager.RegisterCommand(_attackCommand);
            }
        }
    }

    private void HexObject_Clicked(object sender, EventArgs e)
    {
        var nodeModel = (CombatTerrainNode)sender;
        _combatStateManager.HoverNode = nodeModel;
        if (_combatStateManager.SelectedSquad != null)
        {
            _commandManager.RegisterCommand(_moveCommand);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var squad in squadModels)
        {
            var nodeModel = nodeModels.Single(x => x.Node == squad.Squad.Node);
            squad.transform.position = nodeModel.transform.position;
        }
    }
}
