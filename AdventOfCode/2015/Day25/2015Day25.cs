using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode;
public partial class _2015Day25 : _2015Day
{
    private readonly int Row;
    private readonly int Column;

    public _2015Day25() : base("Day25")
    {
        var a = RowColRegex().Match(Input[0]);
        Row = int.Parse(a.Groups[1].Value);
        Column = int.Parse(a.Groups[2].Value);
    }

    [GeneratedRegex(@"row (\d+), column (\d+)")]
    private static partial Regex RowColRegex();

    private static int Table(int row, int column) => (row, column) switch
    {
        (int n, int m) when n <= 0 && m <= 0 => 0,
        (1, 1) => 1,
        (int n, 1) => Table(n - 1, 1) + n - 1,
        (int n, int m) => Table(n, m - 1) + n + m - 1,
    };

    private static long Pseudo(long seed, long mult, long mod, int n)
    {
        var a_n = BigInteger.ModPow(mult, n - 1, mod);
        return (long)(seed * a_n % mod);

        // Stack overflow
        // ... but it worked in F#, thanks to tail-recursion
        //return n switch
        //{
        //    int x when x <= 0 => 0,
        //    1 => seed,
        //    int x => Pseudo(seed * mult % mod, mult, mod, x - 1),
        //};
    }

    public override void Part1()
    {
        base.Part1();
        int index = Table(Row, Column);
        Console.WriteLine(Row);
        Console.WriteLine(Column);
        Console.WriteLine(index);
        long code = Pseudo(20151125, 252533, 33554393, index);
        Console.WriteLine($"The copy protection code is: {code}");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine("I'll take a bonus star any time *");
    }
}
