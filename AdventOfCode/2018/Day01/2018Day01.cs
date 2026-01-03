namespace AdventOfCode._2018.Day01;

public class _2018Day01 : _2018Day
{
    private readonly int[] Changes;

    public _2018Day01() : base("Day01")
    {
        Changes = [.. Input.Select(int.Parse)];
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine(Changes.Sum());
    }

    public override void Part2()
    {
        base.Part2();

        int current = 0, index = 0;
        var visited = new HashSet<int> { current };
        do
        {
            current += Changes[index];
            index = (index + 1) % Changes.Length;
        }
        while (visited.Add(current));

        Console.WriteLine(current);
    }
}
