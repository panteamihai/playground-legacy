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
    }

    public class PlayerService : IPlayerService
    {
        private readonly ILocationService _locationService;
        private readonly IList<Player> _players = new List<Player>();

        private int _currentOrdinal;

        public PlayerService() : this(new LocationService()) { }

        public PlayerService(ILocationService locationService)
        {
            _locationService = locationService;
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
    }
}
