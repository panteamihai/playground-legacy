using System.Collections.Generic;
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
        readonly IDictionary<string, Queue<string>> _questions;

        public QuestionProvider() : this(new QuestionGenerator(new[] { Pop, Rock, Science, Sports }, 50)) { }

        public QuestionProvider(IGenerator<IDictionary<string, Queue<string>>> generator)
        {
            _questions = generator.Generate();
        }

        public int GetQuestionCount(string questionCategory)
        {
            return _questions[questionCategory].Count;
        }

        public string GetQuestion(string questionCategory)
        {
            return _questions[questionCategory].Dequeue();
        }
    }
}
