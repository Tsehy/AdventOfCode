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

        byte[] lengths = [.. Input[0].Split(',').Select(i => (byte)int.Parse(i))];
        var cList = new CircularList();
        int position = 0, skipSize = 0;
        KnotHash.OneStep(cList, lengths, ref position, ref skipSize);

        Console.WriteLine($"The checksum is: {cList[0] * cList[1]}");
    }

    public override void Part2()
    {
        base.Part2();

        string cipher = Convert.ToHexString(KnotHash.Encode(Input[0].Trim())).ToLower();
        Console.WriteLine($"The Knot Hash is: {cipher}");
    }
}
