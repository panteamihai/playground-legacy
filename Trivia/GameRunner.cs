using System;

namespace Trivia
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
        private static bool notAWinner;

        public static void Main(string[] args)
        {
            Run(new MyRandom());
        }

        internal static void Run(IRandom random)
        {
            Game aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            do
            {
                aGame.Roll(random.Next(5) + 1);

                if (random.Next(9) == 7)
                {
                    notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    notAWinner = aGame.WasCorrectlyAnswered();
                }
            }
            while (notAWinner);
        }
    }
}

