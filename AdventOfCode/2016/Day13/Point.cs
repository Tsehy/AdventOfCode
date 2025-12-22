using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2016.Day13;

internal readonly struct Point(int x, int y, int moves = 0)
{
    public readonly int X { get; } = x;
    public readonly int Y { get; } = y;
    public readonly int Moves { get; } = moves;

    public int GetHeuristic(Point goal) => Math.Abs(X - goal.X) + Math.Abs(Y + goal.Y);

    public bool IsWall(int secret)
    {
        int num = X * X + 3 * X + 2 * X * Y + Y + Y * Y + secret;
        return Convert.ToString(num, 2).Count(c => c == '1') % 2 == 1;
    }

    public IEnumerable<Point> Neighbours(int secret)
    {
        int[] directions = [-1, 1];

        foreach (int xMove in directions)
        {
            int newX = X + xMove;
            if (newX < 0)
                continue;

            var np = new Point(newX, Y, Moves + 1);
            if (np.IsWall(secret))
                continue;

            yield return np;
        }

        foreach (int yMove in directions)
        {
            int newY = Y + yMove;
            if (newY < 0)
                continue;

            var np = new Point(X, newY, Moves + 1);
            if (np.IsWall(secret))
                continue;

            yield return np;
        }
    }

    public override string ToString() => $"{X}:{Y} - {Moves}";
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null)
            return false;

        if (obj is Point other)
            return X == other.X && Y == other.Y;

        return false;
    }

    public static bool operator ==(Point left, Point right) => left.Equals(right);
    public static bool operator !=(Point left, Point right) => !left.Equals(right);
}
