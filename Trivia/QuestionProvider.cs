using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public interface IQuestionProvider
    {
        void AskQuestion(string questionCategory);
    }

    public class QuestionProvider : IQuestionProvider
    {
        readonly Queue<string> _popQuestions = new Queue<string>();
        readonly Queue<string> _rockQuestions = new Queue<string>();
        readonly Queue<string> _scienceQuestions = new Queue<string>();
        readonly Queue<string> _sportsQuestions = new Queue<string>();

        public QuestionProvider()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.Enqueue("Pop Question " + i);
                _scienceQuestions.Enqueue("Science Question " + i);
                _sportsQuestions.Enqueue("Sports Question " + i);
                _rockQuestions.Enqueue("Rock Question " + i);
            }
        }

        public void AskQuestion(string questionCategory)
        {
            if (questionCategory == QuestionCategory.Pop)
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Science)
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Sports)
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Rock)
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.Dequeue();
            }
        }
    }
}
