using AdventOfCode._2016.Day10;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class _2016Day10 : _2016Day
{
    private readonly Dictionary<string, Bot> Bots = [];
    private readonly string PartOneSolution = string.Empty;

    [GeneratedRegex(@"(bot|output) \d+", RegexOptions.Compiled)]
    private static partial Regex BotName();

    [GeneratedRegex(@"value (\d+)")]
    private static partial Regex Value();

    public _2016Day10() : base("Day10")
    {
        foreach (string instruction in Input.Where(i => !i.Contains("value")))
        {
            var matches = BotName().Matches(instruction);
            Bots.Add(matches[0].Value, new(matches[1].Value, matches[2].Value));
            if (matches[1].Value.StartsWith('o') && !Bots.ContainsKey(matches[1].Value))
                Bots.Add(matches[1].Value, new(string.Empty, string.Empty));
            if (matches[2].Value.StartsWith('o') && !Bots.ContainsKey(matches[2].Value))
                Bots.Add(matches[2].Value, new(string.Empty, string.Empty));
        }

        foreach (string instruction in Input.Where(i => i.Contains("value")))
        {
            string bot = BotName().Match(instruction).Value;
            Bots[bot].Values.Add(int.Parse(Value().Match(instruction).Groups[1].Value));
        }

        while (Bots.Any(v => v.Key.StartsWith('b') && v.Value.Values.Count == 2))
        {
            var vp = Bots.First(v => v.Key.StartsWith('b') && v.Value.Values.Count == 2);
            var bot = vp.Value;
            int min = bot.Values.Min();
            int max = bot.Values.Max();

            if (min == 17 && max == 61)
                PartOneSolution = vp.Key;

            Bots[bot.Lowest].Values.Add(min);
            Bots[bot.Highest].Values.Add(max);
            bot.Values = [];
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"{PartOneSolution} is comparing 17 and 61\n");
    }

    public override void Part2()
    {
        base.Part2();

        int productSum = Bots.Where(vp => vp.Key is "output 0" or "output 1" or "output 2").Select(vp => vp.Value.Values.Sum()).Aggregate((i, p) => i * p);
        Console.WriteLine($"Product sum of output 0, 1 and 2 is: {productSum}\n");
    }
}
