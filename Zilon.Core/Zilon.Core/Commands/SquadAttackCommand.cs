using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public class SquadAttackCommand : ISquadAttackCommand
    {
        private readonly ICombatStateManager _combatStateManager;

        [ExcludeFromCodeCoverage]
        public SquadAttackCommand(ICombatStateManager combatStateManager)
        {
            _combatStateManager = combatStateManager;
        }

        public void Execute()
        {
            var squad = _combatStateManager.SelectedSquad.Squad;
            var targetSquad = _combatStateManager.HoverSquad.Squad;
            squad.UseSkill(targetSquad);
        }

        public bool CanExecute()
        {
            if (_combatStateManager.SelectedSquad == null)
            {
                return false;
            }

            if (_combatStateManager.HoverSquad == null)
            {
                return false;
            }

            return true;
        }
    }
}
