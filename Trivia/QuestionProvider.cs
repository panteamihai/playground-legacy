using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class QuestionProvider
    {
        readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();

        public QuestionProvider()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast("Science Question " + i);
                _sportsQuestions.AddLast("Sports Question " + i);
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        private string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public void AskQuestionByCategory(Game.QuestionCategory questionCategory)
        {
            if (questionCategory == Game.QuestionCategory.Pop)
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }

            if (questionCategory == Game.QuestionCategory.Science)
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }

            if (questionCategory == Game.QuestionCategory.Sports)
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }

            if (questionCategory == Game.QuestionCategory.Rock)
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }
    }
}
