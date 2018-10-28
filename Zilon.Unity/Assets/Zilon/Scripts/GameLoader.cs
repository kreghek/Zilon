using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Commands;
using Zilon.Core.GlobalMap;
using Zilon.Core.Spatial;
// ReSharper disable CheckNamespace

public class GameLoader : MonoBehaviour
{
    [Inject] private ICommandManager _commandManager;

    [Inject] private IGlobalState _globalStateManager;

    [Inject] private IArmyModeCommand _moveCommand;

    public GameObject HexPrefab;
    public GlobalArmy ArmyPrefab;
    public Transform Parent;

    private readonly List<GlobalTerrainNode> nodeModels;
    private readonly List<GlobalArmy> armyModels;

    public GameLoader()
    {
        nodeModels = new List<GlobalTerrainNode>();
        armyModels = new List<GlobalArmy>();
    }

    void Start()
    {
        var map = new LazyHexMap(100);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0], position[1] / 2, 0);

            var clientModel = hexObject.GetComponent<GlobalTerrainNode>();
            clientModel.Init(node);

            nodeModels.Add(clientModel);

            clientModel.Clicked += ClientModel_Clicked;
        }

        var army = new Army(map.Nodes.First());
        var armyObject = Instantiate(ArmyPrefab, Parent);
        armyObject.Init(army);
        armyObject.Clicked += ArmyObject_Clicked;

        armyModels.Add(armyObject);
    }

    void Update()
    {
        foreach (var globalArmy in armyModels)
        {
            var nodeModel = nodeModels.Single(x => x.Node == globalArmy.Army.Node);
            globalArmy.transform.position = nodeModel.transform.position;
        }
    }

    private void ArmyObject_Clicked(object sender, System.EventArgs e)
    {
        var armyClientModel = (GlobalArmy) sender;
        _globalStateManager.SelectedArmy = armyClientModel;
    }

    private void ClientModel_Clicked(object sender, System.EventArgs e)
    {
        var nodeClientModel = (GlobalTerrainNode) sender;
        _globalStateManager.HoverNode = nodeClientModel;

        if (_globalStateManager.SelectedArmy != null)
        {
            _commandManager.RegisterCommand(_moveCommand);
        }
    }
}
