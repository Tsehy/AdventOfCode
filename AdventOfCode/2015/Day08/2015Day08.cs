using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day08 : _2015Day
    {
        private readonly int totalCharacters;

        public _2015Day08() : base("Day08")
        {
            totalCharacters = Input.Sum(s => s.Length);
        }

        public override void Part1()
        {
            base.Part1();

            var oneChar = new Regex(@"\\\\|\\\x22");
            var threeChar = new Regex(@"\\x[0-9a-f]{2}");
            int memoryCharacters = Input.Sum(s => s.Length - oneChar.Matches(s).Count - 3 * threeChar.Matches(s).Count - 2);

            Console.WriteLine($"Diff: {totalCharacters - memoryCharacters}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int encodedCharacters = Input.Sum(s => s.Length + s.Count(c => "\\\x22".Contains(c)) + 2);

            Console.WriteLine($"Diff: {encodedCharacters - totalCharacters}\n");
        }
    }
}
