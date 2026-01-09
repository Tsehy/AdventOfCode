namespace AdventOfCode._2018.Day15;

public class _2018Day15 : _2018Day
{
    private readonly Dictionary<(int x, int y), Node> Grid = [];

    public _2018Day15() : base("Day15")
    {
        List<CombatUnit> units = [];

        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[0].Length; x++)
            {
                char current = Input[y][x];
                if (current == '#')
                    continue;

                var newNode = new Node(x, y);
                Grid[(x, y)] = newNode;

                if (current != '.')
                {
                    var newUnit = new CombatUnit(current == 'E', newNode);
                    newNode.CurrentUnit = newUnit;
                    units.Add(newUnit);
                }
            }
        }

        ConnectNodes(Grid);
    }
    
    public override void Part1()
    {
        base.Part1();

        SimulateCombat(3, out int outcome);
        Console.WriteLine($"The battle's outcome is: {outcome}");
    }

    public override void Part2()
    {
        base.Part2();

        int outcome;
        int elfStrength = 4;
        while (!SimulateCombat(elfStrength, out outcome, runUntilElfDies: true))
            elfStrength++;

        Console.WriteLine($"If the elves attack power is {elfStrength} the the battle's outcome is: {outcome}");
    }

    #region Setup methods
    private static void ConnectNodes(Dictionary<(int x, int y), Node> grid)
    {
        foreach (var node in grid.Values)
        {
            int x = node.X, y = node.Y;

            // add in order to avoid later ordering
            if (grid.TryGetValue((x, y - 1), out var topNeighbour))
                node.Neighbours.Add(topNeighbour);

            for (int dx = -1; dx <= 1; dx += 2)
                if (grid.TryGetValue((x + dx, y), out var neighbour))
                    node.Neighbours.Add(neighbour);

            if (grid.TryGetValue((x, y + 1), out var bottomNeighbour))
                node.Neighbours.Add(bottomNeighbour);
        }
    }

    private static List<CombatUnit> DeepCopy(Dictionary<(int x, int y), Node> grid)
    {
        var deepCopy = new Dictionary<(int x, int y), Node>();

        foreach (var vp in grid)
            deepCopy[vp.Key] = vp.Value.DeepCopy();

        ConnectNodes(deepCopy);

        return [.. deepCopy.Select(vp => vp.Value.CurrentUnit).Where(u => u != null).Cast<CombatUnit>()];
    }
    #endregion

    #region SimulateCombat
    private bool SimulateCombat(int elfStrength, out int outcome, bool runUntilElfDies = false)
    {
        var units = DeepCopy(Grid);

        outcome = 0;
        int turns = 0;
        bool combatEnded = false;
        while (true)
        {
            foreach (var currentUnit in units)
            {
                if (currentUnit.Health <= 0)
                    continue;

                if (currentUnit.Position.Neighbours.All(p => p.CurrentUnit == null || p.CurrentUnit.IsElf == currentUnit.IsElf)) // only move if there's no enemy in range
                {
                    var nodesInRangeOfOpponents = units
                        .Where(u => u.IsElf != currentUnit.IsElf && u.Health > 0)
                        .SelectMany(u => u.Position.Neighbours)
                        .ToHashSet();

                    if (nodesInRangeOfOpponents.Count == 0) // there's no opponent -> the battle ends
                    {
                        combatEnded = true;
                        break;
                    }

                    Node? targetDestination = null;
                    int minDistance = int.MaxValue;
                    foreach (var n in nodesInRangeOfOpponents)
                    {
                        if (n.CurrentUnit != null)
                            continue;

                        int dist = currentUnit.Position.Distance(n);
                        if (dist < minDistance && dist != -1)
                        {
                            targetDestination = n;
                            minDistance = dist;
                        }
                    }

                    if (targetDestination == null) // cannot reach eny enemy -> skip turn
                        continue;

                    Node? nextPosition = null;
                    minDistance = int.MaxValue;
                    foreach (var n in currentUnit.Position.Neighbours)
                    {
                        if (n.CurrentUnit != null)
                            continue;

                        int dist = n.Distance(targetDestination);
                        if (dist < minDistance && dist != -1)
                        {
                            nextPosition = n;
                            minDistance = dist;
                        }
                    }

                    if (nextPosition != null) // prevent compiler warnings
                    {
                        currentUnit.Position.CurrentUnit = null;
                        currentUnit.Position = nextPosition;
                        nextPosition.CurrentUnit = currentUnit;
                    }
                }

                if (currentUnit.OpponentInRange(out var opponent))
                {
                    opponent.Health -= currentUnit.IsElf ? elfStrength : 3;
                    if (opponent.Health <= 0)
                    {
                        opponent.Position.CurrentUnit = null; // remove from map
                        if (runUntilElfDies && opponent.IsElf)
                            return false;
                    }
                }
            }

            units = [.. units.Where(u => u.Health > 0).OrderBy(u => u.Position.Y).ThenBy(u => u.Position.X)];
            if (combatEnded)
                break;

            turns++;
        }

        int sumHp = units.Sum(u => u.Health);
        outcome = sumHp * turns;

        return true;
    }
    #endregion
}
