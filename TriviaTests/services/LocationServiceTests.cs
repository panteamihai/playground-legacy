using NUnit.Framework;
using System;
using trivia.models;
using trivia.services;

namespace trivia.tests.services
{
    public class LocationServiceTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void NegativeValues_AreForbidden_WhenAdvancing(int invalidOffset)
        {
            var locationService = new LocationService();

            Assert.Throws<ArgumentException>(
                () =>
                {
                    var x = locationService.AdvanceBy(Location.Start, invalidOffset);
                });
        }

        [Test]
        public void TheLocationValue_ResetsAtBoundary()
        {
            var locationService = new LocationService();

            var oneOverBoundary = locationService.AdvanceBy(Location.Start, 12);

            Assert.That(Location.Start, Is.EqualTo(oneOverBoundary));
        }

        [TestCase(0, 1)]
        [TestCase(0, 12)]
        [TestCase(1, 24)]
        [TestCase(5, 2)]
        [TestCase(5, 8)]
        [TestCase(12, 3)]
        [TestCase(12, 21)]
        public void GivenValidOffset_WhenAdvancing_FinalPosition_IsSumModuloTwelve(int initialPosition, int offset)
        {
            var locationService = new LocationService();
            var initialLocation = new Location(initialPosition);

            var finalPosition = locationService.AdvanceBy(initialLocation, offset);

            Assert.That(finalPosition, Is.EqualTo(new Location((initialPosition + offset) % 12)));
        }
    }
}
