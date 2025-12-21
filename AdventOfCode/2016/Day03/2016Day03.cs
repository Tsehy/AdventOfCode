namespace AdventOfCode;

public class _2016Day03 : _2016Day
{
    private readonly List<int[]> Triangles;

    public _2016Day03(): base("Day03")
    {
        Triangles = [.. Input.Select(i => i.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray())];
    }

    private bool IsTriangle(int[] sides)
    {
        int sum = sides.Sum();
        return !sides.Any(s => s >= sum - s);
    }

    public override void Part1()
    {
        base.Part1();

        int validTriangles = Triangles.Count(IsTriangle);
        Console.WriteLine($"Number of valid triangles: {validTriangles}");
    }

    public override void Part2()
    {
        base.Part2();

        int i = 0, count = 0;
        while (i < Triangles.Count)
        {
            for (int j = 0; j  < 3; j++)
                if (IsTriangle([Triangles[i][j], Triangles[i + 1][j], Triangles[i + 2][j]]))
                    count++;
            i += 3;
        }
        Console.WriteLine($"Number of valid triangles: {count}");
    }
}
