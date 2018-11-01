using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;
using Zilon.Core.Commands;
using Zilon.Core.Dices;
using Zilon.Core.NameGeneration;
using Zilon.Core.Players;
using Zilon.Core.Spatial;

// ReSharper disable CheckNamespace

[UsedImplicitly]
public class CombatLoader : MonoBehaviour
{
    [UsedImplicitly] [Inject] private readonly ISquadAttackCommand _attackCommand;

    [UsedImplicitly] [Inject] private readonly ICombatEventBus _combatEventBus;

    [UsedImplicitly] [Inject] private readonly ICombatService _combatService;

    [UsedImplicitly] [Inject] private readonly ICombatStateManager _combatStateManager;

    [UsedImplicitly] [Inject] private readonly ICommandManager _commandManager;

    [UsedImplicitly] [Inject] private readonly IDice _dice;

    [UsedImplicitly] [Inject] private readonly ISquadMoveCommand _moveCommand;

    private readonly List<CombatTerrainNode> _nodeModels;
    private readonly List<CombatSquadModel> _squadModels;

    public CombatSquadModel CombatSquadPrefab;
    public CombatPersonModel CompatPersonPrefab;
    public CombatTerrainNode HexPrefab;
    public Transform Parent;
    public ShootFlash ShootFlashPrefab;
    public BulletTracer ShootTracerPrefab;

    public CombatLoader()
    {
        _nodeModels = new List<CombatTerrainNode>();
        _squadModels = new List<CombatSquadModel>();
    }

    [UsedImplicitly]
    private void Start()
    {
        var map = new FixedMap(25);
        foreach (var node in map.Nodes)
        {
            var hexObject = Instantiate(HexPrefab, Parent);

            var position = HexHelper.ConvertToWorld(node.Offset.X, node.Offset.Y);

            hexObject.transform.position = new Vector3(position[0] * 20, position[1] * 20, 0);

            hexObject.Init(node);

            _nodeModels.Add(hexObject);

            hexObject.Clicked += HexObject_Clicked;
        }

        var nameGenerator = new IdNameGenerator();

        for (var i = 0; i < 6; i++)
        {
            var node = map.Nodes.Skip(i * 3 + 1).First();
            var personList = new List<ICombatPerson>();

            for (var j = 0; j < 5; j++)
            {
                var person = new CombatPerson(nameGenerator);
                personList.Add(person);
            }

            var squad = new CombatSquad(node, personList.ToArray(), i < 2 ? Player.Human : Player.Cpu);
            _combatService.SquadManager.Add(squad);

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
                combatPersonModel.HoverEnter += CombatPersonModelOnHoverEnter;
                combatPersonModel.HoverExit += CombatPersonModelOnHoverExit;

                combatPersonModel.Init(combatPerson);
            }

            squadObject.Init(squad, personModelList.ToArray());
            _squadModels.Add(squadObject);
        }

        _combatEventBus.EventRegistered += CombatEventBusOnEventRegistered;
    }

    private void CombatPersonModelOnHoverExit(object sender, EventArgs e)
    {
        var hoverPersonModel = (CombatPersonModel) sender;
        var clickedSquadModel = FindSquadClientModel(hoverPersonModel);

        if (clickedSquadModel == _combatStateManager.HoverSquad)
        {
            _combatStateManager.HoverSquad = null;
        }
    }

    private void CombatPersonModelOnHoverEnter(object sender, EventArgs e)
    {
        var hoverPersonModel = (CombatPersonModel) sender;
        var clickedSquadModel = FindSquadClientModel(hoverPersonModel);
        _combatStateManager.HoverSquad = clickedSquadModel;
    }

    private void CombatEventBusOnEventRegistered(object sender, EventArgs e)
    {
        var combatEvents = _combatEventBus.Events.ToArray();

        var deadPersonModels = new HashSet<CombatPersonModel>();
        foreach (var combatEvent in combatEvents)
        {
            var senderPerson = combatEvent.Sender;
            ProcessAttackEvent(combatEvent, senderPerson, deadPersonModels);
        }

        foreach (var combatPersonModel in deadPersonModels)
        {
            combatPersonModel.ProcessDead();

            foreach (var combatSquadModel in _squadModels)
            {
                if (combatSquadModel.PersonModels.Contains(combatPersonModel))
                {
                    combatSquadModel.DeadPerson(combatPersonModel);
                }
            }
        }
    }

    private void ProcessAttackEvent(ICombatEvent combatEvent, ICombatPerson senderPerson,
        HashSet<CombatPersonModel> deadPersonModels)
    {
        if (!(combatEvent is AttackCombatEvent attackEvent))
        {
            return;
        }

        var senderModel = FindCombatPersonModel(senderPerson);
        var targetModel = FindCombatPersonModel(attackEvent.Target);

        CreateWeaponTracer(senderModel.transform.position, targetModel.transform.position);
        CreateShootFlash(senderModel.transform.position);
        ShakeCamera(.5f, .05f);

        if (attackEvent.TargetIsDead)
        {
            deadPersonModels.Add(targetModel);
        }
    }

    private CombatPersonModel FindCombatPersonModel(ICombatPerson person)
    {
        foreach (var combatSquadModel in _squadModels)
        {
            foreach (var combatPersonModel in combatSquadModel.PersonModels)
            {
                if (combatPersonModel.CombatPerson == person)
                {
                    return combatPersonModel;
                }
            }
        }

        throw new InvalidOperationException();
    }

    private void CombatPersonModelOnClicked(object sender, EventArgs e)
    {
        var combatPersonModel = (CombatPersonModel) sender;
        var clickedSquad = FindSquadClientModel(combatPersonModel);

        if (clickedSquad?.Squad.Player == Player.Human)
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

    private ISquadClientModel FindSquadClientModel(CombatPersonModel combatPersonModel)
    {
        ISquadClientModel clickedSquad = null;
        foreach (var combatSquadModel in _squadModels)
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

        return clickedSquad;
    }

    private void HexObject_Clicked(object sender, EventArgs e)
    {
        var nodeModel = (CombatTerrainNode) sender;
        _combatStateManager.HoverNode = nodeModel;
        if (_combatStateManager.SelectedSquad != null)
        {
            if (_moveCommand.CanExecute())
            {
                _commandManager.RegisterCommand(_moveCommand);
            }
        }
    }

    [UsedImplicitly]
    private void Update()
    {
        foreach (var squad in _squadModels)
        {
            var nodeModel = _nodeModels.Single(x => x.Node == squad.Squad.Node);
            squad.transform.position = nodeModel.transform.position;
        }
    }


    private void CreateShootFlash(Vector3 spawnPosition)
    {
        var flashObject = Instantiate(ShootFlashPrefab, Parent);
        flashObject.transform.position = spawnPosition;
    }

    private void CreateWeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        var tracer = Instantiate(ShootTracerPrefab, Parent);
        tracer.FromPosition = fromPosition;
        tracer.TargetPosition = targetPosition;
    }


    public static void ShakeCamera(float intensity, float timer)
    {
        //Vector3 lastCameraMovement = Vector3.zero;
        //FunctionUpdater.Create(delegate () {
        //    timer -= Time.unscaledDeltaTime;
        //    Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
        //    Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
        //    lastCameraMovement = randomMovement;
        //    return timer <= 0f;
        //}, "CAMERA_SHAKE");
    }
}