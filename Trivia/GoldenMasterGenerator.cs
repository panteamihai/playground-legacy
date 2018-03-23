using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

namespace Trivia
{
    class RandomStub : IRandom
    {
        private readonly List<int> _seeds = new List<int>();

        private int _count;
        public RandomStub()
        {
            Enumerable.Zip(Enumerable.Range(0, 5), Enumerable.Range(0, 9),
                (a, b) =>
                {
                    _seeds.Add(a);
                    _seeds.Add(b);
                    return 0;
                }).ToList();
        }

        public int Next(int maxValue)
        {
            Console.WriteLine("Interim " + _count);
            return _seeds[_count++ % _seeds.Count];
        }

        public int Count => _count;
    }


    [TestFixture]
    class GoldenMasterGenerator
    {
        [Test]
        public void Generate()
        {
            Console.WriteLine(Path.GetFullPath("Redirect.txt"));

            using (var ostrm = new FileStream("Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(ostrm))
            {

                Console.SetOut(writer);

                var randomizer = new RandomStub();

                for (int i = 0; i < 45; i++)
                {
                    GameRunner.Run(randomizer);
                    Console.WriteLine("Exiting " + randomizer.Count + Environment.NewLine);
                }
            }
        }
    }
}
