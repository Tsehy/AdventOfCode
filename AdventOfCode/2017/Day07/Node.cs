namespace AdventOfCode._2017.Day07;

internal class Node(string name, int weight, List<string> childrenNames)
{
    public string Name { get; set; } = name;
    public int Weight { get; set; } = weight;
    public List<string> ChildrenNames { get; set; } = childrenNames;
    public Node? Partent { get; set; } = null;
    public List<Node> Children { get; set; } = [];
    public int? CumulativeWeight { get; set; } = null;

    public bool IsBalanced()
    {
        if (CumulativeWeight == null)
            return false;

        return Children.GroupBy(c => c.CumulativeWeight).Count() == 1;
    }

    public Node? GetImperfect()
    {
        if (IsBalanced() || CumulativeWeight == null)
            return null;

        return Children.GroupBy(c => c.CumulativeWeight).MinBy(g => g.Count())!.FirstOrDefault();
    }

    internal void CalculateWeights()
    {
        if (Children.Count == 0)
        {
            CumulativeWeight = Weight;
            return;
        }
        else
        {
            foreach (var child in Children)
                child.CalculateWeights();
            CumulativeWeight = Weight + Children.Sum(c => c.CumulativeWeight);
        }
    }
}
