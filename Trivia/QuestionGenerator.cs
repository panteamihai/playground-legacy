using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public interface IGenerator<out TResult>
    {
        TResult Generate();
    }

    public class QuestionGenerator : IGenerator<IDictionary<string, Queue<string>>>
    {
        private readonly IEnumerable<string> _categories;
        private readonly int _questionsPerCategory;

        public QuestionGenerator(ICategoryProvider categoryProvider, int questionsPerCategory)
        {
            _categories = categoryProvider?.GetCategories() ?? throw new ArgumentNullException();
            _questionsPerCategory = questionsPerCategory >= 0 ? questionsPerCategory : throw new ArgumentException();
        }

        public IDictionary<string, Queue<string>> Generate()
        {
            return _categories.ToDictionary(
                    c => c,
                    c => new Queue<string>(Enumerable.Range(0, _questionsPerCategory).Select(i => c + " Question " + i)));
        }
    }
}
