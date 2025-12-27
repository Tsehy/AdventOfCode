namespace AdventOfCode._2017.Day06;

public class _2017Day06 : _2017Day
{
    private readonly List<int> Banks;

    public _2017Day06() : base("Day06")
    {
        Banks = Input[0].Split('\t').Select(int.Parse).ToList();
    }

    private static void Redistribue(List<int> banks)
    {
        int sourceIndex = 0;
        for (int i = 1; i < banks.Count; i++)
            if (banks[i] > banks[sourceIndex])
                sourceIndex = i;

        (int q, int rem) = Math.DivRem(banks[sourceIndex], banks.Count);
        banks[sourceIndex] = 0;
        for (int i = sourceIndex + 1; i <= sourceIndex + banks.Count; i++)
        {
            banks[i % banks.Count] += q + (rem != 0 ? 1 : 0);
            if (rem > 0)
                rem--;
        }
    }

    public override void Part1()
    {
        base.Part1();

        HashSet<string> states = [];

        int steps = 0;
        while (states.Add(string.Join(",", Banks)))
        {
            Redistribue(Banks);
            steps++;
        }

        Console.WriteLine($"After {steps} steps we reach an already visited state.");
    }

    public override void Part2()
    {
        base.Part2();

        string start = string.Join(",", Banks);

        int steps = 0;
        do
        {
            Redistribue(Banks);
            steps++;
        } while (string.Join(",", Banks) != start);

        Console.WriteLine($"The cycle is {steps} steps long.");
    }
}
