using System.Text;

namespace AdventOfCode._2018.Day05;

public class _2018Day05 : _2018Day
{
    public _2018Day05() : base("Day05")
    {
    }

    private static int Collapse(string str, params char[] skip)
    {
        var sb = new StringBuilder();

        foreach (char c in str)
        {
            if (skip.Contains(c))
                continue;

            if (sb.Length == 0 || Math.Abs(c - sb[^1]) != 32)
                sb.Append(c);
            else
                sb.Length--;
        }

        return sb.Length;
    }

    public override void Part1()
    {
        base.Part1();

        int length = Collapse(Input[0]);
        Console.WriteLine($"The polymer's length is: {length}");
    }

    public override void Part2()
    {
        base.Part2();

        int minLength = int.MaxValue;
        for (int i = 0; i <= 26; i++)
        {
            char lowerCase = (char)('a' + i);
            char upperCase = (char)('A' + i);
            int length = Collapse(Input[0], lowerCase, upperCase);
            if (length < minLength)
                minLength = length;
        }

        Console.WriteLine($"The shortest possible polymer's length is: {minLength}");
    }
}
