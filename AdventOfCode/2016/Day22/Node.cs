namespace AdventOfCode._2016.Day22;
internal class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Size { get; set; }
    public int Used { get; set; }
    public int Available => Size - Used;
    public bool IsUnmoveable { get; set; }

    public Node(string name, int size, int used)
    {
        Size = size;
        Used = used;
        string[] parts = name.Split('-');
        X = int.Parse(parts[1][1..]);
        Y = int.Parse(parts[2][1..]);
    }
}