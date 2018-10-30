using JetBrains.Annotations;

namespace Zilon.Core.Players
{
    public enum Player
    {
        [PublicAPI] Neutrals,
        [PublicAPI] Human,
        [PublicAPI] Cpu
    }
}
