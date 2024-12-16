using System.Text.RegularExpressions;

namespace AdventOfCode.Day06
{
    readonly record struct Instruction(string Mode, int X1, int Y1, int X2, int Y2)
    {
        public static implicit operator Instruction(string stringInstruction)
        {
            var r = new Regex(@"(toggle|turn on|turn off) (\d{0,3}),(\d{0,3}) through (\d{0,3}),(\d{0,3})");
            Match m = r.Match(stringInstruction);
            return new Instruction(
                m.Groups[1].Value,
                int.Parse(m.Groups[2].Value),
                int.Parse(m.Groups[3].Value),
                int.Parse(m.Groups[4].Value),
                int.Parse(m.Groups[5].Value)
            );
        }
    }

}
