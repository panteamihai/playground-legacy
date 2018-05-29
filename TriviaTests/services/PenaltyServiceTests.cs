using NUnit.Framework;
using System;
using System.Collections.Generic;
using trivia.models;
using trivia.services;

namespace trivia.tests.services
{
    public class PenaltyServiceTests
    {
        private Player _player;
        private PenaltyService _penaltyService;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Spe", 0);
            _penaltyService = new PenaltyService();
        }

        [Test]
        public void GivenPlayerWithNoPenalty_IfIncurred_PlayerIsPenalized()
        {
            _penaltyService.Incur(_player);

            Assert.That(_penaltyService.HasIncurredPenalty(_player), Is.True);
        }

        [Test]
        public void GivenPlayerWithPenalty_PlayerCanTemporarilyOvercomesPenalty()
        {
            _penaltyService.Incur(_player);

            _penaltyService.TemporarilyOvercome(_player);

            Assert.That(_penaltyService.HasTemporarilyOvercomePenalty(_player), Is.True);
        }

        [Test]
        public void GivenPlayerWithNoPenalty_PlayerCannotTemporarilyOvercomesPenalty()
        {
            Assert.Throws<InvalidOperationException>(() => _penaltyService.TemporarilyOvercome(_player));
        }

        [Test]
        public void GivenPlayerWithTemporarilyOvercomePenalty_PlayerCanHaveItsHopeReset()
        {
            _penaltyService.Incur(_player);

            _penaltyService.TemporarilyOvercome(_player);

            Assert.That(_penaltyService.HasTemporarilyOvercomePenalty(_player), Is.True);
        }

        [Test]
        public void GivenPlayerWithNoPenalty_PlayerCannotHaveItsHopeReset()
        {
            _penaltyService.ResetHope(_player);

            Assert.That(_penaltyService.HasIncurredPenalty(_player), Is.False);
            Assert.That(_penaltyService.HasTemporarilyOvercomePenalty(_player), Is.False);
        }

        [TestCase(1, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(13, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(8, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = false)]
        public bool WhenRollingOddNumbers_PlayerCanTemporarilyOvercomePenalty(int roll)
        {
            return _penaltyService.CanTemporarilyOvercomePenalty(roll);
        }

        private static IEnumerable<TestCaseData> HasIncurredTestCases
        {
            get
            {
                var penaltyService = new PenaltyService();

                var penaltyFreePlayer = new Player("Monkey", 0);

                var playerWithPenalty = new Player("See", 1);
                penaltyService.Incur(playerWithPenalty);

                var playerWithTemporarilyOvercomePenalty = new Player("Do", 2);
                penaltyService.Incur(playerWithTemporarilyOvercomePenalty);
                penaltyService.TemporarilyOvercome(playerWithTemporarilyOvercomePenalty);

                yield return new TestCaseData(penaltyFreePlayer).Returns(false);
                yield return new TestCaseData(playerWithTemporarilyOvercomePenalty).Returns(true);
                yield return new TestCaseData(playerWithPenalty).Returns(true);
            }
        }

        [TestCaseSource(nameof(HasIncurredTestCases))]
        public bool HasIncurredPenalty_ReturnsCorrectly(Player player)
        {
            return _penaltyService.HasIncurredPenalty(player);
        }

        private static IEnumerable<TestCaseData> HasTemporarilyOvercomeTestCases
        {
            get
            {
                var penaltyService = new PenaltyService();

                var penaltyFreePlayer = new Player("Monkey", 0);

                var playerWithPenalty = new Player("See", 1);
                penaltyService.Incur(playerWithPenalty);

                var playerWithTemporarilyOvercomePenalty = new Player("Do", 2);
                penaltyService.Incur(playerWithTemporarilyOvercomePenalty);
                penaltyService.TemporarilyOvercome(playerWithTemporarilyOvercomePenalty);

                yield return new TestCaseData(penaltyFreePlayer).Returns(false);
                yield return new TestCaseData(playerWithTemporarilyOvercomePenalty).Returns(true);
                yield return new TestCaseData(playerWithPenalty).Returns(false);
            }
        }

        [TestCaseSource(nameof(HasTemporarilyOvercomeTestCases))]
        public bool HasTemporarilyOvercomePenalty_ReturnsCorrectly(Player player)
        {
            return _penaltyService.HasTemporarilyOvercomePenalty(player);
        }
    }
}
