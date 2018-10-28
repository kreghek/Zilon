namespace Zilon.Core.ClientState
{
    public interface IGlobalState
    {
        IArmyClientModel SelectedArmy { get; }
        ITerrainNodeClientModel HoverNode { get; }
    }
}
