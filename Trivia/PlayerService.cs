using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Trivia
{
    public interface IPlayerService
    {
        string CurrentPlayer { get; }

        int CurrentPlayerIndex { get; }

        int PlayerCount { get; }
    }

    public class PlayerService : IPlayerService
    {
        private readonly Subject<Unit> _playerAdded = new Subject<Unit>();
        private readonly List<string> _players = new List<string>();

        private int _index;

        public IObservable<Unit> PlayerAdded => _playerAdded.AsObservable();

        public string CurrentPlayer => _players[CurrentPlayerIndex];

        public int CurrentPlayerIndex
        {
            get
            {
                if(_players.Count == 0)
                    throw new InvalidOperationException();

                return _index;
            }
        }

        public int PlayerCount => _players.Count;

        public void Add(string playerName)
        {
            _players.Add(playerName);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + PlayerCount);
        }

        public void ChangePlayer()
        {
            if(_players.Count == 0)
                throw new InvalidOperationException();

            _index = _index + 1;
            if (_index == _players.Count) _index = 0;
        }
    }
}
