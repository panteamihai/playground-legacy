using NUnit.Framework;
using trivia.models;

namespace trivia.tests.models
{
    public class LocationTests
    {
        [Test]
        public void TheStartPosition_IsZero()
        {
            Assert.That(Location.Start, Is.EqualTo(new Location(0)));
        }

        [TestCase(5, 5, ExpectedResult = true)]
        [TestCase(5, 10, ExpectedResult = false)]
        public bool GivenTwoLocations_WithSameValue_AreEqual(int first, int second)
        {
            return new Location(first).Equals(new Location(second));
        }

        [Test]
        public void GivenLocation_WhenComparingToOtherValueObject_EqualityReturnsFalse()
        {
            Assert.That(new Location(5), Is.Not.EqualTo(new ValueObjectFake(5)));
        }
    }
}
