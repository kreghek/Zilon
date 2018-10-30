using JetBrains.Annotations;

namespace Zilon.Core.ClientState
{
    [PublicAPI]
    public interface IGlobalStateManager
    {
        IArmyClientModel SelectedArmy { get; [PublicAPI] set; }
        ITerrainNodeClientModel HoverNode { get; [PublicAPI] set; }
    }
}
