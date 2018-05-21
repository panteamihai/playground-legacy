using System.Collections.Generic;
using trivia.enums;

namespace trivia.providers
{
    public interface ICategoryProvider
    {
        IEnumerable<string> GetCategories();
        string GetCategory(int position);
    }

    public class CategoryProvider : ICategoryProvider
    {
        public IEnumerable<string> GetCategories()
        {
            return new[] { QuestionCategory.Pop, QuestionCategory.Rock, QuestionCategory.Science, QuestionCategory.Sports };
        }

        public string GetCategory(int position)
        {
            switch (position % 4)
            {
                case 0:
                    return QuestionCategory.Pop;
                case 1:
                    return QuestionCategory.Science;
                case 2:
                    return QuestionCategory.Sports;
                default:
                    return QuestionCategory.Rock;
            }
        }
    }
}
