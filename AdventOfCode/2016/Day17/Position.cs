using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016.Day17;

internal class Position(int x, int y, string password, string path)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public string Path { get; set; } = path;
    public string Hash { get; set; } = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(password + path)))[..4].ToLower();
    public int Moves => Path.Length;

    public int Heuristic(int x, int y) => Math.Abs(x - X) + Math.Abs(y - Y);

    public IEnumerable<Position> Neighbours(string password)
    {
        var doors = Hash.Select((c, i) => (DoorIndex: i, IsOpen: c > 'a')).Where(p => p.IsOpen);
        foreach ((int doorIndex, _) in doors)
        {
            int newX = X, newY = Y;
            string newPath = Path;
            switch (doorIndex)
            {
                case 0:
                    newY--;
                    newPath += 'U';
                    break;

                case 1:
                    newY++;
                    newPath += 'D';
                    break;

                case 2:
                    newX--;
                    newPath += 'L';
                    break;

                case 3:
                    newX++;
                    newPath += 'R';
                    break;
            }

            if (newX < 0 || newX > 3 || newY < 0 || newY > 3)
                continue;

            yield return new Position(newX, newY, password, newPath);
        }
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Hash);
    public override bool Equals([NotNullWhen(true)]object? obj)
    {
        if (obj == null)
            return false;

        if (obj is Position other)
            return other.Y == Y && other.X == X && other.Hash == Hash;

        return false;
    }
}
