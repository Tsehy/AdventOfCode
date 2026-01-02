using System.Text;

namespace AdventOfCode._2017.Day21;

public class _2017Day21 : _2017Day
{
    private readonly Dictionary<string, bool[,]> Rules = [];
    private bool[,] Picture;
    private int Iter = 0;

    public _2017Day21() : base("Day21")
    {
        foreach (string line in Input)
        {
            string[] parts = line.Split(" => ");
            bool[,] source = ToArray(parts[0]);
            bool[,] target = ToArray(parts[1]);

            bool[,] current = source;
            for (int r = 0; r < 4; r++)
            {
                string id = ToString(current);
                Rules[id] = target;

                bool[,] flipped = Flip(current);
                string flippedId = ToString(flipped);
                Rules[flippedId] = target;

                current = Rotate90(current);
            }
        }

        Picture = ToArray(".#./..#/###");
    }

    public override void Part1()
    {
        base.Part1();

        for (; Iter < 5; Iter++)
            Picture = Iterate(Picture);

        Console.WriteLine($"There are {Picture.Cast<bool>().Count(b => b)} pixels on after 5 iterations.");
    }

    public override void Part2()
    {
        base.Part2();

        for (; Iter < 18; Iter++)
            Picture = Iterate(Picture);

        Console.WriteLine($"There are {Picture.Cast<bool>().Count(b => b)} pixels on after 18 iterations.");
    }

    private static string ToString(bool[,] arr)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
                sb.Append(arr[i, j] ? '#' : '.');
            sb.Append('/');
        }
        sb.Length--;

        return sb.ToString();
    }

    private static bool[,] ToArray(string str)
    {
        int size = str.IndexOf('/');

        bool[,] ret = new bool[size, size];
        for (int row = 0; row < size; row++)
            for (int col = 0; col < size; col++)
                ret[row, col] = str[row * (size + 1) + col] == '#';

        return ret;
    }

    private static bool[,] Rotate90(bool[,] arr)
    {
        int size = arr.GetLength(0);
        bool[,] ret = new bool[size, size];

        for (int row = 0; row < size; row++)
            for (int col = 0; col < size; col++)
                ret[col, size - row - 1] = arr[row, col];

        return ret;
    }

    private static bool[,] Flip(bool[,] arr)
    {
        int size = arr.GetLength(0);
        bool[,] ret = new bool[size, size];

        for (int row = 0; row < size; row++)
            for (int col = 0; col < size; col++)
                ret[size - row - 1, col] = arr[row, col];

        return ret;
    }

    private bool[,] Iterate(bool[,] arr)
    {
        int size = arr.GetLength(0);
        int segment = size % 2 == 0 ? 2 : 3;
        int newSegment = segment + 1;
        int skip = size / segment;
        int newSize = skip * (segment + 1);
        bool[,] ret = new bool[newSize, newSize];

        for (int row = 0; row < skip; row++)
        {
            for (int col = 0; col < skip; col++)
            {
                bool[,] part = new bool[segment, segment];
                for (int i = 0; i < segment; i++)
                    for (int j = 0; j < segment; j++)
                        part[i, j] = arr[row * segment + i, col * segment + j];

                bool[,] replacement = Rules[ToString(part)];
                for (int i = 0; i < newSegment; i++)
                    for (int j = 0; j < newSegment; j++)
                        ret[row * newSegment + i, col * newSegment + j] = replacement[i, j];
            }
        }

        return ret;
    }
}
