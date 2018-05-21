using System;
using System.Collections.Generic;
using System.Linq;
using trivia.providers;

namespace trivia.services
{
    public interface IQuestionService
    {
        IDictionary<string, Queue<string>> Get();
    }

    public class QuestionService : IQuestionService
    {
        private readonly IEnumerable<string> _categories;
        private readonly int _questionsPerCategory;

        public QuestionService(ICategoryProvider categoryProvider, int questionsPerCategory)
        {
            _categories = categoryProvider?.GetCategories() ?? throw new ArgumentNullException();
            _questionsPerCategory = questionsPerCategory >= 0 ? questionsPerCategory : throw new ArgumentException();
        }

        public IDictionary<string, Queue<string>> Get()
        {
            return _categories.ToDictionary(
                    c => c,
                    c => new Queue<string>(Enumerable.Range(0, _questionsPerCategory).Select(i => c + " Question " + i)));
        }
    }
}
