using NUnit.Framework;
using System;
using trivia.models;

namespace trivia.tests
{
    [TestFixture]
    public class GameTests
    {
        private static readonly Player PlayerOne = new Player("Gheo", 0);
        private static readonly Player PlayerTwo = new Player("Iuon", 1);

        [Test]
        public void GivenANewGame_IsPlayable_ReturnsFalse()
        {
            var game = new Game();

            Assert.That(game.IsPlayable, Is.False);
        }

        [Test]
        public void GivenANewGame_HowManyPlayers_ReturnsZero()
        {
            var game = new Game();

            Assert.That(game.PlayerCount, Is.Zero);
        }

        [Test]
        public void GivenANewGame_CurrentPlayer_Throws()
        {
            var game = new Game();

            Assert.Throws<InvalidOperationException>(() => { var x = game.CurrentPlayer; });
        }

        [Test]
        public void GivenANewGame_WasCorrectlyAnswered_ThrowsException()
        {
            var game = new Game();

            Assert.Throws<InvalidOperationException>(() => game.WasCorrectlyAnswered());
        }

        [Test]
        public void GivenANewGame_WasWronglyAnswered_ThrowsException()
        {
            var game = new Game();

            Assert.Throws<InvalidOperationException>(() => game.WasWronglyAnswered());
        }

        [Test]
        public void GivenANewGame_WhenRolling_ThenThrows()
        {
            var game = new Game();

            Assert.Throws<InvalidOperationException>(() => game.Roll(5));
        }

        [Test]
        public void GivenAGameWithOnePlayer_IsPlayable_ReturnsFalse()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            Assert.That(game.IsPlayable, Is.False);
        }

        [Test]
        public void GivenAGameWithOnePlayer_HowManyPlayers_ReturnsOne()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            Assert.That(game.PlayerCount, Is.EqualTo(1));
        }

        [Test]
        public void GivenAGameWithOnePlayer_CurrentPlayer_ReturnPlayerName()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

           Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            Assert.That(game.WasCorrectlyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            Assert.That(game.WasWronglyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            game.WasCorrectlyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);

            game.WasWronglyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WhenRolling_ThenThrows()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            Assert.Throws<InvalidOperationException>(() => game.Roll(5));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_IsPlayable_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.That(game.IsPlayable, Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_HowManyPlayers_ReturnsTwo()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.That(game.PlayerCount, Is.EqualTo(2));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_CurrentPlayer_ReturnFirstPlayerName()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasCorrectlyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.That(game.WasCorrectlyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasWronglyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.That(game.WasWronglyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasCorrectlyAnswered_CurrentPlayerChanges()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            game.WasCorrectlyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasWronglyAnswered_CurrentPlayerChanges()
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            game.WasWronglyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WhenCurrentPlayerRolls_ThenCurrentPlayerLocationChangesWithModuloTwelve(
            [Values(1,2,3,4,5,6,7,8,9,10,11,12,13)] int roll)
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            game.Roll(roll);

            Assert.That(game.CurrentPlayerLocation, Is.EqualTo(roll % 12));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WhenCurrentPlayerRollsNegativeNumber_ThenThrows(
            [Values(-1, -2)] int negativeRoll)
        {
            var game = new Game();
            game.Add(PlayerOne.Name);
            game.Add(PlayerTwo.Name);

            Assert.Throws<ArgumentException>(() => game.Roll(negativeRoll));
        }
    }
}
