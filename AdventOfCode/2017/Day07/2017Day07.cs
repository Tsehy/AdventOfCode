namespace AdventOfCode._2017.Day07;

public class _2017Day07 : _2017Day
{
    private readonly Node Root;

    public _2017Day07() : base("Day07")
    {
        Dictionary<string, Node> nodes = [];

        foreach (string i in Input)
        {
            string[] parts = i.Split([' ', '(', ')', ','], StringSplitOptions.RemoveEmptyEntries);
            List<string> children = [.. parts.Where((p, i) => i > 2)];
            nodes[parts[0]] = new Node(parts[0], int.Parse(parts[1]), children);
        }

        foreach (var node in nodes.Values)
        {
            foreach (string name in node.ChildrenNames)
            {
                node.Children.Add(nodes[name]);
                nodes[name].Partent = node;
            }
        }

        foreach (var node in nodes.Values)
        {
            if (node.Partent == null)
            {
                Root = node;
                Root.CalculateWeights();
                break;
            }
        }

        if (Root == null) throw new Exception("Graph doesn't have a root!");
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The root is: {Root.Name}");
    }

    private (int, string) CalculateCorrectWeight()
    {
        var current = Root;
        while (!current.IsBalanced())
        {
            current = current.GetImperfect();
            if (current == null)
                break;
        }

        if (current == null || current.Partent == null)
            return (-1, string.Empty);

        int expectedWeight = current.Partent.Children.GroupBy(c => c.CumulativeWeight).MaxBy(g => g.Count())!.Key ?? 0;
        return (expectedWeight - (current.Children.Sum(c => c.CumulativeWeight) ?? 0), current.Name);
    }

    public override void Part2()
    {
        base.Part2();

        (int correctWeight, string name) = CalculateCorrectWeight();
        Console.WriteLine($"{name}'s weight should be {correctWeight}");
    }
}
