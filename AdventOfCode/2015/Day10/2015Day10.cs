using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day10 : _2015Day
    {
        private string number;

        public _2015Day10() : base("Day10")
        {
            number = Input[0];
        }

        public override void Part1()
        {
            base.Part1();

            for (int i = 0; i< 40; i++)
            {
                number = IterateNumber(number);
            }

            Console.WriteLine($"Length of the result: {number.Length}\n");
        }

        public override void Part2()
        {
            base.Part2();

            for (int i = 0; i < 10; i++) // 40 + 10 = 50
            {
                number = IterateNumber(number);
            }

            Console.WriteLine($"Length of the result: {number.Length}\n");
        }

        private static string IterateNumber(string number) => string.Join("", Regex.Matches(number, @"(\d)\1*").Select(m => $"{m.Value.Length}{m.Value[0]}"));
    }
}
