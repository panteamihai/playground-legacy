using System;
using System.Collections.Generic;

namespace Trivia
{
    public class Game
    {
        private readonly IQuestionProvider _questionProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly PlayerService _playerService;

        private int _currentPlayerIndex;
        private readonly List<string> _players = new List<string>();

        private readonly bool[] _inPenaltyBox = new bool[6];
        private bool _isGettingOutOfPenaltyBox;

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        public int CurrentPlayerLocation
        {
            get => _places[_currentPlayerIndex];
            private set => _places[_currentPlayerIndex] = value;
        }

        public string CurrentPlayer => _players[_currentPlayerIndex];

        public int PlayerCount => _players.Count;

        public bool IsPlayable => PlayerCount >= 2;

        public Game()
        {
            _categoryProvider = new CategoryProvider();
            _questionProvider = new QuestionProvider(_categoryProvider);
            _playerService = new PlayerService();
        }

        public Game(ICategoryProvider categoryProvider, IQuestionProvider questionProvider)
        {
            _categoryProvider = categoryProvider;
            _questionProvider = questionProvider;
        }

        public void Add(string playerName)
        {
            _players.Add(playerName);
            _places[PlayerCount] = 0;
            _purses[PlayerCount] = 0;
            _inPenaltyBox[PlayerCount] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + PlayerCount);
        }

        public void Roll(int roll)
        {
            if (roll < 0)
                throw new ArgumentException("Invalid roll!");

            if (!IsPlayable)
                throw new InvalidOperationException("Cannot roll if game not playable.");

            Console.WriteLine(CurrentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayerIndex])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(CurrentPlayer + " is getting out of the penalty box");
                    Move(roll);
                }
                else
                {
                    Console.WriteLine(CurrentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                Move(roll);
            }
        }

        private void Move(int roll)
        {
            CurrentPlayerLocation = CurrentPlayerLocation + roll;
            if (CurrentPlayerLocation > 11) CurrentPlayerLocation = CurrentPlayerLocation - 12;

            Console.WriteLine(CurrentPlayer + "'s new location is " + CurrentPlayerLocation);
            AskQuestion();
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayerIndex])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    return Winner("Answer was correct!!!!");
                }

                _currentPlayerIndex = _currentPlayerIndex + 1;
                if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
                return true;
            }
            return Winner("Answer was corrent!!!!");
        }

        private bool Winner(string answerWasCorrent)
        {
            Console.WriteLine(answerWasCorrent);
            _purses[_currentPlayerIndex]++;
            Console.WriteLine(CurrentPlayer + " now has " + _purses[_currentPlayerIndex] + " Gold Coins.");

            var winner = DidPlayerWin();
            _currentPlayerIndex = _currentPlayerIndex + 1;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;

            return winner;
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayerIndex] = true;

            _currentPlayerIndex = _currentPlayerIndex + 1;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
            return true;
        }

        private void AskQuestion()
        {
            var category = _categoryProvider.GetCategory(CurrentPlayerLocation);
            Console.WriteLine("The category is " + category);
            Console.WriteLine(_questionProvider.GetQuestion(category));
        }

        private bool DidPlayerWin()
        {
            return _purses[_currentPlayerIndex] != 6;
        }
    }
}
