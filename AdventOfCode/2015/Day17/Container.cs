namespace AdventOfCode._2015.Day17
{
    public class Container(int size)
    {
        public int Size { get; init; } = size;
        public bool Used { get; set; } = false;
    }
}
