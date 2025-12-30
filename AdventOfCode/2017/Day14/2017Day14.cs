using AdventOfCode._2017.Cryptography;

namespace AdventOfCode._2017.Day14;

internal readonly record struct Point(int X, int Y)
{
    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
}

public class _2017Day14 : _2017Day
{
    private readonly List<Point> Grid = [];

    public _2017Day14() : base("Day14")
    {
        for (int row = 0; row < 128; row++)
        {
            byte[] encoded = KnotHash.Encode($"{Input[0]}-{row}");
            for (int byteIndex = 0; byteIndex < encoded.Length; byteIndex++)
            {
                string s = Convert.ToString(encoded[byteIndex], 2).PadLeft(8, '0');
                for (int bitIndex = 0; bitIndex < s.Length; bitIndex++)
                    if (s[bitIndex] == '1')
                        Grid.Add(new((byteIndex * 8) + bitIndex, row));
            }
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"There are {Grid.Count} used squares.");
    }

    public override void Part2()
    {
        base.Part2();

        int groups = 0;

        var points = new List<Point>(Grid);
        Point[] directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];

        while (points.Count > 0)
        {
            groups++;

            var open = new Queue<Point>();
            open.Enqueue(points[0]);
            points.RemoveAt(0);

            while (open.Count > 0)
            {
                var current = open.Dequeue();

                foreach (var d in directions)
                {
                    Point np = current + d;
                    if (points.Remove(np))
                        open.Enqueue(np);
                }
            }
        }

        Console.WriteLine($"There are {groups} groups.");
    }
}
