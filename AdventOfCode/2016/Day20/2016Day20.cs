namespace AdventOfCode._2016.Day20;

public class _2016Day20 : _2016Day
{
    public List<(uint From, uint To)> Ranges;

    public _2016Day20() : base("Day20")
    {
        Ranges = [.. Input.Select(i => i.Split('-').Select(i => uint.Parse(i)).ToArray()).Select(i => (From: i[0], To: i[1])).OrderBy(i => i.From)];

        // Simplification for part2
        bool changed = true;
        while (changed)
        {
            changed = false;
            for (int i = Ranges.Count - 1; i > 0; i--)
            {
                var (prevFrom, prevTo) = Ranges[i - 1];
                var (thisFrom, thisTo) = Ranges[i];
                if (prevFrom <= thisFrom && prevTo >= thisTo)
                {
                    // prev includes this
                    Ranges.RemoveAt(i);
                    changed = true;
                }
                else if (prevTo >= thisFrom)
                {
                    // merge prev and this
                    Ranges.RemoveAt(i);
                    Ranges[i - 1] = (prevFrom, thisTo);
                    changed = true;
                }
            }
        }
    }

    public override void Part1()
    {
        base.Part1();

        uint lowest = 0;
        int index = 0;
        while (index < Ranges.Count)
        {
            if (lowest < Ranges[index].From)
                break;

            if (lowest <= Ranges[index].To)
                lowest = Ranges[index].To + 1;

            index++;
        }

        Console.WriteLine($"First not blocked IP: {lowest}");
    }

    public override void Part2()
    {
        base.Part2();

        uint allowedCount = 0;
        for (int index = 0; index < Ranges.Count - 1; index++)
        {
            uint nexStart = Ranges[index + 1].From;
            uint currEnd = Ranges[index].To;
            if (currEnd + 1 < nexStart)
            {
                uint diff = nexStart - currEnd - 1;
                allowedCount += diff;
            }
        }

        uint end = Ranges[^1].To;
        if (end < uint.MaxValue)
        {
            uint endDiff = uint.MaxValue - end - 1;
            allowedCount += endDiff;
        }

        Console.WriteLine($"{allowedCount} IPs are allowed.");
    }
}
