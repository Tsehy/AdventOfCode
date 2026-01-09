using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2018.Day15;

internal class CombatUnit(bool isElf, Node pos)
{
    public bool IsElf { get; } = isElf;
    public Node Position { get; set; } = pos;
    public int Health { get; set; } = 200;

    public bool OpponentInRange([NotNullWhen(true)] out CombatUnit? opponent)
    {
        opponent = Position.Neighbours.Where(p => p.CurrentUnit != null && p.CurrentUnit.IsElf != IsElf).OrderBy(p => p.CurrentUnit!.Health).Select(p => p.CurrentUnit).FirstOrDefault();
        return opponent != null;
    }
}
