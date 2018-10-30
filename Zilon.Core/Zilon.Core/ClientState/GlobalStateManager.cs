using JetBrains.Annotations;

namespace Zilon.Core.ClientState
{
    [PublicAPI]
    public class GlobalStateManager : IGlobalStateManager
    {
        public IArmyClientModel SelectedArmy { get; set; }
        public ITerrainNodeClientModel HoverNode { get; set; }
    }
}
