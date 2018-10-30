using System.Collections.Generic;

using UnityEngine;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;
using Zilon.Core.Players;

public class CombatSquadModel : MonoBehaviour, ISquadClientModel
{
    public CombatPersonModel[] PersonModels { get; private set; }

    public ICombatSquad Squad { get; private set; }

    public Player Player;

    public void Init(ICombatSquad squad, CombatPersonModel[] personModels)
    {
        Squad = squad;
        PersonModels = personModels;

        Player = squad.Player;
    }

    public void DeadPerson(CombatPersonModel person)
    {
        var list = new List<CombatPersonModel>(PersonModels);
        list.Remove(person);
        PersonModels = list.ToArray();
    }
}
