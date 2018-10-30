using System;
using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public class SkillUsedEventArgs: EventArgs
    {
        public ICombatPerson Target { get; }

        [ExcludeFromCodeCoverage]
        public SkillUsedEventArgs(ICombatPerson rolledPerson)
        {
            Target = rolledPerson;
        }
    }
}
