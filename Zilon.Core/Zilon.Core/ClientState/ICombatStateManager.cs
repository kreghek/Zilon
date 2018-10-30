using JetBrains.Annotations;

namespace Zilon.Core.ClientState
{
    public interface ICombatStateManager
    {
        ISquadClientModel SelectedSquad { get; [PublicAPI] set; }

        ISquadClientModel HoverSquad { get; [PublicAPI] set; }

        ITerrainNodeClientModel HoverNode { get; [PublicAPI] set; }
    }
}
