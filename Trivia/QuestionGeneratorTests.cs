using NUnit.Framework;
using System;
using System.Linq;

namespace Trivia
{
    class QuestionGeneratorTests
    {
        [Test]
        public void GivenNullAsCategories_WhenConstructingQuestionGenerator_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new QuestionGenerator(null, 5));
        }

        [Test]
        public void GivenNegativeNumberOfQuestions_WhenConstructingQuestionGenerator_ThenThrows()
        {
            Assert.Throws<ArgumentException>(() => new QuestionGenerator(new[] { "Poop" }, -4));
        }

        [Test]
        public void GivenEmptyListOfCategories_WhenGeneratingQuestions_ReturnsEmpty()
        {
            var questionGenerator = new QuestionGenerator(Enumerable.Empty<string>(), 5);

            var questions = questionGenerator.Generate();

            Assert.That(questions, Is.Empty);
        }

        [Test]
        public void GivenZeroNumberOfQUestions_WHenGenerating_ThenReturnDictionaryWithEmptyListsAsValues()
        {
            var questionGenerator = new QuestionGenerator(new[] { "Poop" }, 0);

            var questions = questionGenerator.Generate();

            Assert.That(questions["Poop"], Is.Empty);
        }

        [Test]
        public void GivenOneCategoryWithOneQuestion_WhenGenerating_ThenResultContainsOneActualQuestion()
        {
            var questionGenerator = new QuestionGenerator(new[] { "Poop" }, 1);

            var questions = questionGenerator.Generate();

            Assert.That(questions["Poop"].Single(), Is.EqualTo("Poop Question 0"));
        }
    }
}
