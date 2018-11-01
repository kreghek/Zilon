using System.Diagnostics.CodeAnalysis;
using System.Linq;

using JetBrains.Annotations;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public class SquadMoveCommand : ISquadMoveCommand
    {
        private readonly ICombatStateManager _combatStateManager;
        private readonly ICombatService _combatService;

        [ExcludeFromCodeCoverage]
        public SquadMoveCommand(ICombatStateManager globalState,
            ICombatService combatService)
        {
            _combatStateManager = globalState;
            _combatService = combatService;
        }

        public void Execute()
        {
            var squad = _combatStateManager.SelectedSquad.Squad;
            var node = _combatStateManager.HoverNode.Node;
            squad.MoveToNode(node);
        }

        public bool CanExecute()
        {
            if (_combatStateManager.SelectedSquad == null)
            {
                return false;
            }

            var node = _combatStateManager.HoverNode.Node;
            var squadsInNode = _combatService.SquadManager.Items.SingleOrDefault(x => x.Node == node);
            if (squadsInNode != null)
            {
                return false;
            }

            return true;
        }
    }
}
