using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using trivia.enums;
using trivia.providers;
using trivia.services;

namespace trivia.tests.providers
{
    public class QuestionProviderTests
    {
        [TestCase(QuestionCategory.Pop)]
        [TestCase(QuestionCategory.Rock)]
        [TestCase(QuestionCategory.Sports)]
        [TestCase(QuestionCategory.Science)]
        public void GivenZeroQuestionsPerCategory_WhenQueryingCount_ReturnsZero(string category)
        {
            var generatorMock = new Mock<IQuestionService>();
            generatorMock.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>() },
                    { QuestionCategory.Rock, new Queue<string>() },
                    { QuestionCategory.Science, new Queue<string>() },
                    { QuestionCategory.Sports, new Queue<string>() }
                });
            var provider = new QuestionProvider(generatorMock.Object);

            var count = provider.GetQuestionCount(category);

            Assert.That(count, Is.Zero);
        }

        [TestCase(QuestionCategory.Pop)]
        [TestCase(QuestionCategory.Rock)]
        [TestCase(QuestionCategory.Sports)]
        [TestCase(QuestionCategory.Science)]
        public void GivenZeroQuestionsPerCategory_WheAskingQuestion_Throws(string category)
        {
            var generatorMock = new Mock<IQuestionService>();
            generatorMock.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>() },
                    { QuestionCategory.Rock, new Queue<string>() },
                    { QuestionCategory.Science, new Queue<string>() },
                    { QuestionCategory.Sports, new Queue<string>() }
                });
            var provider = new QuestionProvider(generatorMock.Object);

            Assert.Throws<InvalidOperationException>(() => provider.GetQuestion(category));
        }

        [TestCase(QuestionCategory.Pop)]
        [TestCase(QuestionCategory.Rock)]
        [TestCase(QuestionCategory.Sports)]
        [TestCase(QuestionCategory.Science)]
        public void GivenOneQuestionPerCategory_WhenAskingQuestion_QuestionContainerIsEmptied(string category)
        {
            var generator = new Mock<IQuestionService>();
            generator.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>(new[] { "QPop" }) },
                    { QuestionCategory.Rock, new Queue<string>(new[] { "QRock" }) },
                    { QuestionCategory.Science, new Queue<string>(new[] { "QScience" }) },
                    { QuestionCategory.Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            provider.GetQuestion(category);

            Assert.That(provider.GetQuestionCount(category), Is.Zero);
        }

        [TestCase(QuestionCategory.Pop)]
        [TestCase(QuestionCategory.Rock)]
        [TestCase(QuestionCategory.Sports)]
        [TestCase(QuestionCategory.Science)]
        public void GivenOneQuestionPerCategory_WhenAskingQuestion_CorrectQuestionIsReturned(string category)
        {
            var generator = new Mock<IQuestionService>();
            generator.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>(new[] { "QPop" }) },
                    { QuestionCategory.Rock, new Queue<string>(new[] { "QRock" }) },
                    { QuestionCategory.Science, new Queue<string>(new[] { "QScience" }) },
                    { QuestionCategory.Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            var question = provider.GetQuestion(category);

            Assert.That(question, Is.EqualTo("Q"+category));
        }

        [Test]
        public void GivenVariableQuestionsPerCategory_WhenQueryingCount_ReturnsCorrectValue()
        {
            var generator = new Mock<IQuestionService>();
            generator.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { QuestionCategory.Rock, new Queue<string>(new[] { "QRock1", "QRock2", "QRock3", "QRock4" }) },
                    { QuestionCategory.Science, new Queue<string>(new[] { "QScience1" }) },
                    { QuestionCategory.Sports, new Queue<string>(new[] { "QSports1", "QSports2" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            Assert.That(provider.GetQuestionCount(QuestionCategory.Pop), Is.EqualTo(3));
            Assert.That(provider.GetQuestionCount(QuestionCategory.Rock), Is.EqualTo(4));
            Assert.That(provider.GetQuestionCount(QuestionCategory.Science), Is.EqualTo(1));
            Assert.That(provider.GetQuestionCount(QuestionCategory.Sports), Is.EqualTo(2));
        }

        [Test]
        public void GivenMultipleQuestionsPerCategory_WhenAskingQuestion_FirstQuestionIsReturned()
        {
            var generator = new Mock<IQuestionService>();
            generator.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { QuestionCategory.Rock, new Queue<string>(new[] { "QRock" }) },
                    { QuestionCategory.Science, new Queue<string>(new[] { "QScience" }) },
                    { QuestionCategory.Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            var question = provider.GetQuestion(QuestionCategory.Pop);

            Assert.That(question, Is.EqualTo("QPop1"));
        }

        [Test]
        public void GivenMultipleQuestionsPerCategory_AfterAskingQuestion_QuestionContainerDecreasesInSizeWithOne()
        {
            var generator = new Mock<IQuestionService>();
            generator.Setup(g => g.Get()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { QuestionCategory.Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { QuestionCategory.Rock, new Queue<string>(new[] { "QRock" }) },
                    { QuestionCategory.Science, new Queue<string>(new[] { "QScience" }) },
                    { QuestionCategory.Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            provider.GetQuestion(QuestionCategory.Pop);

            Assert.That(provider.GetQuestionCount(QuestionCategory.Pop), Is.EqualTo(2));
        }
    }
}
