using System;
using System.Collections.Generic;

namespace Zilon.Core.Combat
{
    public interface ICombatEventBus
    {
        IEnumerable<ICombatEvent> Events { get; }

        void RegisterEvents(IEnumerable<ICombatEvent> events);

        event EventHandler EventRegistered;
    }
}