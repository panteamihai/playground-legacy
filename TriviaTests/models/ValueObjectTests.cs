using NUnit.Framework;
using System;

namespace trivia.tests.models
{
    public class ValueObjectTests
    {
        [TestCase(-1)]
        [TestCase(-13)]
        public void GivenNegativeValue_PassedToCtor_Throws(int invalidValue)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var x = new ValueObjectFake(invalidValue);
                });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(22)]
        public void GivenNonNegativeValue_PassedToCtor_IsSet(int validValue)
        {
            Assert.That(new ValueObjectFake(validValue).Value, Is.EqualTo(validValue));
        }
    }
}
