namespace AdventOfCode._2017.Day17;

public class _2017Day17 : _2017Day
{
    private readonly int Steps;

    public _2017Day17() : base("Day17")
    {
        Steps = int.Parse(Input[0]);
    }

    public override void Part1()
    {
        base.Part1();

        int pos = 0;
        List<int> Numbers = [0];
        for (int val = 1; val <= 2017; val++)
        {
            pos = (pos + Steps) % Numbers.Count + 1;
            Numbers.Insert(pos, val);
        }

        pos = (pos + 1) % Numbers.Count;
        Console.WriteLine($"Value {Numbers[pos]} is after 2017.");
    }

    public override void Part2()
    {
        base.Part2();

        int pos = 0, len = 1;
        int afterZero = -1;
        for (int val = 1; val <= 50_000_000; val++)
        {
            pos = (pos + Steps) % len + 1;
            len++;

            if (pos == 1)
                afterZero = val;
        }

        Console.WriteLine($"Value {afterZero} is after zero.");
    }
}
