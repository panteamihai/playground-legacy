using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Trivia.QuestionCategory;

namespace Trivia
{
    public class QuestionProviderTests
    {
        [TestCase(Pop)]
        [TestCase(Rock)]
        [TestCase(Sports)]
        [TestCase(Science)]
        public void GivenZeroQuestionsPerCategory_WhenQueryingCount_ReturnsZero(string category)
        {
            var generator = new QuestionGenerator(new[] { Pop, Rock, Science, Sports }, 0);
            var provider = new QuestionProvider(generator);

            var count = provider.GetQuestionCount(category);

            Assert.That(count, Is.Zero);
        }

        [TestCase(Pop)]
        [TestCase(Rock)]
        [TestCase(Sports)]
        [TestCase(Science)]
        public void GivenZeroQuestionsPerCategory_WheAskingQuestion_Throws(string category)
        {
            var generator = new QuestionGenerator(new[] { Pop, Rock, Science, Sports }, 0);
            var provider = new QuestionProvider(generator);

            Assert.Throws<InvalidOperationException>(() => provider.GetQuestion(category));
        }

        [TestCase(Pop)]
        [TestCase(Rock)]
        [TestCase(Sports)]
        [TestCase(Science)]
        public void GivenOneQuestionPerCategory_WhenAskingQuestion_QuestionContainerIsEmptied(string category)
        {
            var generator = new QuestionGenerator(new[] { Pop, Rock, Science, Sports }, 1);
            var provider = new QuestionProvider(generator);

            provider.GetQuestion(category);

            Assert.That(provider.GetQuestionCount(category), Is.Zero);
        }

        [TestCase(Pop)]
        [TestCase(Rock)]
        [TestCase(Sports)]
        [TestCase(Science)]
        public void GivenOneQuestionPerCategory_WhenAskingQuestion_CorrectQuestionIsReturned(string category)
        {
            var generator = new Mock<IGenerator<IDictionary<string, Queue<string>>>>();
            generator.Setup(g => g.Generate()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { Pop, new Queue<string>(new[] { "QPop" }) },
                    { Rock, new Queue<string>(new[] { "QRock" }) },
                    { Science, new Queue<string>(new[] { "QScience" }) },
                    { Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            var question = provider.GetQuestion(category);

            Assert.That(question, Is.EqualTo("Q"+category));
        }

        [Test]
        public void GivenVariableQuestionsPerCategory_WhenQueryingCount_ReturnsCorrectValue()
        {
            var generator = new Mock<IGenerator<IDictionary<string, Queue<string>>>>();
            generator.Setup(g => g.Generate()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { Rock, new Queue<string>(new[] { "QRock1", "QRock2", "QRock3", "QRock4" }) },
                    { Science, new Queue<string>(new[] { "QScience1" }) },
                    { Sports, new Queue<string>(new[] { "QSports1", "QSports2" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            Assert.That(provider.GetQuestionCount(Pop), Is.EqualTo(3));
            Assert.That(provider.GetQuestionCount(Rock), Is.EqualTo(4));
            Assert.That(provider.GetQuestionCount(Science), Is.EqualTo(1));
            Assert.That(provider.GetQuestionCount(Sports), Is.EqualTo(2));
        }

        [Test]
        public void GivenMultipleQuestionsPerCategory_WhenAskingQuestion_FirstQuestionIsReturned()
        {
            var generator = new Mock<IGenerator<IDictionary<string, Queue<string>>>>();
            generator.Setup(g => g.Generate()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { Rock, new Queue<string>(new[] { "QRock" }) },
                    { Science, new Queue<string>(new[] { "QScience" }) },
                    { Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            var question = provider.GetQuestion(Pop);

            Assert.That(question, Is.EqualTo("QPop1"));
        }

        [Test]
        public void GivenMultipleQuestionsPerCategory_AfterAskingQuestion_QuestionContainerDecreasesInSizeWithOne()
        {
            var generator = new Mock<IGenerator<IDictionary<string, Queue<string>>>>();
            generator.Setup(g => g.Generate()).Returns(
                new Dictionary<string, Queue<string>>
                {
                    { Pop, new Queue<string>(new[] { "QPop1", "QPop2", "QPop3" }) },
                    { Rock, new Queue<string>(new[] { "QRock" }) },
                    { Science, new Queue<string>(new[] { "QScience" }) },
                    { Sports, new Queue<string>(new[] { "QSports" }) }
                });
            var provider = new QuestionProvider(generator.Object);

            provider.GetQuestion(Pop);

            Assert.That(provider.GetQuestionCount(Pop), Is.EqualTo(2));
        }
    }
}
