using System;
using System.Collections.Generic;

namespace Trivia
{
    public class Game
    {
        private readonly IQuestionProvider _questionProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly PlayerService _playerService;

        private int _currentPlayerIndex => _playerService.Current.Ordinal;

        private readonly bool[] _inPenaltyBox = new bool[6];
        private bool _isGettingOutOfPenaltyBox;

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        public int CurrentPlayerLocation
        {
            get => _playerService.Current.Location.Value;
            private set => _playerService.Current.Move(value);
        }

        public string CurrentPlayer => _playerService.Current.Name;

        public int PlayerCount => _playerService.Count;

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
            _playerService.AddPlayer(playerName);
            _places[PlayerCount] = 0;
            _purses[PlayerCount] = 0;
            _inPenaltyBox[PlayerCount] = false;
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
            _playerService.MoveCurrentPlayer(roll);
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

                _playerService.ChangePlayer();
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
            _playerService.ChangePlayer();

            return winner;
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayerIndex] = true;

            _playerService.ChangePlayer();
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
