namespace AdventOfCode._2018.Day06;

internal readonly struct Point(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public int ManhattanDistance(Point other) => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
}

public class _2018Day06 : _2018Day
{
    private readonly int MaxFiniteRegion;
    private readonly int ClosestArea = 0;

    public _2018Day06() : base("Day06")
    {
        List<Point> Points = [];

        int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue;
        foreach (string line in Input)
        {
            string[] parts = line.Split(", ");
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            Points.Add(new(x, y));
            if (x < minx) minx = x;
            if (x > maxx) maxx = x;
            if (y < miny) miny = y;
            if (y > maxy) maxy = y;
        }

        Dictionary<int, int> grid = [];
        HashSet<int> onEdge = [];

        for (int y = miny; y <= maxy; y++)
        {
            for (int x = minx; x <= maxx; x++)
            {
                var point = new Point(x, y);
                int minDist = Points.Min(point.ManhattanDistance);
                var closest = Points.Select((p, i) => (point: p, id: i)).Where(p => point.ManhattanDistance(p.point) == minDist).ToArray();
                if (closest.Length == 1)
                {
                    int id = closest[0].id;

                    if (!grid.TryGetValue(id, out int value))
                        grid[id] = 1;
                    else
                        grid[id] = value + 1;

                    if (x == minx || x == maxx || y == miny || y == maxy)
                        onEdge.Add(id);
                }

                int totalDistance = Points.Sum(point.ManhattanDistance);
                if (totalDistance < 10_000)
                    ClosestArea++;
            }
        }

        MaxFiniteRegion = grid.Where(vp => !onEdge.Contains(vp.Key)).Max(vp => vp.Value);
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The area of the biggest finite region is: {MaxFiniteRegion}");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The area's size is: {ClosestArea}");
    }
}
