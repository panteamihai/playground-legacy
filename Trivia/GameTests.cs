using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Trivia
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Verify()
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

            Assert.That(game.HowManyPlayers(), Is.Zero);
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
        public void GivenAGameWithOnePlayer_IsPlayable_ReturnsFalse()
        {
            var game = new Game();
            game.Add("Gheo");

            Assert.That(game.IsPlayable(), Is.False);
        }

        [Test]
        public void GivenAGameWithOnePlayer_HowManyPlayers_ReturnsOne()
        {
            var game = new Game();
            game.Add("Gheo");

            Assert.That(game.HowManyPlayers(), Is.EqualTo(1));
        }

        [Test]
        public void GivenAGameWithOnePlayer_CurrentPlayer_ReturnPlayerName()
        {
            var game = new Game();
            game.Add("Gheo");

           Assert.That(game.CurrentPlayer, Is.EqualTo("Gheo"));
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
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add("Gheo");

            Assert.That(game.WasCorrectlyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_ReturnsTrue()
        {
            var game = new Game();
            game.Add("Gheo");

            Assert.That(game.WasWronglyAnswered(), Is.True);
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasCorrectlyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add("Gheo");

            game.WasCorrectlyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo("Gheo"));
        }

        [Test]
        public void GivenAGameWithOnePlayer_WasWronglyAnswered_CurrentPlayerStayTheSame()
        {
            var game = new Game();
            game.Add("Gheo");

            game.WasWronglyAnswered();

            Assert.That(game.CurrentPlayer, Is.EqualTo("Gheo"));
        }
    }
}
