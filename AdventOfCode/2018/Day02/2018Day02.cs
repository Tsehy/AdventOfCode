using System.Text;

namespace AdventOfCode._2018.Day02;

public class _2018Day02 : _2018Day
{
    public _2018Day02() : base("Day02")
    {
    }

    public override void Part1()
    {
        base.Part1();

        int two = 0, three = 0;
        foreach (string id in Input)
        {
            int[] groupLengths = [.. id.GroupBy(c => c).Select(g => g.Count())];
            if (groupLengths.Contains(2))
                two++;
            if (groupLengths.Contains(3))
                three++;
        }

        Console.WriteLine($"The checksum is: {two * three}");
    }

    public override void Part2()
    {
        base.Part2();

        string commonLetters = string.Empty;
        for (int i = 0; i < Input.Length - 1; i++)
        {
            for (int j = i + 1; j < Input.Length; j++)
            {
                if (Distance(Input[i], Input[j]) == 1)
                {
                    commonLetters = CommonLetters(Input[i], Input[j]);
                    break;
                }
            }

            if (!string.IsNullOrEmpty(commonLetters))
                break;
        }

        Console.WriteLine($"The common letters between the two IDs are: {commonLetters}");
    }

    private static int Distance(string a, string b)
    {
        int distance = 0;

        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i])
                distance++;

        return distance;
    }

    private static string CommonLetters(string a, string b)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < a.Length; i++)
            if (a[i] == b[i])
                sb.Append(a[i]);

        return sb.ToString();
    }
}
