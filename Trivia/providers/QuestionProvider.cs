using System.Collections.Generic;
using trivia.services;

namespace trivia.providers
{
    public interface IQuestionProvider
    {
        int GetQuestionCount(string questionCategory);
        string GetQuestion(string questionCategory);
    }

    public class QuestionProvider : IQuestionProvider
    {
        readonly IDictionary<string, Queue<string>> _questions;

        public QuestionProvider() : this(new CategoryProvider()) { }

        public QuestionProvider(ICategoryProvider categoryProvider)
        {
            var generator = new QuestionService(categoryProvider, 50);
            _questions = generator.Get();
        }

        public QuestionProvider(IQuestionService questionService)
        {
            _questions = questionService.Get();
        }

        public int GetQuestionCount(string questionCategory)
        {
            return _questions[questionCategory].Count;
        }

        public string GetQuestion(string questionCategory)
        {
            return _questions[questionCategory].Dequeue();
        }
    }
}
