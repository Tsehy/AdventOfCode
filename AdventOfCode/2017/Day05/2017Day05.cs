namespace AdventOfCode._2017.Day05;

public class _2017Day05 : _2017Day
{
    private readonly List<int> Offsets;

    public _2017Day05() : base("Day05")
    {
        Offsets = [.. Input.Select(int.Parse)];
    }

    public override void Part1()
    {
        base.Part1();

        var copy = new List<int>(Offsets);
        int index = 0, steps = 0;
        while (index >= 0 && index < copy.Count)
        {
            int offset = copy[index];
            copy[index]++;
            index += offset;
            steps++;
        }

        Console.WriteLine($"It takes {steps} steps to reach the exit.");
    }

    public override void Part2()
    {
        base.Part2();

        var copy = new List<int>(Offsets);
        int index = 0, steps = 0;
        while (index >= 0 && index < copy.Count)
        {
            int offset = copy[index];
            if (offset >= 3)
                copy[index]--;
            else
                copy[index]++;
            index += offset;
            steps++;
        }

        Console.WriteLine($"It takes {steps} steps to reach the exit.");
    }
}
