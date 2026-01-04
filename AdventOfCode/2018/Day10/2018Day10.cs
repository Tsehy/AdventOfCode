using System.Text.RegularExpressions;

namespace AdventOfCode._2018.Day10;

internal struct Point(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
}

internal class Light(Point pos, Point vel)
{
    public Point Position { get; set; } = pos;
    public Point Velocity { get; set; } = vel;

    public void Move() => Position += Velocity;
}

public partial class _2018Day10 : _2018Day
{
    private readonly List<Light> Lights = [];
    private readonly Point Min;
    private readonly Point Max;
    private readonly int Seconds = 0;

    [GeneratedRegex(@"(?<=<) *([0-9-]+), *([0-9-]+)", RegexOptions.Compiled)]
    private static partial Regex PointRegex();

    public _2018Day10() : base("Day10")
    {
        foreach (string line in Input)
        {
            var points = PointRegex().Matches(line).Select(m => new Point(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value))).ToArray();
            Lights.Add(new(points[0], points[1]));
        }

        do
        {
            Min = new(int.MaxValue, int.MaxValue);
            Max = new(int.MinValue, int.MinValue);
            foreach (var l in Lights)
            {
                l.Move();
                if (l.Position.Y < Min.Y) Min.Y = l.Position.Y;
                if (l.Position.Y > Max.Y) Max.Y = l.Position.Y;
                if (l.Position.X < Min.X) Min.X = l.Position.X;
                if (l.Position.X > Max.X) Max.X = l.Position.X;
            }
            Seconds++;
        }
        while (Max.Y - Min.Y > 9);
    }

    public override void Part1()
    {
        base.Part1();

        for (int row = Min.Y; row <= Max.Y; row++)
        {
            for (int col = Min.X; col <= Max.X; col++)
            {
                if (Lights.Any(l => l.Position.X == col && l.Position.Y == row))
                    Console.Write('#');
                else
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The elfs should have waited {Seconds} seconds.");
    }
}
