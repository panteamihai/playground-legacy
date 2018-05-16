using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public interface IPlayerService
    {
        Player Current { get; }

        int Count { get; }
    }

    public class PlayerService : IPlayerService
    {
        private readonly IList<Player> _players = new List<Player>();

        private int _currentOrdinal;

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

        public void MoveCurrentPlayer(int offset)
        {
            if (_players.Count == 0)
                throw new InvalidOperationException();

            if (offset <= 0)
                throw new ArgumentException();

            Current.Move(offset);
            Console.WriteLine(Current.Name + "'s new location is " + Current.Location);
        }
    }
}
