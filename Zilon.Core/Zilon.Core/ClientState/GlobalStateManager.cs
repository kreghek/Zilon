namespace Zilon.Core.ClientState
{
    public class GlobalStateManager : IGlobalState
    {
        public IArmyClientModel SelectedArmy { get; set; }
        public ITerrainNodeClientModel HoverNode { get; set; }
    }
}
