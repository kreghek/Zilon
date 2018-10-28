using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;

public class CombatSquadModel : MonoBehaviour, ISquadClientModel
{
    public CombatPersonModel[] PersonModels { get; private set; }

    public ICombatSquad Squad { get; private set; }

    public void Init(ICombatSquad squad, CombatPersonModel[] personModels)
    {
        Squad = squad;
        PersonModels = personModels;
    }
}
