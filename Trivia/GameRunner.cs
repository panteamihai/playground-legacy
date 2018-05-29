using System;

namespace trivia
{
    public interface IRandom
    {
        int Next(int maxValue);
    }

    public class MyRandom : IRandom
    {
        private readonly Random _rand;

        public MyRandom()
        {
            _rand = new Random();
        }

        public int Next(int maxValue)
        {
            return _rand.Next(maxValue);
        }
    }


    public class GameRunner
    {
        private static bool continueGame;

        public static void Main(string[] args)
        {
            Run(new MyRandom());
        }

        public static void Run(IRandom random)
        {
            Game aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            do
            {
                aGame.Roll(random.Next(5) + 1);

                continueGame = random.Next(9) == 7 ? aGame.ShouldContinueAfterWrongAnswer() : aGame.ShouldContinueAfterRightAnswer();
            }
            while (continueGame);
        }
    }
}

