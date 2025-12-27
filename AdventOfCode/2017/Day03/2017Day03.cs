namespace AdventOfCode._2017.Day03;

public class _2017Day03 : _2017Day
{
    private readonly int Number;

    public _2017Day03() : base("Day03")
    {
        Number = int.Parse(Input[0]);
    }

    private static int GetSteps(int number)
    {
        int n = 1;
        while (n * n < number)
            n += 2;

        int pos = n * n,
            max = n - 1,
            min = max / 2,
            direction = -1,
            val = n - 1;

        while (pos > number)
        {
            pos--;
            if (val + direction > max)
                direction = -1;
            else if (val + direction < min)
                direction = 1;
            val += direction;
        }

        return val;
    }

    public override void Part1()
    {
        base.Part1();

        int val = GetSteps(Number);
        Console.WriteLine($"{val} steps are required to reach the access port.");
    }

    private static int GetNextValue(int x, int y, Dictionary<(int, int), int> grid)
    {
        int sum = 0;

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (grid.TryGetValue((x + dx, y + dy), out int value))
                    sum += value;

        return sum;
    }

    private static int GetLarger(int number)
    {
        var points = new Dictionary<(int, int), int> { [(0, 0)] = 1 };

        int x = 0, y = 0;
        int sideLength = 0;

        // left, up, right, down
        (int dx, int dy)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];
        while (true)
        {
            foreach ((int dx, int dy) in directions)
            {
                for (int i = 0; i < sideLength; i++)
                {
                    x += dx;
                    y += dy;

                    int val = GetNextValue(x, y, points);
                    if (val > number)
                        return val;

                    points[(x, y)] = val;
                }

                // every second side will be larger
                if (dx == 0)
                    sideLength++;
            }
        }
    }

    public override void Part2()
    {
        base.Part2();

        int val = GetLarger(Number);
        Console.WriteLine($"{val} is the first larger value.");
    }
}
