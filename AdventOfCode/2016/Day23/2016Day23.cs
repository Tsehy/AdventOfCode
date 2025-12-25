using AdventOfCode._2016.AssemBunny;

namespace AdventOfCode._2016.Day23;

public class _2016Day23 : _2016Day
{
    private readonly List<Command> Commands;

    public _2016Day23() : base("Day23")
    {
        Commands = CommandParser.Parse(Input);
    }

    public override void Part1()
    {
        base.Part1();

        var i = new Interpreter(Commands);
        i.SimplifyMultiplication();
        i.Registers["a"] = 7;
        i.ExecuteCommands();
        Console.WriteLine($"The password is: {i.Registers["a"]}");
    }

    public override void Part2()
    {
        base.Part2();

        var i = new Interpreter(Commands);
        i.SimplifyMultiplication();
        i.Registers["a"] = 12;
        i.ExecuteCommands();
        Console.WriteLine($"The password is: {i.Registers["a"]}");
    }
}
