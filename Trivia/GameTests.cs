using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trivia
{
    [TestFixture]
    public class GameTests
    {
        private const string PlayerOne = "Gheo";
        private const string PlayerTwo = "Iuon";

        [Test]
        public void GivenGame_WhenRunWithGoldenMasterInput_ThenOutputIsTheSameAsGoldenMasterOutput()
        {
            using (var istrm = new FileStream("Input.txt", FileMode.Open, FileAccess.Read))
            using (var gstrm = new FileStream("Output.txt", FileMode.Open, FileAccess.Read))
            using (var input = new StreamReader(istrm))
            using (var master = new StreamReader(gstrm))
            using(var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var values = input.ReadToEnd().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                var randomizer = new RandomStub(values);

                for (var i = 0; i < 5000; i++)
                {
                    GameRunner.Run(randomizer);
                    Console.WriteLine("Exiting " + randomizer.Count + Environment.NewLine);
                }

                Assert.That(writer.ToString(), Is.EqualTo(master.ReadToEnd()));
            }
        }

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

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = game.CurrentPlayer;
            });
        }

        [Test]
        public void GivenANewGame_WasCorrectlyAnswered_ThrowsException()
        {
            var game = new Game();

            Assert.Throws<ArgumentOutOfRangeException>(() => game.WasCorrectlyAnswered());
        }

        [Test]
        public void GivenANewGame_WasWronglyAnswered_ThrowsException()
        {
            var game = new Game();

            Assert.Throws<ArgumentOutOfRangeException>(() => game.WasWronglyAnswered());
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
            game.Add(PlayerOne);

            Assert.That(game.IsPlayable, Is.False);
        }

        [Test]
        public void GivenAGameWithOnePlayer_HowManyPlayers_ReturnsOne()
        {
            var game = new Game();
            game.Add(PlayerOne);

            Assert.That(game.PlayerCount, Is.EqualTo(1));
        }

        [Test]
        public void GivenAGameWithOnePlayer_CurrentPlayer_ReturnPlayerName()
        {
            var game = new Game();
            game.Add(PlayerOne);

           Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne);

            Assert.That(game.WasCorrectlyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne);

            Assert.That(game.WasWronglyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add(PlayerOne);

            game.WasCorrectlyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add(PlayerOne);

            game.WasWronglyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WhenRolling_ThenThrows()
        {
            var game = new Game();
            game.Add(PlayerOne);
            Assert.Throws<InvalidOperationException>(() => game.Roll(5));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_IsPlayable_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.That(game.IsPlayable, Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_HowManyPlayers_ReturnsTwo()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.That(game.PlayerCount, Is.EqualTo(2));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_CurrentPlayer_ReturnFirstPlayerName()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerOne));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasCorrectlyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.That(game.WasCorrectlyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasWronglyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.That(game.WasWronglyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasCorrectlyAnswered_CurrentPlayerChanges()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            game.WasCorrectlyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WasWronglyAnswered_CurrentPlayerChanges()
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            game.WasWronglyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo(PlayerTwo));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WhenCurrentPlayerRolls_ThenCurrentPlayerLocationChangesWithModuloTwelve(
            [Values(0,1,2,3,4,5,6,7,8,9,10,11,12,13)] int roll)
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            game.Roll(roll);

            Assert.That(game.CurrentPlayerLocation, Is.EqualTo(roll % 12));
        }

        [Test]
        public void GivenAGameWithTwoPlayers_WhenCurrentPlayerRollsNegativeNumber_ThenThrows(
            [Values(-1, -2)] int negativeRoll)
        {
            var game = new Game();
            game.Add(PlayerOne);
            game.Add(PlayerTwo);

            Assert.Throws<ArgumentException>(() => game.Roll(negativeRoll));
        }
    }
}
