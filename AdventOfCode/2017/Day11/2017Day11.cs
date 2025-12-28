namespace AdventOfCode._2017.Day11;

// Thanks to: https://www.redblobgames.com/grids/hexagons/
internal readonly record struct Point(int Q, int R, int S)
{
    public int Length => (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
    public static Point operator +(Point left, Point right) => new(left.Q + right.Q, left.R + right.R, left.S + right.S);
    public static Point operator -(Point left, Point right) => new(left.Q - right.Q, left.R - right.R, left.S - right.S);
}

public class _2017Day11 : _2017Day
{
    private readonly Dictionary<string, Point> Directions = new()
    {
        ["n"]  = new( 0, -1,  1),
        ["ne"] = new( 1, -1,  0),
        ["se"] = new( 1,  0, -1),
        ["s"]  = new( 0,  1, -1),
        ["sw"] = new(-1,  1,  0),
        ["nw"] = new(-1,  0,  1),
    };

    private readonly int FinalDistance;
    private readonly int MaxDistance;

    public _2017Day11() : base("Day11")
    {
        string[] moves = Input[0].Split(',');

        Point position = new(0, 0, 0);
        MaxDistance = position.Length;

        foreach (string direction in moves)
        {
            position += Directions[direction];
            if (position.Length > MaxDistance)
                MaxDistance = position.Length;
        }

        FinalDistance = position.Length;
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The child process is {FinalDistance} steps away.");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The furthest reached point was {MaxDistance} steps away.");
    }
}
