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
            diceMock.Setup(x => x.Roll(It.IsAny<int>())).Returns<int>(n => n / 2);
            var dice = diceMock.Object;

            var attackerSquadMock = new Mock<ICombatSquad>();
            AddPersonsToSquadMock(attackerSquadMock);
            var attackerSquad = attackerSquadMock.Object;

            var targetSquadMock = new Mock<ICombatSquad>();
            AddPersonsToSquadMock(targetSquadMock);
            var targetSquad = targetSquadMock.Object;

            var combatService = new CombatService(dice);



            // ACT
            var combatEvents = combatService.UseSkill(attackerSquad, targetSquad);



            // ASSERT
            combatEvents.ElementAt(0).Should().BeOfType<AttackCombatEvent>();
        }

        private void AddPersonsToSquadMock(Mock<ICombatSquad> squadMock)
        {
            squadMock.SetupGet(x => x.Persons).Returns(new[]
            {
                CreatePerson(),
                CreatePerson(),
                CreatePerson(),
                CreatePerson(),
                CreatePerson()
            });
        }

        private ICombatPerson CreatePerson()
        {
            var personMock = new Mock<ICombatPerson>();
            personMock.SetupGet(x => x.HitPoints).Returns(10);
            return personMock.Object;
        }
    }
}