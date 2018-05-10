using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public interface IQuestionProvider
    {
        void AskQuestion(string questionCategory);
    }

    public class QuestionProvider : IQuestionProvider
    {
        readonly Queue<string> _popQuestions;
        readonly Queue<string> _rockQuestions;
        readonly Queue<string> _scienceQuestions;
        readonly Queue<string> _sportsQuestions;

        public QuestionProvider() : this(new QuestionGenerator(
                                                new[] { QuestionCategory.Pop, QuestionCategory.Rock, QuestionCategory.Science, QuestionCategory.Sports },
                                                50))
        {
        }

        public QuestionProvider(IGenerator<IDictionary<string, Queue<string>>> generator)
        {
            var questions = generator.Generate();
            questions.TryGetValue(QuestionCategory.Pop, out _popQuestions);
            questions.TryGetValue(QuestionCategory.Rock, out _rockQuestions);
            questions.TryGetValue(QuestionCategory.Science, out _scienceQuestions);
            questions.TryGetValue(QuestionCategory.Sports, out _sportsQuestions);
        }

        public void AskQuestion(string questionCategory)
        {
            if (questionCategory == QuestionCategory.Pop)
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Science)
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Sports)
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.Dequeue();
            }

            if (questionCategory == QuestionCategory.Rock)
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.Dequeue();
            }
        }
    }
}
