using System.Collections.Generic;
using static Trivia.QuestionCategory;

namespace Trivia
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
            return new[] { Pop, Rock, Science, Sports };
        }

        public string GetCategory(int position)
        {
            switch (position % 4)
            {
                case 0:
                    return Pop;
                case 1:
                    return Science;
                case 2:
                    return Sports;
                default:
                    return Rock;
            }
        }
    }
}
