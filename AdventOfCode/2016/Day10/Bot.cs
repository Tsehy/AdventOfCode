namespace AdventOfCode._2016.Day10;

internal class Bot(string lowest, string highest)
{
    public string Lowest { get; set; } = lowest;
    public string Highest { get; set; } = highest;
    public List<int> Values { get; set; } = [];
}
