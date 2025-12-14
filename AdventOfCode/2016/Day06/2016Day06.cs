namespace AdventOfCode;

public class _2016Day06 : _2016Day
{
    public readonly List<List<char>> Transposed;

    public _2016Day06() : base("Day06")
    {
        Transposed = [];
        for (int j = 0; j < Input[0].Length; j++)
            Transposed.Add([]);

        for (int i = 0; i < Input.Length; i++)
            for (int j = 0; j < Input[0].Length; j++)
                Transposed[j].Add(Input[i][j]);
    }

    public override void Part1()
    {
        base.Part1();

        string message = string.Join("", Transposed.Select(cg => cg.GroupBy(c => c).MaxBy(c => c.Count())!.First()));
        Console.WriteLine($"The most common letters: {message}\n");
    }

    public override void Part2()
    {
        base.Part2();

        string message = string.Join("", Transposed.Select(cg => cg.GroupBy(c => c).MinBy(c => c.Count())!.First()));
        Console.WriteLine($"The least common letters: {message}\n");
    }
}
