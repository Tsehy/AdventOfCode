namespace AdventOfCode._2018.Day17;

public class _2018Day17 : _2018Day
{
    private readonly Dictionary<(int x, int y), char> Grid = [];
    private readonly int MinY;
    private readonly int MaxY;

    public _2018Day17() : base("Day17")
    {
        foreach (string line in Input)
        {
            string[] parts = line.Split(", ");

            char dir = parts[0][0];
            int axis1 = int.Parse(parts[0][2..]);
            int[] axis2 = [.. parts[1][2..].Split("..").Select(int.Parse)];

            for (int i = axis2[0]; i <= axis2[1]; i++)
                if (dir == 'x')
                    Grid[(axis1, i)] = '#';
                else
                    Grid[(i, axis1)] = '#';
        }

        MinY = Grid.Keys.Min(p => p.y);
        MaxY = Grid.Keys.Max(p => p.y);
        FloodDown((500, 0));
    }

    private void FloodDown((int x, int y) source)
    {
        (int x, int y) = source;

        if (y > MaxY)
            return;

        (int x, int y) down = (x, y + 1);
        if (!Grid.TryGetValue(down, out char downValue))
        {
            Grid[down] = '|';
            FloodDown(down);
            downValue = Grid[down];
        }

        if (downValue is '#' or '~')
        {
            bool supported = true;
            supported &= Spread(source, -1, out var left);
            supported &= Spread(source, 1, out var right);

            if (supported)
                for (int sx = left.x + 1; sx < right.x; sx++)
                    Grid[(sx, y)] = '~';
        }
    }

    private bool Spread((int x, int y) source, int direction, out (int x, int y) flood)
    {
        (int x, int y) = source;

        (int x, int y) bellow;
        for (int offset = direction; ; offset += direction)
        {
            flood = (x + offset, y);
            bellow = (x + offset, y + 1);

            if (Grid.TryGetValue(flood, out char c) && c == '#')
                break;

            if (!Grid.ContainsKey(flood))
                Grid[flood] = '|';

            if (!Grid.TryGetValue(bellow, out char bellowValue))
            {
                FloodDown(flood);
                return false;
            }
            
            if (bellowValue is '|')
                return false;
        }

        return true;
    }

    public override void Part1()
    {
        base.Part1();

        int reachable = Grid.Where(vp => vp.Key.y <= MaxY && vp.Key.y >= MinY).Count(vp => vp.Value != '#');
        Console.WriteLine($"The water can reach {reachable} tiles.");
    }

    public override void Part2()
    {
        base.Part2();

        int retained = Grid.Where(vp => vp.Key.y <= MaxY && vp.Key.y >= MinY).Count(vp => vp.Value == '~');
        Console.WriteLine($"{retained} water will be retained.");
    }
}
