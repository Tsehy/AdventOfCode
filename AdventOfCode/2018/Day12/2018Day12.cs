namespace AdventOfCode._2018.Day12;

public class _2018Day12 : _2018Day
{
    private readonly HashSet<int> Plants = [];
    private readonly HashSet<int> GrowRules = [];

    public _2018Day12() : base("Day12")
    {
        string initialState = Input[0].Split(' ')[2];
        for (int i = 0; i < initialState.Length; i++)
            if (initialState[i] == '#')
                Plants.Add(i);

        for (int i = 2; i < Input.Length; i++)
        {
            string[] parts = Input[i].Split(' ');
            if (parts[2] == "#")
            {
                int bitMask = parts[0].Select((c, i) => c == '.' ? 0 : (1 << (4 - i))).Sum();
                GrowRules.Add(bitMask);
            }
        }
    }

    private HashSet<int> Step(HashSet<int> plants)
    {
        var nextGen = new HashSet<int>();

        int min = plants.Min() - 2;
        int max = plants.Max() + 2;

        for (int pos = min; pos <= max; pos++)
        {
            int mask = 0;
            for (int dx = -2; dx <= 2; dx++)
            {
                mask <<= 1;
                if (plants.Contains(pos + dx))
                    mask |= 1;
            }

            if (GrowRules.Contains(mask))
                nextGen.Add(pos);
        }

        return nextGen;
    }

    private long FastIterate(long count)
    {
        var plants = new HashSet<int>(Plants);
        long lastSum = plants.Sum();
        long lastDelta = 0;

        for (long i = 1; i <= count; i++)
        {
            plants = Step(plants);

            long sum = plants.Sum();
            long delta = sum - lastSum;

            if (delta == lastDelta)
            {
                long remaining = count - i;
                lastSum = sum + remaining * delta;
                break;
            }

            lastSum = sum;
            lastDelta = delta;
        }

        return lastSum;
    }

    public override void Part1()
    {
        base.Part1();

        long plants = FastIterate(20);
        Console.WriteLine($"The plant cout is: {plants}");
    }

    public override void Part2()
    {
        base.Part2();

        long plants = FastIterate(50_000_000_000);
        Console.WriteLine($"The plant cout is: {plants}");
    }
}
