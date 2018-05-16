using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Trivia
{
    public class GoldenMasterTest
    {

        [Test]
        public void GivenGame_WhenRunWithGoldenMasterInput_ThenOutputIsTheSameAsGoldenMasterOutput()
        {
            using (var istrm = new FileStream("Input.txt", FileMode.Open, FileAccess.Read))
            using (var gstrm = new FileStream("GoldenMaster.txt", FileMode.Open, FileAccess.Read))
            using (var input = new StreamReader(istrm))
            using (var goldenMaster = new StreamReader(gstrm))
            using (var output = new StringWriter())
            {
                Console.SetOut(output);
                var values = input.ReadToEnd().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                var randomizer = new RandomStub(values);

                for (var i = 0; i < 5000; i++)
                {
                    GameRunner.Run(randomizer);
                    Console.WriteLine("Exiting " + randomizer.Count + Environment.NewLine);
                }

                var runOutput = output.ToString();
                File.WriteAllText("Output.txt", runOutput);
                Assert.That(runOutput, Is.EqualTo(goldenMaster.ReadToEnd()));
            }
        }
    }
}
