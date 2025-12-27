namespace AdventOfCode._2017.Day02;

public class _2017Day02 : _2017Day
{
    public List<int[]> Sheet;

    public _2017Day02() : base("Day02")
    {
        Sheet = Input.Select(i => i.Split('\t').Select(s => int.Parse(s)).ToArray()).ToList();
    }

    public override void Part1()
    {
        base.Part1();

        int checkSum = Sheet.Select(r => r.Max() - r.Min()).Sum();
        Console.WriteLine($"The checksum is: {checkSum}");
    }

    public override void Part2()
    {
        base.Part2();

        int checkSum = 0;
        for (int row = 0; row < Sheet.Count; row++)
        {
            bool found = false;
            for (int i = 0; i < Sheet[row].Length - 1; i++)
            {
                for (int j = i + 1; j < Sheet[row].Length; j++)
                {
                    int a = Math.Max(Sheet[row][i], Sheet[row][j]);
                    int b = Math.Min(Sheet[row][i], Sheet[row][j]);
                
                    if (a % b == 0)
                    {
                        checkSum += a / b;
                        found = true;
                        break;
                    }
                }

                if (found)
                    break;
            }
        }

        Console.WriteLine($"The checksum is: {checkSum}");
    }
}
