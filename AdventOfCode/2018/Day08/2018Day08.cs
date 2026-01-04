namespace AdventOfCode._2018.Day08;

internal class Node
{
    public Node? Parent { get; set; }
    public List<Node> Children { get; } = [];
    public List<int> MetaData { get; } = [];
    public int Value { get; set; }
}

public class _2018Day08 : _2018Day
{
    private readonly Node Root;

    public _2018Day08() : base("Day08")
    {
        var numbers = Input[0].Split(' ').Select(int.Parse).ToList();
        int i = 0;
        Root = Parse(numbers, ref i, null);
    }

    private static Node Parse(List<int> input, ref int from, Node? parent)
    {
        int childern = input[from++];
        int metaData = input[from++];

        var node = new Node { Parent = parent };

        for (int i = 0; i < childern; i++)
            node.Children.Add(Parse(input, ref from, node));

        for (int i = 0; i < metaData; i++)
            node.MetaData.Add(input[from++]);

        if (childern == 0)
        {
            node.Value = node.MetaData.Sum();
        }
        else
        {
            foreach (int md in node.MetaData)
            {
                if (md <= childern)
                    node.Value += node.Children[md - 1].Value;
            }
        }

        return node;
    }

    public override void Part1()
    {
        base.Part1();

        int sum = 0;
        SumMetaData(Root, ref sum);
        Console.WriteLine($"The sum of all metadata entries: {sum}");
    }

    private static void SumMetaData(Node from, ref int sum)
    {
        sum += from.MetaData.Sum();

        foreach (var child in from.Children)
            SumMetaData(child, ref sum);
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The root node's value is: {Root.Value}");
    }
}
