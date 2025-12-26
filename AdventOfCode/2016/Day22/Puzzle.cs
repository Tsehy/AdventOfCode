using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2016.Day22;

internal struct PuzzleState(int moves, int goalX, int goalY, int emptyX, int emptyY)
{
    public int Moves { get; set; } = moves;
    public int GoalX { get; set; } = goalX;
    public int GoalY { get; set; } = goalY;
    public int EmptyX { get; set; } = emptyX;
    public int EmptyY { get; set; } = emptyY;
    public readonly int Heuristic()
    {
        return (GoalX + GoalY) * 5; // It takes min 5 steps to move the goal one step closer
    }
    public override readonly int GetHashCode() => HashCode.Combine(GoalX, GoalY, EmptyX, EmptyY);
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null)
            return false;

        if (obj is PuzzleState other)
            return other.GoalX == GoalX && other.GoalY == GoalY && other.EmptyX == EmptyX && other.EmptyY == EmptyY;

        return false;
    }

    internal readonly PuzzleState MoveTo(int x, int y)
    {
        int gx, gy;
        if (GoalX == x && GoalY == y)
        {
            gx = EmptyX;
            gy = EmptyY;
        }
        else
        {
            gx = GoalX;
            gy = GoalY;
        }
        return new PuzzleState(Moves + 1, gx, gy, x, y);
    }
}

internal class Puzzle
{
    public List<PuzzleNode> Grid { get; set; } = [];

    public IEnumerable<PuzzleState> GetNeighbours(PuzzleState ps)
    {
        var node = Grid.First(n => n.X == ps.EmptyX && n.Y == ps.EmptyY);
        foreach (var n in node.Neighbours)
            yield return ps.MoveTo(n.X, n.Y);
    }

    public Puzzle(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            if (!node.IsUnmoveable)
            {
                Grid.Add(new PuzzleNode(node));
            }
        }

        foreach (var node in Grid)
        {
            node.Neighbours.AddRange(Grid.Where(n => (n.X == node.X && Math.Abs(n.Y - node.Y) == 1) || (n.Y == node.Y && Math.Abs(n.X - node.X) == 1)));
        }
    }
}

internal struct PuzzleNode(Node node)
{
    public int X { get; set; } = node.X;
    public int Y { get; set; } = node.Y;
    public List<PuzzleNode> Neighbours { get; set; } = [];
}
