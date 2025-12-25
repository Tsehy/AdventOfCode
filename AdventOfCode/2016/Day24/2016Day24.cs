namespace AdventOfCode._2016.Day24;

internal readonly record struct Node(int X, int Y, List<Node> Neighbours)
{
    public readonly int Heuristic(Node goal) => Math.Abs(X - goal.X) + Math.Abs(Y - goal.Y);
}

internal readonly record struct TspEdge(int From, int To, int Cost);

public class _2016Day24 : _2016Day
{
    private readonly List<TspEdge> Edges = [];
    private readonly Dictionary<int, Node> Goals = [];

    public _2016Day24() : base("Day24")
    {
        List<Node> maze = [];

        for (int row = 0; row < Input.Length; row++)
        {
            for (int col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == '#')
                    continue;

                var newNode = new Node(row, col, []);
                maze.Add(newNode);

                if (Input[row][col] != '.')
                    Goals[Input[row][col] - '0'] = newNode;
            }
        }

        foreach (var node in maze)
            node.Neighbours.AddRange(maze.Where(n => (n.X == node.X && Math.Abs(n.Y - node.Y) == 1) || (n.Y == node.Y && Math.Abs(n.X - node.X) == 1)));

        foreach (int key in Goals.Keys)
        {
            var curr = Goals[key];
            Goals[key] = maze.First(n => n.X == curr.X && n.Y == curr.Y);
        }

        var keys = Goals.Keys;
        for (int i = 0; i < keys.Count + 1; i++)
        {
            for (int j = i + 1; j < keys.Count; j++)
            {
                int min = ShortestPathAstar(Goals[i], Goals[j]);
                Edges.Add(new(i, j, min));
                Edges.Add(new(j, i, min));
            }
        }
    }

    private static int ShortestPathAstar(Node start, Node goal)
    {
        var open = new PriorityQueue<(Node current, int moves), int>();
        var best = new Dictionary<Node, int>();

        open.Enqueue((start, 0), start.Heuristic(goal));
        best[start] = 0;

        while (open.Count > 0)
        {
            (Node current, int moves) = open.Dequeue();

            if (current == goal)
                return moves;

            int newMoves = moves + 1;
            foreach (var neighbour in current.Neighbours)
            {
                if (!best.TryGetValue(neighbour, out int prevMoves) || newMoves < prevMoves)
                {
                    open.Enqueue((neighbour, newMoves), newMoves + neighbour.Heuristic(goal));
                    best[neighbour] = newMoves;
                }
            }
        }

        return -1;
    }

    private static int TspBranchAndBound(List<TspEdge> edges, int start, int nodeCount, bool returnHome = false)
    {
        var open = new PriorityQueue<List<int>, int>();

        int bestCost = int.MaxValue;

        open.Enqueue([start], 0);

        while (open.Count > 0)
        {
            if (!open.TryDequeue(out var path, out int currentCost))
                break;

            int last = path.Last();

            if (currentCost > bestCost)
                continue;

            if (path.Count == nodeCount)
            {
                if (returnHome)
                    currentCost += edges.First(e => e.From == last && e.To == start).Cost;

                if (currentCost < bestCost)
                    bestCost = currentCost;

                continue;
            }

            foreach (var edge in edges.Where(e => e.From == last && !path.Contains(e.To)))
            {
                int newCost = currentCost + edge.Cost;

                if (newCost < bestCost)
                {
                    var np = new List<int>(path) { edge.To };
                    open.Enqueue(np, newCost);
                }
            }
        }

        return bestCost;
    }

    public override void Part1()
    {
        base.Part1();

        int minTsp = TspBranchAndBound(Edges, 0, Goals.Keys.Count);
        Console.WriteLine($"The shortest path is: {minTsp}");
    }

    public override void Part2()
    {
        base.Part2();

        int minTsp = TspBranchAndBound(Edges, 0, Goals.Keys.Count, returnHome: true);
        Console.WriteLine($"The shortest path with returning to home is: {minTsp}");
    }
}
