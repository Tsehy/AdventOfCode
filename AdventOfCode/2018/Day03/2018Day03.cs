namespace AdventOfCode._2018.Day03;

public readonly struct Claim(string id, int left, int top, int width, int height)
{
    public string Id { get; } = id;
    public int Width { get; } = width;
    public int Height { get; } = height;
    public int Left { get; } = left;
    public int Right { get; } = left + width - 1;
    public int Top { get; } = top;
    public int Bottom { get; } = top + height - 1;

    public bool Overlap(Claim other) => !(other.Right < Left || Right < other.Left || other.Bottom < Top || Bottom < other.Top);
}

public class _2018Day03 : _2018Day
{
    public readonly int[,] Fabric = new int[1_000, 1_000];
    public readonly List<Claim> Claims = [];

    public _2018Day03() : base("Day03")
    {
        foreach (string claim in Input)
        {
            string[] parts = claim.Split([' ', ',', 'x', ':']);

            int left = int.Parse(parts[2]);
            int top = int.Parse(parts[3]);
            int width = int.Parse(parts[5]);
            int height = int.Parse(parts[6]);

            Claim newClaim = new(parts[0], left, top, width, height);
            Claims.Add(newClaim);

            for (int x = newClaim.Left; x <= newClaim.Right; x++)
                for (int y = newClaim.Top; y <= newClaim.Bottom; y++)
                    Fabric[x, y]++;
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"{Fabric.Cast<int>().Count(v => v > 1)} square inch of fabric are within two or more claims.");
    }

    public override void Part2()
    {
        base.Part2();

        bool overlap;
        string id = string.Empty;
        foreach (var claim in Claims)
        {
            overlap = false;
            foreach (var other in Claims)
            {
                if (other.Id == claim.Id)
                    continue;

                overlap = claim.Overlap(other);
                if (overlap)
                    break;
            }

            if (!overlap)
            {
                id = claim.Id;
                break;
            }
        }

        Console.WriteLine($"Claim {id} is the only one that doesn't overlap.");
    }
}
