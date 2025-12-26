namespace AdventOfCode._2017.Day01;

public  class _2017Day01 : _2017Day
{
    public _2017Day01() : base("Day01")
    {
    }

    private int CalculateChapta(int away = 1)
    {
        int sum = 0;
        for (int i = 0; i < Input[0].Length; i++)
        {
            int next = (i + away) % Input[0].Length;
            char curr = Input[0][i];
            if (curr == Input[0][next])
                sum += curr - '0';
        }
        return sum;
    }

    public override void Part1()
    {
        base.Part1();

        int sum = CalculateChapta();
        Console.WriteLine($"The chapta is: {sum}");
    }

    public override void Part2()
    {
        base.Part2();

        int away = Input[0].Length / 2;
        int sum = CalculateChapta(away);
        Console.WriteLine($"The chapta is: {sum}");
    }
}
