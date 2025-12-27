namespace AdventOfCode._2017.Day04;

public class _2017Day04 : _2017Day
{
    private readonly List<string[]> PassPhrases;

    public _2017Day04() : base("Day04")
    {
        PassPhrases = [.. Input.Select(i => i.Split(' '))];
    }

    public override void Part1()
    {
        base.Part1();

        int count = 0;
        foreach (string[] phrase in PassPhrases)
        {
            var set = phrase.ToHashSet();
            if (set.Count == phrase.Length)
                count++;
        }

        Console.WriteLine($"{count} phrases are valid.");
    }

    public override void Part2()
    {
        base.Part2();

        int count = 0;
        foreach (string[] phrase in PassPhrases)
        {
            var set = phrase.Select(p => string.Join("", p.Order())).ToHashSet();
            if (set.Count == phrase.Length)
                count++;
        }

        Console.WriteLine($"{count} phrases are valid.");
    }
}
