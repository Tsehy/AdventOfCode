using System.Text;

namespace AdventOfCode._2017.Cryptography;

internal static class KnotHash
{
    private static readonly byte[] Appendix = [17, 31, 73, 47, 23];

    public static void OneStep(CircularList cList, byte[] bytes, ref int position, ref int skipSize)
    {
        foreach (byte val in bytes)
        {
            cList.Twist(position, val);
            position = (position + val + skipSize) % 256;
            skipSize++;
        }
    }

    public static byte[] Encode(string plainText)
    {
        var sparseHash = new CircularList();
        int position = 0, skipSize = 0;
        byte[] bytes = [.. Encoding.ASCII.GetBytes(plainText), .. Appendix];

        for (int i = 0; i < 64; i++)
            OneStep(sparseHash, bytes, ref position, ref skipSize);

        byte[] denseHash = new byte[16];
        for (int i = 0; i < 16; i++)
        {
            byte num = sparseHash[i * 16];
            for (int j = 1; j < 16; j++)
                num = (byte)(num ^ sparseHash[i * 16 + j]);
            denseHash[i] = num;
        }

        return denseHash;
    }
}
