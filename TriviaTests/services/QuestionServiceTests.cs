using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using trivia.providers;
using trivia.services;

namespace trivia.tests.services
{
    public class QuestionServiceTests
    {
        [Test]
        public void GivenNullAsCategories_WhenConstructingQuestionGenerator_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new QuestionService(null, 5));
        }

        [Test]
        public void GivenNegativeNumberOfQuestions_WhenConstructingQuestionGenerator_ThenThrows()
        {
            var categoryProvider = GetCategoryProvider(new[] { "Poop" });
            Assert.Throws<ArgumentException>(() => new QuestionService(categoryProvider, -4));
        }

        [Test]
        public void GivenEmptyListOfCategories_WhenGeneratingQuestions_ReturnsEmpty()
        {
            var categoryProvider = GetCategoryProvider(Enumerable.Empty<string>());
            var questionGenerator = new QuestionService(categoryProvider, 5);

            var questions = questionGenerator.Get();

            Assert.That(questions, Is.Empty);
        }

        [Test]
        public void GivenZeroNumberOfQUestions_WHenGenerating_ThenReturnDictionaryWithEmptyListsAsValues()
        {
            var categoryProvider = GetCategoryProvider(new[] { "Poop" });
            var questionGenerator = new QuestionService(categoryProvider, 0);

            var questions = questionGenerator.Get();

            Assert.That(questions["Poop"], Is.Empty);
        }

        [Test]
        public void GivenOneCategoryWithOneQuestion_WhenGenerating_ThenResultContainsOneActualQuestion()
        {
            var categoryProvider = GetCategoryProvider(new[] { "Poop" });
            var questionGenerator = new QuestionService(categoryProvider, 1);

            var questions = questionGenerator.Get();

            Assert.That(questions["Poop"].Single(), Is.EqualTo("Poop Question 0"));
        }

        private static ICategoryProvider GetCategoryProvider(IEnumerable<string> categories)
        {
            var categoryProviderMock = new Mock<ICategoryProvider>();
            categoryProviderMock.Setup(cp => cp.GetCategories()).Returns(categories);

            return categoryProviderMock.Object;
        }
    }
}
