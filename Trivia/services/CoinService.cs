using System;
using trivia.models;

namespace trivia.services
{
    public interface ICoinService
    {
        CoinBalance Accumulate(CoinBalance current, int winnings);

        bool HasWinningThresholdBeenReached(CoinBalance currentBalance);
    }

    public class CoinService : ICoinService
    {
        private const int WinThreshold = 6;

        public CoinBalance Accumulate(CoinBalance current, int winnings)
        {
            if (winnings <= 0)
                throw new ArgumentException();

            return new CoinBalance(current.Value + winnings);
        }

        public bool HasWinningThresholdBeenReached(CoinBalance currentBalance)
        {
            return currentBalance.Value >= WinThreshold;
        }
    }
}
