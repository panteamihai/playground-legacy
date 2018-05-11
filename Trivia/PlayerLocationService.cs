using System;
using System.Collections.Generic;

namespace Trivia
{
    public interface IPlayerLocationService
    {
        int GetCurrentPlayerLocation();

        void SetCurrentPlayerLocation(int value);

        void UpdateCurrentLocation(int roll);
    }

    public class PlayerLocationService
    {
        private readonly List<int> _places = new List<int>();
        private readonly IPlayerService _playerService;

        private int CurrentPlayerIndex => _playerService.CurrentPlayerIndex;

        public PlayerLocationService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public int GetCurrentPlayerLocation()
        {
            if (_places.Count == 0)
                throw new InvalidOperationException();

            return _places[CurrentPlayerIndex];
        }

        public void SetCurrentPlayerLocation(int value)
        {
            if (_places.Count == 0)
                throw new InvalidOperationException();

            _places[CurrentPlayerIndex] = value;
        }

        public void UpdateCurrentLocation(int roll)
        {
            SetCurrentPlayerLocation(GetCurrentPlayerLocation() + roll);
            if (GetCurrentPlayerLocation() > 11) SetCurrentPlayerLocation(GetCurrentPlayerLocation() - 12);

            Console.WriteLine(_playerService.CurrentPlayer + "'s new location is " + GetCurrentPlayerLocation());
        }
    }
}
