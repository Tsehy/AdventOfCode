using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016.Day14;

public partial class _2016Day14 : _2016Day
{
    private readonly string Salt;
    private readonly Dictionary<int, string> Hashes = [];

    public _2016Day14() : base("Day14")
    {
        Salt = Input[0];
    }

    public override void Part1()
    {
        base.Part1();

        int index = FindIndex(Encode);
        Console.WriteLine($"Index of the 64th key: {index}");
    }

    public override void Part2()
    {
        base.Part2();

        int index = FindIndex(LongEncode);
        Console.WriteLine($"Index of the 64th stretched key: {index}");
    }

    private static string Encode(string key) => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(key))).ToLower();
    private static string LongEncode(string key)
    {
        string tmp = key;
        for (int i = 0; i < 2017; i++)
            tmp = Encode(tmp);
        return tmp;
    }

    [GeneratedRegex(@"(\w)\1\1", RegexOptions.Compiled)]
    private static partial Regex HasTriplet();

    public bool TestIndex(string key, int index, Func<string, string> Encoder)
    {
        string encoded = Encoder.Invoke($"{key}{index}");
        var match = HasTriplet().Match(encoded);

        if (!match.Success)
            return false;

        string searchString = new(match.Value[0], 5);
        for (int testIndex = index + 1; testIndex <= index + 1000; testIndex++)
        {
            if (!Hashes.TryGetValue(testIndex, out string? hash))
            {
                hash = Encoder.Invoke($"{key}{testIndex}");
                Hashes[testIndex] = hash;
            }

            if (hash.Contains(searchString))
                return true;
        }

        return false;
    }

    private int FindIndex(Func<string, string> Encoder)
    {
        Hashes.Clear();
        int count = 0;
        int index = 0;
        while (count < 64)
        {
            if (TestIndex(Salt, index, Encoder))
                count++;
            index++;
        }

        return index - 1;
    }
}
