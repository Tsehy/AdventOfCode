namespace AdventOfCode._2016.Day11;

internal readonly record struct Component(string Name, bool IsGenerator)
{
}

internal class State(List<List<Component>> floors, int elevator, int moves = 0)
{
    public List<List<Component>> Floors { get; set; } = floors;
    public int Elevator { get; set; } = elevator;
    public int Moves { get; set; } = moves;
    public int Away => Floors.Select((f, i) => f.Count * (Floors.Count - 1 - i)).Sum();
    public string UniqueKey
    {
        get
        {
            var map = new Dictionary<string, (int micro, int gen)>();
            for (int floor = 0; floor < Floors.Count; floor++)
            {
                foreach (var c in Floors[floor])
                {
                    if (!map.TryGetValue(c.Name, out var pair))
                        pair = (-1, -11);

                    if (c.IsGenerator)
                        pair.gen = floor;
                    else
                        pair.micro = floor;

                    map[c.Name] = pair;
                }
            }

            string[] pairs = map.Values.Select(p => $"{p.micro}:{p.gen}").OrderBy(p => p, StringComparer.Ordinal).ToArray();
            return Elevator + "|" + string.Join(",", pairs);
        }
    }

    public int Heuristic()
    {
        int top = Floors.Count - 1;
        int totalDistance = 0;
        for (int i = 0; i < Floors.Count; i++)
            foreach (var c in Floors[i])
                totalDistance += top - i;

        return (totalDistance + 1) / 2;
    }

    public bool IsValid()
    {
        foreach (var floor in Floors)
        {
            var microchips = floor.Where(c => !c.IsGenerator).Select(c => c.Name).ToHashSet();
            var generators = floor.Where(c => c.IsGenerator).Select(c => c.Name).ToHashSet();

            if (generators.Count > 0 && microchips.Any(m => !generators.Contains(m)))
                return false;
        }

        return true;
    }

    public IEnumerable<State> NextValidStates()
    {
        var items = Floors[Elevator];
        int n = items.Count;

        foreach (int direction in new int[] { -1, 1 })
        {
            int newElevator = Elevator + direction;
            if (newElevator < 0 || newElevator >= Floors.Count)
                continue;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var deepCopy = Floors.Select(x => new List<Component>(x)).ToList();
                    deepCopy[Elevator].Remove(items[i]);
                    deepCopy[Elevator].Remove(items[j]);
                    deepCopy[newElevator].Add(items[i]);
                    deepCopy[newElevator].Add(items[j]);
                    var next = new State(deepCopy, newElevator, Moves + 1);
                    if (next.IsValid())
                        yield return next;
                }
            }

            for (int i = 0; i < n; i++)
            {
                var deepCopy = Floors.Select(x => new List<Component>(x)).ToList();
                deepCopy[Elevator].Remove(items[i]);
                deepCopy[newElevator].Add(items[i]);
                var next = new State(deepCopy, newElevator, Moves + 1);
                if (next.IsValid())
                    yield return next;
            }
        }
    }
}