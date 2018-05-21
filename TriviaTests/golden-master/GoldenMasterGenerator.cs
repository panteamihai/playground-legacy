using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace trivia.tests
{
    public class RandomStub : IRandom
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

        public RandomStub(IEnumerable<int> values)
        {
            _seeds.AddRange(values);
        }

        public int Next(int maxValue)
        {
            Console.WriteLine("Interim " + _count);
            return _seeds[_count++ % _seeds.Count];
        }

        public int Count => _count;
    }

    class GoldenMasterGenerator
    {
        //[Test]
        public void GenerateInput()
        {
            Console.WriteLine(Path.GetFullPath("Input.txt"));

            using (var ostrm = new FileStream("Input.txt", FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(ostrm))
            {
                var rand = new Random();
                for (var i = 0; i < 10000; i++)
                {
                    writer.Write(rand.Next(0, 5) + " " + rand.Next(0, 9) + " ");
                }
            }
        }

        //[Test]
        public void GenerateGoldenMaster()
        {
            //"C:\\Users\\mpantea\\AppData\\Local\\JetBrains\\Installations\\ReSharperPlatformVs15_23e94da4\\Output.txt"
            Console.WriteLine(Path.GetFullPath("GoldenMaster.txt"));

            using (var istrm = new FileStream("Input.txt", FileMode.Open, FileAccess.Read))
            using (var ostrm = new FileStream("GoldenMaster.txt", FileMode.OpenOrCreate, FileAccess.Write))
            using (var reader = new StreamReader(istrm))
            using (var writer = new StreamWriter(ostrm))
            {
                Console.SetOut(writer);
                var values = reader.ReadToEnd().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                var randomizer = new RandomStub(values);

                for (var i = 0; i < 5000; i++)
                {
                    GameRunner.Run(randomizer);
                    Console.WriteLine("Exiting " + randomizer.Count + Environment.NewLine);
                }
            }
        }
    }
}
