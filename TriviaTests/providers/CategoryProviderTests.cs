using NUnit.Framework;
using System.Collections.Generic;
using trivia.enums;
using trivia.providers;

namespace trivia.tests.providers
{
    public class CategoryProviderTests
    {

        private static IEnumerable<TestCaseData> CurrentCategoryByCurrentPlayerLocationTestCases
        {
            get
            {
                yield return new TestCaseData(0).Returns(QuestionCategory.Pop);
                yield return new TestCaseData(4).Returns(QuestionCategory.Pop);
                yield return new TestCaseData(8).Returns(QuestionCategory.Pop);
                yield return new TestCaseData(1).Returns(QuestionCategory.Science);
                yield return new TestCaseData(5).Returns(QuestionCategory.Science);
                yield return new TestCaseData(9).Returns(QuestionCategory.Science);
                yield return new TestCaseData(2).Returns(QuestionCategory.Sports);
                yield return new TestCaseData(6).Returns(QuestionCategory.Sports);
                yield return new TestCaseData(10).Returns(QuestionCategory.Sports);
                yield return new TestCaseData(3).Returns(QuestionCategory.Rock);
                yield return new TestCaseData(7).Returns(QuestionCategory.Rock);
                yield return new TestCaseData(11).Returns(QuestionCategory.Rock);
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
