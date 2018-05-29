using System;
using trivia.models;
using trivia.providers;
using trivia.services;

namespace trivia
{
    public class Game
    {
        private readonly IQuestionProvider _questionProvider;
        private readonly ICategoryProvider _categoryProvider;
        private readonly IPlayerService _playerService;

        public Player CurrentPlayer => _playerService.Current;

        public int PlayerCount => _playerService.Count;

        public bool IsPlayable => PlayerCount >= 2;

        public Game()
        {
            _categoryProvider = new CategoryProvider();
            _questionProvider = new QuestionProvider(_categoryProvider);
            _playerService = new PlayerService();
        }

        public Game(ICategoryProvider categoryProvider, IQuestionProvider questionProvider, IPlayerService playerService)
        {
            _categoryProvider = categoryProvider;
            _questionProvider = questionProvider;
            _playerService = playerService;
        }

        public void Add(string playerName)
        {
            _playerService.Add(playerName);
        }

        public void Roll(int roll)
        {
            if (roll < 0)
                throw new ArgumentException("Invalid roll!");

            if (!IsPlayable)
                throw new InvalidOperationException("Cannot roll if game not playable.");

            Console.WriteLine(CurrentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_playerService.HasCurrentPlayerIncurredPenalty())
            {
                if(_playerService.CanTemporarilyOvercomePenalty(roll))
                    Move(roll);
            }
            else
            {
                Move(roll);
            }
        }

        private void Move(int roll)
        {
            _playerService.Move(roll);
            AskQuestion();
        }

        public bool ShouldContinueAfterRightAnswer()
        {
            if (!_playerService.HasCurrentPlayerIncurredPenalty())
                return ShouldContinueAfterRightAnswer("Answer was corrent!!!!");

            if (_playerService.HasCurrentPlayerTemporarilyOvercomePenalty())
                return ShouldContinueAfterRightAnswer("Answer was correct!!!!");

            _playerService.GiveTurnToNextPlayer();
            return true;
        }

        private bool ShouldContinueAfterRightAnswer(string correctAnswerMessage)
        {
            DoRightAnswerAction(correctAnswerMessage);

            var doesGameContinue =  DoesGameContinue();
            _playerService.GiveTurnToNextPlayer();

            return doesGameContinue;
        }

        private void DoRightAnswerAction(string correctAnswerMessage)
        {
            Console.WriteLine(correctAnswerMessage);
            _playerService.CollectOneCoin();
        }

        private bool DoesGameContinue()
        {
            return !_playerService.HasCurrentPlayerWon();
        }

        public bool ShouldContinueAfterWrongAnswer()
        {
            DoWrongAnswerAction();

            _playerService.GiveTurnToNextPlayer();

            return true;
        }

        private void DoWrongAnswerAction()
        {
            Console.WriteLine("Question was incorrectly answered");
            _playerService.PenalizeCurrentPlayer();
        }

        private void AskQuestion()
        {
            var category = _categoryProvider.GetCategory(CurrentPlayer.Location);
            Console.WriteLine("The category is " + category);
            Console.WriteLine(_questionProvider.GetQuestion(category));
        }
    }
}
