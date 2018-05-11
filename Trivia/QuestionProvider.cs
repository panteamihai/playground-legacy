using System.Collections.Generic;

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

        public QuestionProvider() : this(new CategoryProvider()) { }

        public QuestionProvider(ICategoryProvider categoryProvider)
        {
            var generator = new QuestionGenerator(categoryProvider, 50);
            _questions = generator.Generate();
        }

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
