namespace AdventOfCode._2016.Day18;

public class _2016Day18 : _2016Day
{
    private readonly bool[] Traps;

    public _2016Day18() : base("Day18")
    {
        Traps = [.. Input[0].Select(t => t == '^')];
    }

    public static bool[] GetNextRow(bool[] currRow)
    {
        bool[] nextRow = new bool[currRow.Length];
        for (int i = 0; i < currRow.Length; i++)
        {
            bool left = i != 0 && currRow[i - 1];
            bool center = currRow[i];
            bool right = i != currRow.Length - 1 && currRow[i + 1];
            nextRow[i] = (left && center && !right) || (!left && center && right) || (!left && !center && right) || (left && !center && !right);
        }
        return nextRow;
    }

    private int CountSafeSpaces(int numberOfRows)
    {
        bool[] currRow = Traps, nextRow;
        int index = 0, safeSpaces = 0;
        do
        {
            safeSpaces += currRow.Count(t => !t);
            nextRow = GetNextRow(currRow);
            currRow = nextRow;
            index++;
        } while (index < numberOfRows);
        return safeSpaces;
    }

    public override void Part1()
    {
        base.Part1();

        int safeSpaces = CountSafeSpaces(40);
        Console.WriteLine($"Number of safe spaces in 40 rows: {safeSpaces}");
    }

    public override void Part2()
    {
        base.Part2();

        int safeSpaces = CountSafeSpaces(400_000);
        Console.WriteLine($"Number of safe spaces in 400000 rows: {safeSpaces}");
    }
}
