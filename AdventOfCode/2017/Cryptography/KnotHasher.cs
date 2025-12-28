using System.Text;

namespace AdventOfCode._2017.Cryptography;

internal static class KnotHasher
{
    private static readonly int[] Appendix = [17, 31, 73, 47, 23];

    public static void OneStep(CircularList cList, List<int> bytes, ref int position, ref int skipSize)
    {
        foreach (int length in bytes)
        {
            cList.Twist(position, length);
            position = (position + length + skipSize) % cList.Count;
            skipSize++;
        }
    }

    public static string Encode(string plainText)
    {
        var cList = new CircularList(256);
        int position = 0, skipSize = 0;
        List<int> bytes = [.. Encoding.ASCII.GetBytes(plainText).Select(b => (int)b)];
        bytes.AddRange(Appendix);

        for (int i = 0; i < 64; i++)
            OneStep(cList, bytes, ref position, ref skipSize);

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
}
