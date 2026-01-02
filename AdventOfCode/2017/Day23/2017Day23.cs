namespace AdventOfCode._2017.Day23;

public class _2017Day23 : _2017Day
{
    private readonly List<Command> Commands = [];

    public _2017Day23() : base("Day23")
    {
        foreach (string line in Input)
        {
            string[] parts = line.Split(' ');
            Commands.Add(new(parts[0], parts[1], parts[2]));
        }
    }

    public override void Part1()
    {
        base.Part1();

        var i = new Interpreter(Commands, true);
        i.Process();

        Console.WriteLine($"The mult command was invoked {i.MulCount} times.");
    }

    public override void Part2()
    {
        base.Part2();

        var i = new Interpreter(Commands, false);
        i.Process(onlySetup: true);

        int b = (int)i.Reg[1];
        int c = (int)i.Reg[2];
        int h = 0;
        do
        {
            for (int d = 2; d < b; d++)
            {
                if (b % d == 0)
                {
                    h++;
                    break;
                }
            }
            b += 17;
        }
        while (b <= c);

        Console.WriteLine($"The value in register h is: {h}");
    }
}
