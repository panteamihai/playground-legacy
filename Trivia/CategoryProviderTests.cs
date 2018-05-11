using NUnit.Framework;
using System.Collections.Generic;
using static Trivia.QuestionCategory;

namespace Trivia
{
    public class CategoryProviderTests
    {

        private static IEnumerable<TestCaseData> CurrentCategoryByCurrentPlayerLocationTestCases
        {
            get
            {
                yield return new TestCaseData(0).Returns(Pop);
                yield return new TestCaseData(4).Returns(Pop);
                yield return new TestCaseData(8).Returns(Pop);
                yield return new TestCaseData(1).Returns(Science);
                yield return new TestCaseData(5).Returns(Science);
                yield return new TestCaseData(9).Returns(Science);
                yield return new TestCaseData(2).Returns(Sports);
                yield return new TestCaseData(6).Returns(Sports);
                yield return new TestCaseData(10).Returns(Sports);
                yield return new TestCaseData(3).Returns(Rock);
                yield return new TestCaseData(7).Returns(Rock);
                yield return new TestCaseData(11).Returns(Rock);
            }
        }

        [TestCaseSource(nameof(CurrentCategoryByCurrentPlayerLocationTestCases))]
        public string GivenCategoryProvider_WhenRetrievingCategory_ReturnsExpectedCategory(int position)
        {
            var categoryProvider = new CategoryProvider();

            return categoryProvider.GetCategory(position);
        }
    }
}
