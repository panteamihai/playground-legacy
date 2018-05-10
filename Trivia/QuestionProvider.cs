using System;
using System.Collections.Generic;
using System.Linq;
using static Trivia.QuestionCategory;

namespace Trivia
{
    public interface IQuestionProvider
    {
        int GetQuestionCount(string questionCategory);
        void AskQuestion(string questionCategory);
    }

    public class QuestionProvider : IQuestionProvider
    {
        readonly Queue<string> _popQuestions;
        readonly Queue<string> _rockQuestions;
        readonly Queue<string> _scienceQuestions;
        readonly Queue<string> _sportsQuestions;

        public QuestionProvider() : this(new QuestionGenerator(new[] { Pop, Rock, Science, Sports }, 50)) { }

        public QuestionProvider(IGenerator<IDictionary<string, Queue<string>>> generator)
        {
            var questions = generator.Generate();
            questions.TryGetValue(Pop, out _popQuestions);
            questions.TryGetValue(Rock, out _rockQuestions);
            questions.TryGetValue(Science, out _scienceQuestions);
            questions.TryGetValue(Sports, out _sportsQuestions);
        }

        public int GetQuestionCount(string questionCategory)
        {
            IEnumerable<string> questions;
            switch (questionCategory)
            {
                case Pop: questions = _popQuestions;
                    break;
                case Rock:
                    questions = _rockQuestions;
                    break;
                case Science:
                    questions = _scienceQuestions;
                    break;
                default:
                    questions = _sportsQuestions;
                    break;
            }

            return questions.Count();
        }

        public void AskQuestion(string questionCategory)
        {
            if (questionCategory == Pop)
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.Dequeue();
            }

            if (questionCategory == Science)
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.Dequeue();
            }

            if (questionCategory == Sports)
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.Dequeue();
            }

            if (questionCategory == Rock)
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.Dequeue();
            }
        }
    }
}
