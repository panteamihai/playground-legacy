using NUnit.Framework;
using System;
using trivia.models;

namespace trivia.tests.models
{
    public class LocationTests
    {
        [TestCase(-1)]
        [TestCase(-20)]
        public void NegativeValues_AreForbidden_InCtor(int invalidValue)
        {
            Assert.Throws<ArgumentException>(() => { var x = new Location(invalidValue); });
        }

        [Test]
        public void TheStartPosition_IsZero()
        {
            Assert.That(Location.Start, Is.EqualTo(new Location(0)));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(22)]
        public void GivenValidCtorParameter_Value_IsSet(int validValue)
        {
            Assert.That(new Location(validValue).Value, Is.EqualTo(validValue));
        }
    }
}
