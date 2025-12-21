namespace AdventOfCode;

public class _2016Day08 : _2016Day
{
    private readonly bool[,] Display = new bool[6, 50];

    public _2016Day08() : base("Day08")
    {
        var instructions = Input.Select(i => i.Split([' ', 'x', '=']));
        foreach (string[] instruction in instructions)
        {
            if (instruction[0] == "rect")
            {
                int width = int.Parse(instruction[1]);
                int height = int.Parse(instruction[2]);
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        Display[i, j] = true;
            }
            else if (instruction[1] == "row")
            {
                int row = int.Parse(instruction[3]);
                int rotateBy = int.Parse(instruction[5]);
                bool[] newRow = [.. Enumerable.Range(0, Display.GetLength(1)).Select(x => Display[row, (x - rotateBy + Display.GetLength(1)) % Display.GetLength(1)])];
                for (int j = 0; j < Display.GetLength(1); j++)
                    Display[row, j] = newRow[j];
            }
            else
            {
                int column = int.Parse(instruction[4]);
                int rotateBy = int.Parse(instruction[6]);
                bool[] newRow = [.. Enumerable.Range(0, Display.GetLength(0)).Select(x => Display[(x - rotateBy + Display.GetLength(0)) % Display.GetLength(0), column])];
                for (int i = 0; i < Display.GetLength(0); i++)
                    Display[i, column] = newRow[i];
            }
        }
    }

    public override void Part1()
    {
        base.Part1();

        int litPixels = Display.Cast<bool>().Count(p => p);
        Console.WriteLine($"{litPixels} pixels are lit.");
    }

    public override void Part2()
    {
        base.Part2();

        for (int i = 0; i < Display.GetLength(0); i++)
        {
            for (int j = 0; j < Display.GetLength(1); j++)
                Console.Write(Display[i, j] ? "#" : " ");
            Console.WriteLine();
        }
    }
}
