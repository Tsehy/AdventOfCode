namespace AdventOfCode._2017.Day18;

public class _2017Day18 : _2017Day
{
    private readonly List<Command> Commands = [];

    public _2017Day18() : base("Day18")
    {
        foreach (string input in Input)
        {
            string[] parts = input.Split(' ');
            var type = (CommandType)Enum.Parse(typeof(CommandType), parts[0]);
            if (parts.Length == 3)
                Commands.Add(new(type, parts[1], parts[2]));
            else
                Commands.Add(new(type, parts[1], "0"));
        }
    }

    public override void Part1()
    {
        base.Part1();

        var i = new Interpreter(Commands);
        do
            i.Step();
        while (!i.IsBlocked);

        Console.WriteLine($"The recovered sound is: {i.Outgoing.Last()}.");
    }

    public override void Part2()
    {
        base.Part2();

        var i0 = new Interpreter(Commands, 0);
        var i1 = new Interpreter(Commands, 1);

        i0.Incoming = i1.Outgoing;
        i1.Incoming = i0.Outgoing;

        bool progress;
        do
        {
            progress = false;
            progress |= i0.Step();
            progress |= i1.Step();
        }
        while (progress);

        Console.WriteLine($"Program 1 sent {i1.SendCount} values.");
    }
}
