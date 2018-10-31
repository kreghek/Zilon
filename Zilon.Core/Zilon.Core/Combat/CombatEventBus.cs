using System;
using System.Collections.Generic;

namespace Zilon.Core.Combat
{
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
