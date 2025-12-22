using System.Text.RegularExpressions;

namespace AdventOfCode._2016.Day15;

internal readonly record struct Disk(int Segments, int Position, int Delay)
{
    public int PositionAfterTime(int seconds) => (Position + Delay + seconds) % Segments;
}

public partial class _2016Day15 : _2016Day
{
    private readonly List<Disk> Disks;

    [GeneratedRegex(@"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+)\.", RegexOptions.Compiled)]
    private static partial Regex DiskRegex();

    public _2016Day15() : base("Day15")
    {
        Disks = [.. from i in Input
                    let m = DiskRegex().Match(i).Groups.Values.Select(v => int.TryParse(v.Value, out int val) ? val : 0).ToList()
                    select new Disk(m[2], m[3], m[1])];
    }

    public override void Part1()
    {
        base.Part1();

        int time = 0;
        while (Disks.Any(d => d.PositionAfterTime(time) != 0))
            time++;

        Console.WriteLine($"Press the button at: {time}");
    }

    public override void Part2()
    {
        base.Part2();

        Disks.Add(new Disk(11, 0, Disks.Max(d => d.Delay) + 1));

        int time = 0;
        while (Disks.Any(d => d.PositionAfterTime(time) != 0))
            time++;

        Console.WriteLine($"Press the button at: {time}");
    }
}
