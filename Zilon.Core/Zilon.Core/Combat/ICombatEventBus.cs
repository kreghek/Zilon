using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public interface ICombatEventBus
    {
        [PublicAPI]
        IEnumerable<ICombatEvent> Events { get; }

        [PublicAPI]
        void RegisterEvents(IEnumerable<ICombatEvent> events);

        [PublicAPI]
        event EventHandler EventRegistered;
    }
}