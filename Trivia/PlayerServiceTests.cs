using NUnit.Framework;
using System;

namespace Trivia
{
    public class PlayerServiceTests
    {
        private const string PlayerOne = "Player One";
        private const string PlayerTwo = "Player Two";
        private const string PlayerThree = "Player Three";

        [Test]
        public void WhenInitializingService_PlayerCount_IsZero()
        {
            var playerService = new PlayerService();

            Assert.That(playerService.PlayerCount, Is.Zero);
        }

        [Test]
        public void WhenInitializingService_CurrentPlayerIndex_IsZero()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { var x = playerService.CurrentPlayerIndex; });
        }

        [Test]
        public void WhenInitializingService_AccessingCurrentPlayer_Throws()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { var x = playerService.CurrentPlayer; });
        }

        [Test]
        public void GivenFreshlyInitializingService_TryingToChangePlayers_Throws()
        {
            var playerService = new PlayerService();

            Assert.Throws<InvalidOperationException>(() => { playerService.ChangePlayer(); });
        }

        [Test]
        public void GivenFreshlyInitializedService_AddingOnePlayer_IncreasesPlayerCount()
        {
            var playerService = new PlayerService();

            playerService.Add(PlayerOne);

            Assert.That(playerService.PlayerCount, Is.EqualTo(1));
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingOnePlayer_CurrentPlayer_ReturnsTheAddedPlayer()
        {
            var playerService = new PlayerService();

            playerService.Add(PlayerOne);

            Assert.That(playerService.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingOnePlayer_CurrentPlayerIndex_ReturnsZero()
        {
            var playerService = new PlayerService();

            playerService.Add(PlayerOne);

            Assert.That(playerService.CurrentPlayerIndex, Is.Zero);
        }

        [Test]
        public void GivenServiceWithOnePlayer_AfterAddingASecondPlayer_CurrentPlayer_StillReturnsTheFirstAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.Add(PlayerOne);

            playerService.Add(PlayerTwo);

            Assert.That(playerService.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenServiceWithOnePlayer_AfterAddingASecondPlayer_CurrentPlayerIndex_StillReturnsZero()
        {
            var playerService = new PlayerService();
            playerService.Add(PlayerOne);

            playerService.Add(PlayerTwo);

            Assert.That(playerService.CurrentPlayerIndex, Is.Zero);
        }

        [Test]
        public void GivenFreshlyInitializedService_AfterAddingTwoPlayers_PlayerCount_ReturnsTwo()
        {
            var playerService = new PlayerService();

            playerService.Add(PlayerOne);
            playerService.Add(PlayerTwo);

            Assert.That(playerService.PlayerCount, Is.EqualTo(2));
        }

        [Test]
        public void GivenTwoPlayers_WhenChangingPlayer_CurrentPlayer_ReturnsTheSecondAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.Add(PlayerOne);
            playerService.Add(PlayerTwo);

            playerService.ChangePlayer();

            Assert.That(playerService.CurrentPlayer, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenThreePlayers_WithTheSecondPlayerAsTheCurrentPlayer_WhenChangingPlayer_CurrentPlayer_ReturnsTheThirdAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.Add(PlayerOne);
            playerService.Add(PlayerTwo);
            playerService.Add(PlayerThree);
            playerService.ChangePlayer();

            playerService.ChangePlayer();

            Assert.That(playerService.CurrentPlayer, Is.EqualTo(PlayerThree));
        }

        [Test]
        public void GivenThreePlayers_WithTheThirdPlayerAsTheCurrentPlayer_WhenChangingPlayer_CurrentPlayer_ReturnsTheFirstAddedPlayer()
        {
            var playerService = new PlayerService();
            playerService.Add(PlayerOne);
            playerService.Add(PlayerTwo);
            playerService.Add(PlayerThree);
            playerService.ChangePlayer();
            playerService.ChangePlayer();

            playerService.ChangePlayer();

            Assert.That(playerService.CurrentPlayer, Is.EqualTo(PlayerOne));
        }
    }
}
