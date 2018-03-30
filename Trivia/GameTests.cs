using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.IO;
using System.Linq;

namespace Trivia
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Verify()
        {
            using (var istrm = new FileStream("Input.txt", FileMode.Open, FileAccess.Read))
            using (var gstrm = new FileStream("Output.txt", FileMode.Open, FileAccess.Read))
            using (var input = new StreamReader(istrm))
            using (var master = new StreamReader(gstrm))
            using(var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var values = input.ReadToEnd().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                var randomizer = new RandomStub(values);

                for (var i = 0; i < 5000; i++)
                {
                    GameRunner.Run(randomizer);
                    Console.WriteLine("Exiting " + randomizer.Count + Environment.NewLine);
                }

                Assert.That(writer.ToString(), Is.EqualTo(master.ReadToEnd()));
            }
        }
    }
}
