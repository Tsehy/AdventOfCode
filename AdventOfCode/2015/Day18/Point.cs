namespace AdventOfCode._2015.Day18
{
    public class Point(int row, int col, bool state)
    {
        public int Row { get; set; } = row;
        public int Column { get; set; } = col;
        public bool State { get; set; } = state;
        public List<Point> Neighbours { get; set; } = [];
        public bool NextState { get; set; }
    }
}
