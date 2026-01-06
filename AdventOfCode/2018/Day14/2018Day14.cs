namespace AdventOfCode._2018.Day14;

public class _2018Day14() : _2018Day("Day14")
{
    public override void Part1()
    {
        base.Part1();

        int recepieCount = int.Parse(Input[0]) + 10;
        var recepies = new List<byte> { 3, 7 };
        int elf1 = 0, elf2 = 1;

        while (true)
        {
            int sum = recepies[elf1] + recepies[elf2];
            if (sum >= 10)
            {
                recepies.Add(1);
                if (recepies.Count == recepieCount)
                    break;
            }

            recepies.Add((byte)(sum % 10));
            if (recepies.Count == recepieCount)
                break;

            elf1 = (elf1 + 1 + recepies[elf1]) % recepies.Count;
            elf2 = (elf2 + 1 + recepies[elf2]) % recepies.Count;
        }

        Console.WriteLine($"The scores are: {string.Join("", recepies[^10..])}");
    }

    public override void Part2()
    {
        base.Part2();

        byte[] pattern = [.. Input[0].Select(c => (byte)(c - '0'))];
        var recepies = new List<byte> { 3, 7 };
        int elf1 = 0, elf2 = 1;

        var solution = new Queue<char>();
        while (true)
        {
            int sum = recepies[elf1] + recepies[elf2];
            if (sum >= 10)
            {
                recepies.Add(1);
                if (EndsWith(recepies, pattern))
                    break;
            }

            recepies.Add((byte)(sum % 10));
            if (EndsWith(recepies, pattern))
                break;

            elf1 = (elf1 + 1 + recepies[elf1]) % recepies.Count;
            elf2 = (elf2 + 1 + recepies[elf2]) % recepies.Count;
        }

        Console.WriteLine($"There are {recepies.Count - Input[0].Length} recepies before the pattern.");
    }

    private static bool EndsWith(List<byte> list, byte[] pattern)
    {
        if (list.Count < pattern.Length)
            return false;

        int offset = list.Count - pattern.Length;
        for (int i = 0; i < pattern.Length; i++)
            if (list[i + offset] != pattern[i])
                return false;

        return true;
    }
}
