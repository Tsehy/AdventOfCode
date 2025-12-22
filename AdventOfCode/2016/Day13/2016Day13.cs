namespace AdventOfCode._2016.Day13;

public class _2016Day13 : _2016Day
{
    private readonly int Secret;

    public _2016Day13() : base("Day13")
    {
        Secret = int.Parse(Input[0]);
    }

    private int MinMoves(Point start, Point goal)
    {
        var open = new PriorityQueue<Point, int>();
        var best = new Dictionary<Point, int>();

        open.Enqueue(start, 0);
        best[start] = start.Moves;

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current == goal)
                return current.Moves;

            foreach (var neighbour in current.Neighbours(Secret))
            {
                if (!best.TryGetValue(neighbour, out int prevMoves) || neighbour.Moves < prevMoves)
                {
                    best[neighbour] = neighbour.Moves;
                    open.Enqueue(neighbour, neighbour.Moves + neighbour.GetHeuristic(goal));
                }
            }
        }

        return -1;
    }

    public override void Part1()
    {
        base.Part1();

        int min = MinMoves(new Point(1, 1), new Point(31, 39));
        Console.WriteLine($"Fewest moves to the goal: {min}");
    }

    private int ReachablePoints(Point start, int moves)
    {
        var open = new Queue<Point>();
        var visited = new HashSet<Point>();

        open.Enqueue(start);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            visited.Add(current);

            if (current.Moves >= moves)
                continue;

            foreach (var neighbour in current.Neighbours(Secret))
                if (!visited.Contains(neighbour))
                    open.Enqueue(neighbour);
        }

        return visited.Count;
    }

    public override void Part2()
    {
        base.Part2();

        int count = ReachablePoints(new Point(1, 1), 50);
        Console.WriteLine($"Number of reachable points: {count}");
    }
}
