using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public interface ICombatEvent
    {
        ICombatPerson Sender { get; }
    }
}