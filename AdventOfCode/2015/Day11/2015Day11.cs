using System.Text.RegularExpressions;
using AdventOfCode._2015.Day11;

namespace AdventOfCode
{
    public class _2015Day11 : _2015Day
    {
        private string pass;
        private readonly Regex doublePair = new Regex(@"([a-z])\1.*([a-z])\2");

        public _2015Day11() : base("Day11")
        {
            pass = Input[0].ToValidBase();
        }

        public override void Part1()
        {
            base.Part1();

            while (!pass.HasSequence() || !doublePair.IsMatch(pass))
            {
                pass = pass.Increase();
            }

            Console.WriteLine($"Next password: {pass}\n");
        }

        public override void Part2()
        {
            base.Part2();

            // I hate the do-while...
            pass = pass.Increase();
            while (!pass.HasSequence() || !doublePair.IsMatch(pass))
            {
                pass = pass.Increase();
            }

            Console.WriteLine($"Next password: {pass}\n");
        }
    }
}
