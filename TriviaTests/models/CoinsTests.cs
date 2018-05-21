using NUnit.Framework;
using trivia.models;

namespace trivia.tests.models
{
    public class CoinsTests
    {
        [Test]
        public void TheEmptyBalance_IsZero()
        {
            Assert.That(CoinBalance.Empty, Is.EqualTo(new CoinBalance(0)));
        }

        [TestCase(5, 5, ExpectedResult = true)]
        [TestCase(5, 10, ExpectedResult = false)]
        public bool GivenTwoCoinBalances_WithSameValue_AreEqual(int first, int second)
        {
            return new CoinBalance(first).Equals(new CoinBalance(second));
        }

        [Test]
        public void GivenCoinBalance_WhenComparingToOtherValueObject_EqualityReturnsFalse()
        {
            Assert.That(new CoinBalance(5), Is.Not.EqualTo(new ValueObjectFake(5)));
        }
    }
}
