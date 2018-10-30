using JetBrains.Annotations;

namespace Zilon.Core.ClientState
{
    [PublicAPI]
    public class CombatStateManager : ICombatStateManager
    {
        public ISquadClientModel SelectedSquad { get; set; }
        public ITerrainNodeClientModel HoverNode { get; set; }
        public ISquadClientModel HoverSquad { get; set; }
    }
}
