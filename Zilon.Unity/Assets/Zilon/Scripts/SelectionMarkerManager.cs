using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

using Zenject;

using Zilon.Core.ClientState;
// ReSharper disable CheckNamespace

[UsedImplicitly]
public class SelectionMarkerManager : MonoBehaviour
{
    [UsedImplicitly] [Inject] private readonly ICombatStateManager _combatStateManager;

    [UsedImplicitly]
    public Transform World;

    [UsedImplicitly]
    public SelectionMarker SelectionMarkerPrefab;

    private readonly List<SelectionMarker> _markers;
    private ISquadClientModel _lastSquadModel;

    public SelectionMarkerManager()
    {
        _markers = new List<SelectionMarker>();
    }

    [UsedImplicitly]
    void Update()
    {
        if (_lastSquadModel != _combatStateManager.SelectedSquad)
        {
            var personModels = FindObjectsOfType<CombatPersonModel>();
            foreach (var combatPerson in _combatStateManager.SelectedSquad.Squad.Persons)
            {
                var model = personModels.Single(x => x.CombatPerson == combatPerson);
                var marker = Instantiate(SelectionMarkerPrefab, model.transform);
                _markers.Add(marker);

            }

            _lastSquadModel = _combatStateManager.SelectedSquad;
        }
        else if (_combatStateManager.SelectedSquad == null)
        {
            foreach (var selectionMarker in _markers)
            {
                Destroy(selectionMarker.gameObject);
            }

            _markers.Clear();
        }

    }
}
