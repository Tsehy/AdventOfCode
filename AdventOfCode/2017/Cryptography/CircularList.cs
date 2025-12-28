namespace AdventOfCode._2017.Cryptography;

internal class CircularNode(byte value, CircularNode next)
{
    public byte Value { get; set; } = value;
    public CircularNode Next { get; set; } = next;
}

internal class CircularList
{
    private readonly Dictionary<int, CircularNode> Nodes = [];

    public CircularNode First => Nodes[0];
    public CircularNode Last => Nodes[Nodes.Count - 1];
    public int Count => Nodes.Count;

    public CircularList()
    {
        // Add first
        Nodes[0] = new CircularNode(0, null!);
        Nodes[0].Next = Nodes[0];

        for (byte i = 1; Nodes.Count < 256; i++)
        {
            var newNode = new CircularNode(i, First);
            Last.Next = newNode;
            Nodes[Nodes.Count] = newNode;
        }
    }

    public byte this[int index] { get => Nodes[index].Value; }

    public void Twist(int index, int length)
    {
        if (length == 1)
            return;

        int startIndex = index % Nodes.Count;
        int endIndex = (startIndex + length - 1) % Nodes.Count;

        var startNode = Nodes[startIndex];
        var endNode = Nodes[endIndex];

        var beforeNode = Nodes[(startIndex - 1 + Nodes.Count) % Nodes.Count];
        var afterNode = endNode.Next;
        
        beforeNode.Next = endNode;

        int reIndex = endIndex;
        var current = startNode;
        var next = afterNode;
        for (int i = 0; i < length; i++)
        {
            var oldNext = current.Next;
            current.Next = next;
            Nodes[reIndex] = current;
            next = current;
            current = oldNext;

            reIndex = (reIndex - 1 + Nodes.Count) % Nodes.Count;
        }
    }
}