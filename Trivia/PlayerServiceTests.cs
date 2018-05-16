using NUnit.Framework;
using System;

namespace Trivia
{
    public class PlayerServiceTests
    {
        private const string PlayerOneName = "Player One";
        private const string PlayerTwoName = "Player Two";
        private const string PlayerThreeName = "Player Three";

        private static readonly Player PlayerOne = new Player(PlayerOneName, 0);
        private static readonly Player PlayerTwo = new Player(PlayerTwoName, 1);
        private static readonly Player PlayerThree = new Player(PlayerThreeName, 2);

        [Test]
        public void WhenInitializingService_Count_IsZero()
        {
            var playerService = new PlayerService();

            Assert.That(playerService.Count, Is.Zero);
        }

        [Test]
        public void WhenInitializingService_CurrentPlayerIndex_IsZero()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { var x = playerService.Current.Ordinal; });
        }

        [Test]
        public void WhenInitializingService_AccessingCurrentPlayer_Throws()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { var x = playerService.Current; });
        }

        [Test]
        public void GivenFreshlyInitializingService_TryingToChangePlayers_Throws()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { playerService.ChangePlayer(); });
        }

        [Test]
        public void GivenFreshlyInitializedService_AddingOnePlayer_IncreasesCount()
        {
            var playerService = new PlayerService();

            playerService.AddPlayer(PlayerOneName);

            Assert.That(playerService.Count, Is.EqualTo(1));
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingOnePlayer_CurrentPlayer_ReturnsTheAddedPlayer()
        {
            var playerService = new PlayerService();

            playerService.AddPlayer(PlayerOneName);

            Assert.That(playerService.Current, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingOnePlayer_CurrentPlayerIndex_ReturnsZero()
        {
            var playerService = new PlayerService();

            playerService.AddPlayer(PlayerOneName);

            Assert.That(playerService.Current.Ordinal, Is.Zero);
        }

        [Test]
        public void GivenServiceWithOnePlayer_AfterAddingASecondPlayer_CurrentPlayer_StillReturnsTheFirstAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);

            playerService.AddPlayer(PlayerTwoName);

            Assert.That(playerService.Current, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenServiceWithOnePlayer_AfterAddingASecondPlayer_CurrentPlayerIndex_StillReturnsZero()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);

            playerService.AddPlayer(PlayerTwoName);

            Assert.That(playerService.Current.Ordinal, Is.Zero);
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingTwoPlayers_Count_ReturnsTwo()
        {
            var playerService = new PlayerService();

            playerService.AddPlayer(PlayerOneName);
            playerService.AddPlayer(PlayerTwoName);

            Assert.That(playerService.Count, Is.EqualTo(2));
        }

        [Test]
        public void GivenTwoPlayers_WhenChangingPlayer_CurrentPlayer_ReturnsTheSecondAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);
            playerService.AddPlayer(PlayerTwoName);

            playerService.ChangePlayer();

            Assert.That(playerService.Current, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenThreePlayers_WithTheSecondPlayerAsTheCurrentPlayer_WhenChangingPlayer_CurrentPlayer_ReturnsTheThirdAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);
            playerService.AddPlayer(PlayerTwoName);
            playerService.AddPlayer(PlayerThreeName);
            playerService.ChangePlayer();

            playerService.ChangePlayer();

            Assert.That(playerService.Current, Is.EqualTo(PlayerThree));
        }

        [Test]
        public void GivenThreePlayers_WithTheThirdPlayerAsTheCurrentPlayer_WhenChangingPlayer_CurrentPlayer_ReturnsTheFirstAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);
            playerService.AddPlayer(PlayerTwoName);
            playerService.AddPlayer(PlayerThreeName);
            playerService.ChangePlayer();
            playerService.ChangePlayer();

            playerService.ChangePlayer();

            Assert.That(playerService.Current, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void WhenInitializingService_MovingTheCurrentPlayer_Throws()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { playerService.MoveCurrentPlayer(1); });
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void GivenServiceWithOnePlayer_MovingByInvalidOffset_Throws(int offset)
        {
            var playerService = new PlayerService();

            playerService.AddPlayer(PlayerOneName);

            Assert.Throws<ArgumentException>(() => { playerService.MoveCurrentPlayer(offset); });
        }

        [Test]
        public void GivenServiceWithOnePlayer_MovingByPositiveAmount_UpdatesLocationBySameAmount()
        {
            var playerService = new PlayerService();
            playerService.AddPlayer(PlayerOneName);

            playerService.MoveCurrentPlayer(5);

            Assert.That(playerService.Current.Location, Is.EqualTo(new Location(5)));
        }
    }
}
