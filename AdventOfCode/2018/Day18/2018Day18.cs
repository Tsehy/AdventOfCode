namespace AdventOfCode._2018.Day18;

public class _2018Day18 : _2018Day
{
    private readonly char[,] Grid;

    public _2018Day18() : base("Day18")
    {
        int rows = Input.Length;
        int cols = Input[0].Length;
        Grid = new char[cols, rows];

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                Grid[x, y] = Input[y][x];
    }

    private static char[,] Iterate(char[,] grid)
    {
        int cols = grid.GetLength(0);
        int rows = grid.GetLength(1);
        char[,] result = new char[cols, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                int trees = 0, lumber = 0;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int xCheck = x + dx;
                        int yCheck = y + dy;
                        if ((dx == 0 && dy == 0) || xCheck < 0 || xCheck >= cols || yCheck < 0 || yCheck >= rows)
                            continue;

                        switch (grid[xCheck, yCheck])
                        {
                            case '|': trees++; break;
                            case '#': lumber++; break;
                        }

                        result[x, y] = grid[x, y] switch
                        {
                            '.' => trees >= 3 ? '|' : '.',
                            '|' => lumber >= 3 ? '#' : '|',
                            '#' => lumber >= 1 && trees >= 1 ? '#' : '.',
                            _ => grid[x, y]
                        };
                    }
                }
            }
        }

        return result;
    }

    private static int Iterate(char[,] grid, int n)
    {
        char[,] res = grid;
        string representation = string.Join("", res.Cast<char>());
        int value = representation.Count(c => c == '|') * representation.Count(c => c == '#');
        Dictionary<string, int> dict = new() { { representation, value } };
        List<string> order = [representation];

        int iter = 0;
        while (iter < n)
        {
            res = Iterate(res);
            representation = string.Join("", res.Cast<char>());
            value = representation.Count(c => c == '|') * representation.Count(c => c == '#');
            if (dict.ContainsKey(representation))
                break;

            iter++;
            dict[representation] = value;
            order.Add(representation);
        }

        if (iter == n)
        {
            return value;
        }
        else
        {
            int start = order.IndexOf(representation);
            int cycleLength = iter - start + 1;
            int rem = (n - start) % cycleLength + start;
            return dict[order[rem]];
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"Resource value of the lumber collection is {Iterate(Grid, 10)} after 10 minutes");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"Resource value of the lumber collection is {Iterate(Grid, 1_000_000_000)} after 1000000000 minutes");
    }
}
