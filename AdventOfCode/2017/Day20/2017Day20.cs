using System.Text.RegularExpressions;

namespace AdventOfCode._2017.Day20;

internal readonly record struct Point(int X, int Y, int Z)
{
    public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
}

internal class Particle(int id, Point p, Point v, Point a)
{
    public int Id { get; } = id;
    public Point Position { get; set; } = p;
    public Point Velocity { get; set; } = v;
    public Point Acceleration { get; set; } = a;

    public /*bool*/ void Move()
    {
        //int start = Position.ManhattanDistance;
        Velocity += Acceleration;
        Position += Velocity;
        //return Position.ManhattanDistance <= start; // approaching zero
    }
}

public partial class _2017Day20 : _2017Day
{
    private readonly List<Particle> Particles = [];

    [GeneratedRegex(@"(?<=<)([0-9-]+),([0-9-]+),([0-9-]+)", RegexOptions.Compiled)]
    private static partial Regex PointRegex();


    public _2017Day20() : base("Day20")
    {
        int id = 0;
        foreach (string line in Input)
        {
            var matches = PointRegex().Matches(line);
            var points = matches.Select(m => new Point(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value))).ToArray(); ;
            Particles.Add(new(id++, points[0], points[1], points[2]));
        }
    }

    public override void Part1()
    {
        base.Part1();

        // The solution has nothing to do with the proper location of the particles, because eventually they all will move away from the origin
        //bool approaching;
        //do
        //{
        //    approaching = false;
        //    foreach (var p in Particles)
        //        approaching |= p.Move();
        //}
        //while (approaching);

        // They move away from the origin at different rates, and the slowest one is the solution
        var minParticle = Particles.MinBy(p => p.Acceleration.ManhattanDistance);
        Console.WriteLine($"Particle number {minParticle?.Id ?? -1} will stay closest to origin.");
    }

    public override void Part2()
    {
        base.Part2();

        for (int i = 100; i >= 0; i--)
        {
            foreach (var p in Particles)
                p.Move();

            var idsToRemove = Particles.GroupBy(p => p.Position).Where(g => g.Count() > 1).SelectMany(g => g).Select(p => p.Id);
            if (idsToRemove.Any())
            {
                Particles.RemoveAll(p => idsToRemove.Contains(p.Id));
                i = 100; // if we did remove any, we chack another 100 iterations
            }
        }

        Console.WriteLine($"There are {Particles.Count} particles remaining.");
    }
}
