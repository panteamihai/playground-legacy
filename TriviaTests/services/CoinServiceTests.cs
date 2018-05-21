using NUnit.Framework;
using System;
using trivia.models;
using trivia.services;

namespace trivia.tests.services
{
    public class CoinServiceTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void NegativeValues_AreForbidden_WhenAccumulating(int invalidWinnings)
        {
            var coinService = new CoinService();

            Assert.Throws<ArgumentException>(
                () =>
                {
                    var x = coinService.Accumulate(CoinBalance.Empty, invalidWinnings);
                });
        }

        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        public bool WhenWinThresholdIsReached_HasWinningThresholdBeenReached_ReturnsTrue(int winnings)
        {
            var coinService = new CoinService();
            var currentBalance = new CoinBalance(4);

            var winningBalance = coinService.Accumulate(currentBalance, winnings);

            return coinService.HasWinningThresholdBeenReached(winningBalance);
        }

        [TestCase(0, 1)]
        [TestCase(0, 12)]
        [TestCase(1, 24)]
        [TestCase(5, 2)]
        [TestCase(5, 8)]
        [TestCase(12, 3)]
        [TestCase(12, 21)]
        public void GivenValidInitialBalance_WhenAccumulating_FinalBalance_IsSumOfInitialValueAndWinnings(int initial, int winnings)
        {
            var coinService = new CoinService();
            var initialBalance = new CoinBalance(initial);

            var finalBalance = coinService.Accumulate(initialBalance, winnings);

            Assert.That(finalBalance, Is.EqualTo(new CoinBalance(initial + winnings)));
        }
    }
}
