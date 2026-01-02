namespace AdventOfCode._2017.Day24;

public readonly struct Cable(ulong id, int start, int end)
{
    public ulong Id { get; } = id;
    public int Start { get; } = start;
    public int End { get; } = end;
    public int Strength { get; } = start + end;
}

public class Bridge
{
    public ulong CablesMask { get; set; }
    public int Port { get; set; }
    public int Strength { get; set; }
    public int Length { get; set; }

    public Bridge(Cable c)
    {
        CablesMask = c.Id;
        Length = 1;
        Strength = c.Strength;
        Port = Math.Max(c.Start, c.End);
    }

    private Bridge(ulong mask, int currentPort, int strength, int length)
    {
        CablesMask = mask;
        Port = currentPort;
        Strength = strength;
        Length = length;
    }

    public Bridge Add(Cable c)
    {
        ulong newCables = CablesMask | c.Id;
        int newPort = Port != c.Start ? c.Start : c.End;
        return new(newCables, newPort, Strength + c.Strength, Length + 1);
    }

    public bool Contains(Cable cable) => (CablesMask & cable.Id) > 0;
}

public class _2017Day24 : _2017Day
{
    private readonly Dictionary<int, List<Cable>> CablesByPort = [];

    public _2017Day24() : base("Day24")
    {
        ulong nextId = 1;
        foreach (string line in Input)
        {
            string[] parts = line.Split('/');
            var cable = new Cable(nextId, int.Parse(parts[0]), int.Parse(parts[1]));
            nextId <<= 1;

            if (!CablesByPort.TryGetValue(cable.Start, out var startList))
                CablesByPort[cable.Start] = startList = [];
            if (!CablesByPort.TryGetValue(cable.End, out var endList))
                CablesByPort[cable.End] = endList = [];

            startList.Add(cable);
            if (cable.Start != cable.End)
                endList.Add(cable);
        }
    }

    public override void Part1()
    {
        base.Part1();

        int maxStrength = int.MinValue;
        var open = new Queue<Bridge>();
        foreach (var cable in CablesByPort[0])
            open.Enqueue(new Bridge(cable));

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            var next = CablesByPort[current.Port].Where(c => !current.Contains(c));

            if (!next.Any())
            {
                if (current.Strength > maxStrength)
                    maxStrength = current.Strength;
                continue;
            }

            foreach (var cable in next)
                open.Enqueue(current.Add(cable));
        }

        Console.WriteLine($"The hightest bridge strength is: {maxStrength}");
    }

    public override void Part2()
    {
        base.Part2();

        int maxStrength = int.MinValue;
        int maxLength = 0;
        var open = new Queue<Bridge>();
        foreach (var cable in CablesByPort[0])
            open.Enqueue(new Bridge(cable));

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            var next = CablesByPort[current.Port].Where(c => !current.Contains(c));

            if (!next.Any())
            {
                if (maxLength < current.Length)
                {
                    maxLength = current.Length;
                    maxStrength = current.Strength;
                }
                else if (maxLength == current.Length && maxStrength < current.Strength)
                {
                    maxStrength = current.Strength;
                }
                continue;
            }

            foreach (var cable in next)
                open.Enqueue(current.Add(cable));
        }

        Console.WriteLine($"The longest bridge's strength is: {maxStrength}");
    }
}
