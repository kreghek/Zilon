using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public class CombatEventBus : ICombatEventBus
    {
        public IEnumerable<ICombatEvent> Events { get; private set; }

        public event EventHandler EventRegistered;

        public void RegisterEvents(IEnumerable<ICombatEvent> events)
        {
            Events = events;
            EventRegistered?.Invoke(this, new EventArgs());
        }
    }
}
