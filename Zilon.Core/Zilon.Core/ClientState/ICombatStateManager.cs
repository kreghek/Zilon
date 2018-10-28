namespace Zilon.Core.ClientState
{
    public interface ICombatStateManager
    {
        ISquadClientModel SelectedSquad { get; set; }
        ITerrainNodeClientModel HoverNode { get; set; }
    }
}
