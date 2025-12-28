namespace AdventOfCode._2017.Day13;

internal readonly record struct Firewall(int Depth, int Range)
{
    public int Position(int deltaTime)
    {
        if (Range == 1)
            return 0;

        int maxValue = (2 * Range) - 2;
        int position = deltaTime % maxValue;
        if (position < maxValue)
            return position;
        else
            return maxValue - position;
    }
}

public class _2017Day13 : _2017Day
{
    private readonly List<Firewall> Firewalls;

    public _2017Day13() : base("Day13")
    {
        Firewalls = [.. Input.Select(i =>
        {
            string[] parts = i.Split(": ");
            return new Firewall(int.Parse(parts[0]), int.Parse(parts[1]));
        })];
    }

    public override void Part1()
    {
        base.Part1();

        int totalSeverity = 0;
        foreach (var fw in Firewalls)
        {
            int pos = fw.Position(fw.Depth);
            if (pos == 0)
                totalSeverity += fw.Depth * fw.Range;
        }

        Console.WriteLine($"The total severity is: {totalSeverity}");
    }

    public override void Part2()
    {
        base.Part2();

        int delay = 0;
        bool caught;
        do
        {
            caught = false;
            foreach (var fw in Firewalls)
            {
                int pos = fw.Position(fw.Depth + delay);
                if (pos == 0)
                {
                    delay++;
                    caught = true;
                    break;
                }
            }
        }
        while (caught);

        Console.WriteLine($"The package should be delayed {delay} picoseconds.");
    }
}
