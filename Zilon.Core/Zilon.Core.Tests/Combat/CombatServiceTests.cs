using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zilon.Core.Combat;
using Zilon.Core.Dices;

namespace Zilon.Core.Tests.Combat
{
    [TestFixture()]
    public class CombatServiceTests
    {
        [Test()]
        public void UseSkillTest()
        {
            var diceMock = new Mock<IDice>();
            var dice = diceMock.Object;

            var attackerSquadMock = new Mock<ICombatSquad>();
            var attackerSquad = attackerSquadMock.Object;

            var targetSquadMock = new Mock<ICombatSquad>();
            var targetSquad = targetSquadMock.Object;

            var combatService = new CombatService(dice);



            // ACT
            var combatEvents = combatService.UseSkill(attackerSquad, targetSquad);



            // ASSERT
            combatEvents.ElementAt(0).Should().BeOfType<AttackCombatEvent>();
        }
    }
}