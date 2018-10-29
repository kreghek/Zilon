namespace Zilon.Core.ClientState
{
    public class CombatStateManager : ICombatStateManager
    {
        public ISquadClientModel SelectedSquad { get; set; }
        public ITerrainNodeClientModel HoverNode { get; set; }
        public ISquadClientModel HoverSquad { get; set; }
    }
}
