using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private enum QuestionCategory
        {
            Pop,
            Science,
            Sports,
            Rock
        }

        int _currentPlayerIndex;
        readonly List<string> _players = new List<string>();

        readonly bool[] _inPenaltyBox = new bool[6];
        bool _isGettingOutOfPenaltyBox;

        readonly int[] _places = new int[6];
        readonly int[] _purses = new int[6];

        readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();

        public string CurrentPlayer
        {
            get
            {
                return _players[_currentPlayerIndex];
            }
        }

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast("Science Question " + i);
                _sportsQuestions.AddLast("Sports Question " + i);
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public void Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
        }

        private string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public bool IsPlayable()
        {
            return HowManyPlayers() >= 2;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(CurrentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayerIndex])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(CurrentPlayer + " is getting out of the penalty box");
                    DontKnowYet(roll);
                }
                else
                {
                    Console.WriteLine(CurrentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                DontKnowYet(roll);
            }
        }

        private void DontKnowYet(int roll)
        {
            _places[_currentPlayerIndex] = _places[_currentPlayerIndex] + roll;
            if (_places[_currentPlayerIndex] > 11) _places[_currentPlayerIndex] = _places[_currentPlayerIndex] - 12;

            Console.WriteLine(CurrentPlayer + "'s new location is " + _places[_currentPlayerIndex]);
            Console.WriteLine("The category is " + CurrentCategory());
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
            if (CurrentCategory() == QuestionCategory.Pop)
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Science)
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Sports)
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Rock)
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private QuestionCategory CurrentCategory()
        {
            if (_places[_currentPlayerIndex] == 0) return QuestionCategory.Pop;
            if (_places[_currentPlayerIndex] == 4) return QuestionCategory.Pop;
            if (_places[_currentPlayerIndex] == 8) return QuestionCategory.Pop;
            if (_places[_currentPlayerIndex] == 1) return QuestionCategory.Science;
            if (_places[_currentPlayerIndex] == 5) return QuestionCategory.Science;
            if (_places[_currentPlayerIndex] == 9) return QuestionCategory.Science;
            if (_places[_currentPlayerIndex] == 2) return QuestionCategory.Sports;
            if (_places[_currentPlayerIndex] == 6) return QuestionCategory.Sports;
            if (_places[_currentPlayerIndex] == 10) return QuestionCategory.Sports;
            return QuestionCategory.Rock;
        }

        private bool DidPlayerWin()
        {
            return _purses[_currentPlayerIndex] != 6;
        }
    }
}
