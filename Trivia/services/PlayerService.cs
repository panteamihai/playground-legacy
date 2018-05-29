using System;
using System.Collections.Generic;
using System.Linq;
using trivia.models;

namespace trivia.services
{
    public interface IPlayerService
    {
        Player Current { get; }

        int Count { get; }

        void Add(string name);

        void GiveTurnToNextPlayer();

        void Move(int offset);

        void CollectOneCoin();

        bool HasCurrentPlayerWon();

        void PenalizeCurrentPlayer();

        bool CanTemporarilyOvercomePenalty(int roll);

        bool HasCurrentPlayerIncurredPenalty();

        bool HasCurrentPlayerTemporarilyOvercomePenalty();
    }

    public class PlayerService : IPlayerService
    {
        private readonly ILocationService _locationService;
        private readonly ICoinService _coinService;

        private readonly IPenaltyService _penaltyService;
        private readonly IList<Player> _players = new List<Player>();

        private int _currentOrdinal;

        public PlayerService() : this(new LocationService(), new CoinService(), new PenaltyService()) { }

        public PlayerService(ILocationService locationService, ICoinService coinService, IPenaltyService penaltyService)
        {
            _locationService = locationService;
            _coinService = coinService;
            _penaltyService = penaltyService;
        }

        public Player Current => _players.Single(p => p.Ordinal == _currentOrdinal);

        public int Count => _players.Count;

        public void Add(string name)
        {
            var player = new Player(name, _players.Count);
            _players.Add(player);

            Console.WriteLine(player.Name + " was added");
            Console.WriteLine("They are player number " + Count);
        }

        public void GiveTurnToNextPlayer()
        {
            if(_players.Count == 0)
                throw new InvalidOperationException();

            _penaltyService.ResetHope(Current);

            _currentOrdinal = _currentOrdinal + 1;
            if (_currentOrdinal == _players.Count) _currentOrdinal = 0;
        }

        public void Move(int offset)
        {
            if (_players.Count == 0)
                throw new InvalidOperationException();

            var newLocation = _locationService.AdvanceBy(Current.Location, offset);
            Console.WriteLine(Current + "'s new location is " + newLocation);

            Current.MoveTo(newLocation);
        }

        public void CollectOneCoin()
        {
            var coinBalance = _coinService.Accumulate(Current.CoinBalance, 1);
            Console.WriteLine(Current + " now has " + coinBalance + " Gold Coins.");

            Current.UpdateBalance(coinBalance);
        }

        public void PenalizeCurrentPlayer()
        {
            Console.WriteLine(Current + " was sent to the penalty box");
            _penaltyService.Incur(Current);
        }

        public bool CanTemporarilyOvercomePenalty(int roll)
        {
            if (_penaltyService.CanTemporarilyOvercomePenalty(roll))
            {
                _penaltyService.TemporarilyOvercome(Current);
                Console.WriteLine(Current + " is getting out of the penalty box");
                return true;
            }

            Console.WriteLine(Current + " is not getting out of the penalty box");
            return false;
        }

        public bool HasCurrentPlayerIncurredPenalty()
        {
            return _penaltyService.HasIncurredPenalty(Current);
        }

        public bool HasCurrentPlayerTemporarilyOvercomePenalty()
        {
            return _penaltyService.HasTemporarilyOvercomePenalty(Current);
        }

        public bool HasCurrentPlayerWon()
        {
            return _coinService.HasWinningThresholdBeenReached(Current.CoinBalance);
        }
    }
}
