using System.Collections.Generic;
using System.Linq;
using static Trivia.QuestionCategory;

namespace Trivia
{
    public interface IQuestionProvider
    {
        int GetQuestionCount(string questionCategory);
        string GetQuestion(string questionCategory);
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
            return GetQuestions(questionCategory).Count();
        }

        public string GetQuestion(string questionCategory)
        {
            return GetQuestions(questionCategory).Dequeue();
        }

        private Queue<string> GetQuestions(string questionCategory)
        {
            Queue<string> questions;
            switch (questionCategory)
            {
                case Pop:
                    questions = _popQuestions;
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

            return questions;
        }
    }
}
