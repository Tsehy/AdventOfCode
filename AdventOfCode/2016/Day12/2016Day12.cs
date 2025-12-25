using AdventOfCode._2016.AssemBunny;

namespace AdventOfCode._2016.Day12;

public class _2016Day12 : _2016Day
{
    private readonly List<Command> Commands;

    public _2016Day12() : base("Day12")
    {
        Commands = CommandParser.Parse(Input);
    }

    public override void Part1()
    {
        base.Part1();

        var i = new Interpreter(Commands);
        i.ExecuteCommands();
        Console.WriteLine($"Value in register a: {i.Registers["a"]}");
    }

    public override void Part2()
    {
        base.Part2();

        var i = new Interpreter(Commands);
        i.Registers["c"] = 1;
        i.ExecuteCommands();
        Console.WriteLine($"Value in register a: {i.Registers["a"]}");
    }
}
