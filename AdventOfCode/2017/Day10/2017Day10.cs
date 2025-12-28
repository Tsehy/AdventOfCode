using AdventOfCode._2017.Cryptography;

namespace AdventOfCode._2017.Day17;

public class _2017Day10 : _2017Day
{
    public _2017Day10() : base("Day10")
    {
    }

    public override void Part1()
    {
        base.Part1();

        List<int> lengths = [.. Input[0].Split(',').Select(int.Parse)];
        var cList = new CircularList(256);
        int position = 0, skipSize = 0;
        KnotHasher.OneStep(cList, lengths, ref position, ref skipSize);

        Console.WriteLine($"The checksum is: {cList[0] * cList[1]}");
    }

    public override void Part2()
    {
        base.Part2();

        string cipher = KnotHasher.Encode(Input[0].Trim());
        Console.WriteLine($"The Knot Hash is: {cipher}");
    }
}
