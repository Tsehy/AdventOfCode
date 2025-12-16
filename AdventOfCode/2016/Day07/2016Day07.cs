using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class _2016Day07 : _2016Day
{
    public _2016Day07() : base("Day07")
    {
    }

    [GeneratedRegex(@"(\w)(?:(?!\1)(\w))\2\1", RegexOptions.Compiled)]
    private static partial Regex Abba();

    [GeneratedRegex(@"\w+", RegexOptions.Compiled)]
    private static partial Regex Word();

    public override void Part1()
    {
        base.Part1();

        int support = Input
            .Select(i => Abba().Replace(i, "@"))
            .Select(i => Word().Replace(i, ""))
            .Count(i => i.Contains('@') && !i.Contains("[@"));

        Console.WriteLine($"{support} IPv7 supports TLS.\n");
    }

    [GeneratedRegex(@"(?=((?<=\[\w*)(\w)(?:(?!\2)\w)\2))", RegexOptions.Compiled)]
    private static partial Regex AbaInside();

    [GeneratedRegex(@"(?<=\[)[^\]]+", RegexOptions.Compiled)]
    private static partial Regex IgnoreBracket();

    public override void Part2()
    {
        base.Part2();

        int support = (from str in Input
                       let aba = AbaInside().Matches(str)
                       where aba.Count > 0
                       let bab = aba.Select(a => a.Groups[1].Value).Select(v => $"{v[1]}{v[0]}{v[1]}")
                       let noBracket = IgnoreBracket().Replace(str, "")
                       where bab.Any(noBracket.Contains)
                       select str).Count();

        Console.WriteLine($"{support} IPv7 supports SSL.\n");
    }
}
