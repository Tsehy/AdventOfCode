namespace AdventOfCode._2017.Day12;

public class _2017Day12 : _2017Day
{
    private readonly Dictionary<int, int[]> Programs = [];

    public _2017Day12() : base("Day12")
    {
        foreach (string line in Input)
        {
            string[] parts = line.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
            int key = int.Parse(parts[0]);
            Programs[key] = [.. parts[2..].Select(int.Parse)];
        }
    }

    public override void Part1()
    {
        base.Part1();

        HashSet<int> visited = [0];
        var open = new Queue<int>();
        open.Enqueue(0);

        while (open.Count > 0)
        {
            int current = open.Dequeue();
            foreach (int neighbour in Programs[current])
                if (visited.Add(neighbour))
                    open.Enqueue(neighbour);
        }

        Console.WriteLine($"{visited.Count} programs are connected to #0.");
    }

    public override void Part2()
    {
        base.Part2();

        int groups = 0;
        var open = Programs.Keys.ToList();

        while (open.Count > 0)
        {
            groups++;
            var remove = new Queue<int>();
            remove.Enqueue(open[0]);
            open.RemoveAt(0);

            while (remove.Count > 0)
            {
                int current = remove.Dequeue();
                foreach (int neighbour in Programs[current])
                    if (open.Remove(neighbour))
                        remove.Enqueue(neighbour);
            }
        }

        Console.WriteLine($"There are {groups} groups.");
    }
}
