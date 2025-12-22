namespace AdventOfCode._2016.Day17;

public class _2016Day17 : _2016Day
{
    public readonly string Password;

    public _2016Day17() : base("Day17")
    {
        Password = Input[0];
    }

    private static string ShortestPath(int startX, int startY, string password, int goalX, int goalY)
    {
        var start = new Position(startX, startY, password, "");

        var open = new PriorityQueue<Position, int>();
        var best = new Dictionary<Position, int>();

        open.Enqueue(start, 0);
        best[start] = start.Moves;

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current.Heuristic(goalX, goalY) == 0)
                return current.Path;

            foreach (var neighbour in current.Neighbours(password))
            {
                if (!best.TryGetValue(neighbour, out int prevMoves) || neighbour.Moves < prevMoves)
                {
                    best[neighbour] = neighbour.Moves;
                    open.Enqueue(neighbour, neighbour.Moves + neighbour.Heuristic(goalX, goalY));
                }
            }
        }

        return string.Empty;
    }

    public override void Part1()
    {
        base.Part1();

        string path = ShortestPath(0, 0, Password, 3, 3);
        Console.WriteLine($"The shortest path is: {path}");
    }

    private static int LongestPath(int startX, int startY, string password, int goalX, int goalY)
    {
        var start = new Position(startX, startY, password, "");

        var open = new PriorityQueue<Position, int>();
        int max = int.MinValue;

        open.Enqueue(start, 0);

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current.Heuristic(goalX, goalY) == 0)
            {
                if (current.Moves > max)
                    max = current.Moves;
                continue;
            }

            foreach (var neighbour in current.Neighbours(password))
            {
                open.Enqueue(neighbour, neighbour.Moves + neighbour.Heuristic(goalX, goalY));
            }
        }

        return max;
    }

    public override void Part2()
    {
        base.Part2();

        int max = LongestPath(0, 0, Password, 3, 3);
        Console.WriteLine($"The length of the longest path is: {max}");
    }
}
