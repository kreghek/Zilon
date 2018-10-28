namespace Zilon.Core.ClientState
{
    public interface IGlobalState
    {
        IArmyClientModel SelectedArmy { get; set; }
        ITerrainNodeClientModel HoverNode { get; set; }
    }
}
