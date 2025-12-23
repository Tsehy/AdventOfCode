namespace AdventOfCode._2016.Day22;

public class _2016Day22 : _2016Day
{
    private readonly List<Node> Nodes;

    public _2016Day22() : base("Day22")
    {
        Nodes = [];
        for (int i = 2; i < Input.Length; i++)
        {
            string[] parts = Input[i].Split(' ', options: StringSplitOptions.RemoveEmptyEntries);
            Nodes.Add(new Node(parts[0], int.Parse(parts[1][..^1]), int.Parse(parts[2][..^1])));
        }

        foreach (var node in Nodes)
        {
            
            if (Nodes.All(n => n.Available < node.Used))
                node.IsUnmoveable = true;
        }
    }

    public override void Part1()
    {
        base.Part1();

        int count = 0;
        for (int i = 0; i < Nodes.Count; i++)
        {
            var a = Nodes[i];
            if (a.Used == 0)
                continue;

            for (int j = 0; j < Nodes.Count; j++)
            {
                if (i == j)
                    continue;

                if (a.Used < Nodes[j].Available)
                    count++;
            }
        }
        Console.WriteLine($"There are {count} viable pairs.");
    }

    private int MinMoves()
    {
        var p = new Puzzle(Nodes);
        var e = Nodes.First(n => n.Used == 0);

        var start = new PuzzleState
        {
            Moves = 0,
            GoalX = p.Grid.Max(n => n.X),
            GoalY = 0,
            EmptyX = e.X,
            EmptyY = e.Y,
        };

        var open = new PriorityQueue<PuzzleState, int>();
        var best = new Dictionary<PuzzleState, int>();

        open.Enqueue(start, 0);
        best[start] = start.Moves;

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current.Heuristic() == 0)
                return current.Moves;

            foreach (var neighbour in p.GetNeighbours(current))
            {
                if (!best.TryGetValue(neighbour, out int prevMoves) || neighbour.Moves < prevMoves)
                {
                    best[neighbour] = neighbour.Moves;
                    open.Enqueue(neighbour, neighbour.Moves + neighbour.Heuristic());
                }
            }
        }

        return -1;
    }

    public override void Part2()
    {
        base.Part2();

        int minMoves = MinMoves();
        Console.WriteLine($"Fewest number of steps: {minMoves}");
    }
}
