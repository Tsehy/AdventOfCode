using AdventOfCode._2016.AssemBunny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day25;

public class _2016Day25 : _2016Day
{
    private readonly List<Command> Commands;

    public _2016Day25() : base("Day25")
    {
        Commands = CommandParser.Parse(Input);
    }

    public override void Part1()
    {
        base.Part1();

        var i = new Interpreter(Commands);
        i.SimplifyMultiplication();
        int index = 0;
        while (true)
        {
            i.Registers.Clear();
            i.Registers["a"] = index;
            i.ExecuteCommands();

            if (i.IsClockSignal)
                break;
            else
                index++;
        }

        Console.WriteLine($"{index} produces clock signal.");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine("I'll take a bonus star any time *");
    }
}
