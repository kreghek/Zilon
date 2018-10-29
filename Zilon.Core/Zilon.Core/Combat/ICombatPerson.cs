using System;

namespace Zilon.Core.Combat
{
    public interface ICombatPerson
    {
        int HitPoints { get; }
        void TakeDamage(int value);
        void UseSkill(ICombatSquad targetSquad);
        event EventHandler<TakeDamageEventArgs> TakenDamage;
    }
}