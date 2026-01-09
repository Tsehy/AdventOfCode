using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2018.Day15;

internal class Node(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public List<Node> Neighbours { get; } = [];
    public CombatUnit? CurrentUnit { get; set; }

    public int Distance(Node other)
    {
        var visited = new HashSet<Node>();
        var open = new Queue<(Node, int)>();
        open.Enqueue((this, 1));

        while (open.Count > 0)
        {
            (var currentNode, int steps) = open.Dequeue();

            if (currentNode == other)
                return steps;

            if (!visited.Add(currentNode))
                continue;

            steps++;
            foreach (var neighbour in currentNode.Neighbours)
            {
                if (neighbour.CurrentUnit != null)
                    continue;

                open.Enqueue((neighbour, steps));
            }
        }

        return -1;
    }

    public Node DeepCopy()
    {
        var copy = new Node(X, Y);

        if (CurrentUnit != null)
            copy.CurrentUnit = new CombatUnit(CurrentUnit.IsElf, copy);

        return copy;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override bool Equals([NotNullWhen(true)] object? obj) => obj != null && obj is Node other && other.X == X && other.Y == Y;
}
