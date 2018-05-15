using NUnit.Framework;
using System;

namespace Trivia
{
    public class PlayerTests
    {
        [TestCase("")]
        [TestCase(null)]
        public void GivenPlayer_WithInvalidName_CtorThrows(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => { var newPlayer = new Player(invalidName, 12); });
        }

        [TestCase(-1)]
        [TestCase(-24)]
        public void GivenPlayer_WithInvalidOrdinal_CtorThrows(int invalidOrdinal)
        {
            Assert.Throws<ArgumentException>(() => { var newPlayer = new Player("Valid name", invalidOrdinal); });
        }

        [TestCase("Amy", 0)]
        [TestCase("Anna", 2)]
        public void GivenPlayer_WithValidNameAndOrdinal_LocationReturnsStartLocation(string name, int ordinal)
        {
            var newPlayer = new Player(name, ordinal);

            Assert.That(newPlayer.Location, Is.EqualTo(Location.Start));
        }
    }
}
