namespace AdventOfCode._2017.Day10;

internal class CircularNode(int value, CircularNode next, CircularNode prev)
{
    public int Value { get; set; } = value;
    public CircularNode Next { get; set; } = next;
    public CircularNode Previous { get; set; } = prev;
}

internal class CircularList
{
    private readonly Dictionary<int, CircularNode> Nodes = [];

    public CircularNode First => Nodes[0];
    public CircularNode Last => Nodes[Nodes.Count - 1];
    public int Count => Nodes.Count;

    public CircularList()
    {
    }

    public CircularList(int count) : base()
    {
        if (count <= 0)
            return;

        AddFirst(0);

        for (int i = 1; i < count; i++)
            Add(i);
    }

    public int this[int index] { get => Nodes[index].Value; set => Nodes[index].Value = value; }

    public void Add(int item)
    {
        int newIndex = Nodes.Count;
        var first = First;
        var last = Last;
        var newNode = new CircularNode(item, first, last);
        Nodes[newIndex] = newNode;
        first.Previous = last.Next = newNode;
    }

    public void AddFirst(int value)
    {
        if (Nodes.Count > 0)
            throw new InvalidOperationException("List is not empty!");

        Nodes[0] = new CircularNode(value, null!, null!);
        Nodes[0].Next = Nodes[0].Previous = Nodes[0];
    }

    public void Twist(int index, int length)
    {
        int startIndex = index % Nodes.Count;
        int endIndex = (startIndex + length - 1) % Nodes.Count;

        var startNode = Nodes[startIndex];
        var endNode = Nodes[endIndex];

        var beforeNode = startNode.Previous;
        var afterNode = endNode.Next;

        beforeNode.Next = endNode;
        endNode.Next = beforeNode;

        afterNode.Previous = startNode;
        startNode.Previous = afterNode;

        int reIndex = startIndex;
        var current = endNode;
        for (int i = 0; i < length; i++)
        {
            (current.Previous, current.Next) = (current.Next, current.Previous);
            Nodes[reIndex] = current;

            reIndex = (reIndex + 1) % Nodes.Count;
            current = current.Next;
        }
    }

    public List<int> ToList() => [.. Nodes.Values.Select(i => i.Value)];
}