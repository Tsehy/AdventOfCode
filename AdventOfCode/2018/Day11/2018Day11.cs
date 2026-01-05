namespace AdventOfCode._2018.Day11;

public class _2018Day11 : _2018Day
{
    private readonly Dictionary<(int X, int Y), int> Grid = [];

    public _2018Day11() : base("Day11")
    {
        int serial = int.Parse(Input[0]);
        for (int x = 1; x <= 300; x++)
        {
            for (int y = 1; y <= 300; y++)
            {
                int rackId = x + 10;
                int powerLevel = (rackId * y + serial) * rackId;
                powerLevel = (powerLevel % 1_000) / 100;
                Grid[(x, y)] = powerLevel - 5;
            }
        }
    }

    public override void Part1()
    {
        base.Part1();

        int maxPower = int.MinValue;
        string coordinate = string.Empty;

        for (int x = 2; x < 300; x++)
        {
            for (int y = 2; y < 300; y++)
            {
                int power = 0;

                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        power += Grid[(x + dx, y + dy)];

                if (power > maxPower)
                {
                    maxPower = power;
                    coordinate = $"{x - 1},{y - 1}";
                }
            }
        }

        Console.WriteLine($"The {coordinate} coordinate gives the largest total power.");
    }

    public override void Part2()
    {
        base.Part2();

        int maxPower = int.MinValue;
        string identifier = string.Empty;
        int fails = 0;

        for (int size = 1; size <= 300; size++)
        {
            int localMaxPower = int.MinValue;
            string localIdentifier = string.Empty;

            for (int x = 1; x + size <= 301; x++)
            {
                for (int y = 1; y + size <= 301; y++)
                {
                    int power = 0;

                    for (int dx = 0; dx < size; dx++)
                    {
                        for (int dy = 0; dy < size; dy++)
                        {
                            power += Grid[(x + dx, y + dy)];
                        }
                    }

                    if (power > localMaxPower)
                    {
                        localMaxPower = power;
                        localIdentifier = $"{x},{y},{size}";
                    }
                }
            }

            if (localMaxPower >= maxPower)
            {
                maxPower = localMaxPower;
                identifier = localIdentifier;
            }
            else if (fails < 5) // this feels like a hack, but if it works, it works
                fails++;
            else
                break;
        }

        Console.WriteLine($"The {identifier} identifier gives the largest total power.");
    }
}
