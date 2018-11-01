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

    private readonly Dictionary<HighlightType, HighlightData> _highlight;

    public SelectionMarkerManager()
    {
        _highlight = new Dictionary<HighlightType, HighlightData>
        {
            { HighlightType.Own, new HighlightData() },
            { HighlightType.Enemy, new HighlightData() }
        };
    }

    [UsedImplicitly]
    public void Update()
    {
        HighlightSquad(_combatStateManager.SelectedSquad, HighlightType.Own);
        HighlightSquad(_combatStateManager.HoverSquad, HighlightType.Enemy);
    }

    private void HighlightSquad(ISquadClientModel squadModel, HighlightType type)
    {
        var highlightData = _highlight[type];

        if (highlightData.LastSquadModel != squadModel)
        {
            var personModels = FindObjectsOfType<CombatPersonModel>();
            foreach (var combatPerson in squadModel.Squad.Persons)
            {
                var model = personModels.Single(x => x.CombatPerson == combatPerson);
                var marker = Instantiate(SelectionMarkerPrefab, model.transform);
                highlightData.Markers.Add(marker);

            }

            highlightData.LastSquadModel = squadModel;
        }
        else if (squadModel == null)
        {
            foreach (var selectionMarker in highlightData.Markers)
            {
                Destroy(selectionMarker.gameObject);
            }

            highlightData.Markers.Clear();
        }
    }

    private enum HighlightType
    {
        Own,
        Enemy
    }

    private class HighlightData
    {
        public ISquadClientModel LastSquadModel;

        public readonly List<SelectionMarker> Markers;

        public HighlightData()
        {
            Markers = new List<SelectionMarker>();
        }
    }
}
