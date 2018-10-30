using System;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    public interface ICombatPerson
    {
        int HitPoints { get; }
        void TakeDamage(int value);
        void UseSkill(ICombatSquad targetSquad);

        [PublicAPI]
        event EventHandler<TakeDamageEventArgs> TakenDamage;
    }
}