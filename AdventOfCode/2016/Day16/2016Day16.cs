using System.Text;

namespace AdventOfCode._2016.Day16;

public partial class _2016Day16 : _2016Day
{
    private readonly string Data;

    public _2016Day16() : base("Day16")
    {
        Data = Input[0];
    }

    private static string Expand(string str)
    {
        var sb = new StringBuilder(str);
        sb.Append('0');
        sb.Append(string.Join("", str.Select(c => c == '0' ? '1' : '0').Reverse()));
        return sb.ToString();
    }

    private static string Simplify(string str)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < str.Length; i += 2)
            sb.Append(str[i] == str[i + 1] ? '1' : '0');
        return sb.ToString();
    }

    private string CalculateCheckSum(int size)
    {
        string generated = Data;
        while (generated.Length < size)
            generated = Expand(generated);
        generated = generated[..size];

        while (generated.Length % 2 == 0)
            generated = Simplify(generated);
        return generated;
    }

    public override void Part1()
    {
        base.Part1();

        string checkSum = CalculateCheckSum(272);
        Console.WriteLine($"The checksum is: {checkSum}");
    }

    public override void Part2()
    {
        base.Part2();

        string checkSum = CalculateCheckSum(35_651_584);
        Console.WriteLine($"The checksum is: {checkSum}");
    }
}
