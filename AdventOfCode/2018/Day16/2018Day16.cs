using System.Text.RegularExpressions;

namespace AdventOfCode._2018.Day16;

public partial class _2018Day16 : _2018Day
{
    private readonly List<Observation> Observations = [];
    private readonly List<Command> Commands = [];

    [GeneratedRegex(@"\d+", RegexOptions.Compiled)]
    private static partial Regex ArrayRegex();

    public _2018Day16() : base("Day16")
    {
        int index;
        for (index = 0; index < Input.Length; index += 4)
        {
            if (string.IsNullOrEmpty(Input[index]))
                break;

            int[] before = [.. ArrayRegex().Matches(Input[index]).Select(m => int.Parse(m.Value))];
            var cmd = new Command([.. ArrayRegex().Matches(Input[index + 1]).Select(m => int.Parse(m.Value))]);
            int[] after = [.. ArrayRegex().Matches(Input[index + 2]).Select(m => int.Parse(m.Value))];

            Observations.Add(new(before, cmd, after));
        }

        while (index < Input.Length)
        {
            if (!string.IsNullOrEmpty(Input[index]))
                Commands.Add(new([.. ArrayRegex().Matches(Input[index]).Select(m => int.Parse(m.Value))]));

            index++;
        }
    }

    public override void Part1()
    {
        base.Part1();

        int count = 0;
        foreach (var observation in Observations)
            if (Interpreter.Test(observation) >= 3)
                count++;

        Console.WriteLine($"{count} samples behave like three or more operations.");
    }

    public override void Part2()
    {
        base.Part2();

        var i = new Interpreter(Observations);
        int[] reg = i.Run(Commands);

        Console.WriteLine($"After the execution {reg[0]} is the value in register 0.");
    }
}
