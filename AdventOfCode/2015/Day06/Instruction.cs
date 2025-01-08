using System.Text.RegularExpressions;

namespace AdventOfCode._2015.Day06
{
    readonly record struct Instruction(string Mode, int X1, int Y1, int X2, int Y2)
    {
        private static readonly Regex instructionPattern = new(@"(toggle|turn on|turn off) (\d{0,3}),(\d{0,3}) through (\d{0,3}),(\d{0,3})");

        public static implicit operator Instruction(string stringInstruction)
        {
            Match match = instructionPattern.Match(stringInstruction);
            return new Instruction(
                match.Groups[1].Value,
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value),
                int.Parse(match.Groups[4].Value),
                int.Parse(match.Groups[5].Value)
            );
        }
    }
}
