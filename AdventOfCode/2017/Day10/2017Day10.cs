using System.Text;

namespace AdventOfCode._2017.Day10;

public class _2017Day10 : _2017Day
{
    private readonly int[] Appendix = [17, 31, 73, 47, 23];

    public _2017Day10() : base("Day10")
    {
    }

    private static void KnotHashStep(CircularList cList, List<int> bytes, ref int position, ref int skipSize)
    {
        foreach (int length in bytes)
        {
            cList.Twist(position, length);
            position = (position + length + skipSize) % cList.Count;
            skipSize++;
        }
    }

    private string KnotHash(string plainText)
    {
        var cList = new CircularList(256);
        int position = 0, skipSize = 0;
        List<int> bytes = [.. Encoding.ASCII.GetBytes(plainText).Select(b => (int)b)];
        bytes.AddRange(Appendix);

        for (int i = 0; i < 64; i++)
            KnotHashStep(cList, bytes, ref position, ref skipSize);

        byte[] sparseHash = [.. cList.ToList().Select(i => (byte)i)];
        byte[] denseHash = new byte[16];
        for (int i = 0; i < 16; i++)
        {
            byte num = sparseHash[i * 16];
            for (int j = 1; j < 16; j++)
                num = (byte)(num ^ sparseHash[i * 16 + j]);
            denseHash[i] = num;
        }

        return Convert.ToHexString(denseHash).ToLower();
    }

    public override void Part1()
    {
        base.Part1();

        List<int> lengths = [.. Input[0].Split(',').Select(int.Parse)];
        var cList = new CircularList(256);
        int position = 0, skipSize = 0;
        KnotHashStep(cList, lengths, ref position, ref skipSize);

        Console.WriteLine($"The checksum is: {cList[0] * cList[1]}");
    }

    public override void Part2()
    {
        base.Part2();

        string cipher = KnotHash(Input[0].Trim());
        Console.WriteLine($"The Knot Hash is: {cipher}");
    }
}
