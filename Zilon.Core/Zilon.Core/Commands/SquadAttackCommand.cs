using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public class SquadAttackCommand : ISquadAttackCommand
    {
        private readonly ICombatStateManager _combatStateManager;
        private readonly ICombatService _combatService;
        private readonly ICombatEventBus _eventBus;

        [ExcludeFromCodeCoverage]
        public SquadAttackCommand(ICombatStateManager combatStateManager,
            ICombatService combatService,
            ICombatEventBus eventBus)
        {
            _combatStateManager = combatStateManager;
            _combatService = combatService;
            _eventBus = eventBus;
        }

        public void Execute()
        {
            var squad = _combatStateManager.SelectedSquad.Squad;
            var targetSquad = _combatStateManager.HoverSquad.Squad;
            var events = _combatService.UseSkill(squad, targetSquad);
            _eventBus.RegisterEvents(events);
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
