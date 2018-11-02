using System.Linq;

using FluentAssertions;

using Moq;

using NUnit.Framework;

using Zilon.Core.Combat;
using Zilon.Core.Common;
using Zilon.Core.Dices;

namespace Zilon.Core.Tests.Combat
{
    [TestFixture]
    public class CombatServiceTests
    {
        /// <summary>
        /// Тест проверяет, что для каждого персонажа во взводе генерируется событие атаки.
        /// </summary>
        [Test]
        public void UseSkill_PersonSquadAttacks_Returns5EventOfAttack()
        {
            var skillUsageRandomMock = new Mock<ISkillUsageRandomSource>();
            skillUsageRandomMock.Setup(x => x.RollEfficient(It.IsAny<Roll>()))
                .Returns<Roll>(roll => roll.Dice / 2);
            skillUsageRandomMock.Setup(x => x.RollToHit(It.IsAny<Roll>()))
                .Returns<Roll>(roll => 5);
            skillUsageRandomMock.Setup(x => x.RollPersonIndex(It.IsAny<int>()))
                .Returns<int>(personCount => 0);
            var skillUsageRandom = skillUsageRandomMock.Object;

            var attackerSquadMock = new Mock<ICombatSquad>();
            AddPersonsToSquadMock(attackerSquadMock);
            var attackerSquad = attackerSquadMock.Object;

            var targetSquadMock = new Mock<ICombatSquad>();
            AddPersonsToSquadMock(targetSquadMock);
            var targetSquad = targetSquadMock.Object;

            var squadManagerMock = new Mock<IEntityManager<ICombatSquad>>();
            var squadManager = squadManagerMock.Object;

            var combatService = new CombatService(squadManager, skillUsageRandom);



            // ACT
            var combatEvents = combatService.UseSkill(attackerSquad, targetSquad).ToArray();



            // ASSERT
            combatEvents.Should().HaveCount(5);
            foreach (var combatEvent in combatEvents)
            {
                combatEvent.Should().BeOfType<AttackCombatEvent>();
            }
        }

        private static void AddPersonsToSquadMock(Mock<ICombatSquad> squadMock)
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

        private static ICombatPerson CreatePerson()
        {
            var personMock = new Mock<ICombatPerson>();
            personMock.SetupGet(x => x.HitPoints).Returns(10);
            return personMock.Object;
        }
    }
}