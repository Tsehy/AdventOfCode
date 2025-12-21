using AdventOfCode._2016.Day11;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class _2016Day11 : _2016Day
{
    private readonly State Start;

    [GeneratedRegex(@"[a-z-]+(?=-compatible microchip)", RegexOptions.Compiled)]
    private static partial Regex MicrochipRegex();

    [GeneratedRegex(@"[a-z-]+(?= generator)", RegexOptions.Compiled)]
    private static partial Regex GeneratorRegex();

    public _2016Day11() : base("Day11")
    {
        var floors = new List<List<Component>>();
        foreach (string line in Input)
        {
            var floor = new List<Component>();
            floor.AddRange(GeneratorRegex().Matches(line).Select(m => new Component(m.Value, true)));
            floor.AddRange(MicrochipRegex().Matches(line).Select(m => new Component(m.Value, false)));
            floors.Add(floor);
        }
        Start = new State(floors, 0);
    }

    private static int MinMove(State start)
    {
        var open = new PriorityQueue<State, int>();
        var best = new Dictionary<string, int>();

        open.Enqueue(start, 0);
        best[start.UniqueKey] = start.Moves;

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current.Away == 0)
                return current.Moves;

            foreach (var nextState in current.NextValidStates())
            {
                string key = nextState.UniqueKey;
                if (!best.TryGetValue(key, out int prevMoves) || nextState.Moves < prevMoves)
                {
                    best[key] = nextState.Moves;
                    open.Enqueue(nextState, nextState.Moves + nextState.Heuristic());
                }
            }
        }

        return -1;
    }

    public override void Part1()
    {
        base.Part1();

        int min = MinMove(Start);
        Console.WriteLine($"The minimum number of moves is: {min}");
    }

    public override void Part2()
    {
        base.Part2();
        Start.Floors[0].Add(new Component("elerium", true));
        Start.Floors[0].Add(new Component("elerium", false));
        Start.Floors[0].Add(new Component("dilithium", true));
        Start.Floors[0].Add(new Component("dilithium", false));

        int min = MinMove(Start);
        Console.WriteLine($"The minimum number of moves is: {min}");
    }
}
