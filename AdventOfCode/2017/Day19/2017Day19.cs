namespace AdventOfCode._2017.Day19;
public class _2017Day19 : _2017Day
{
    private readonly string Letters = string.Empty;
    private readonly int Steps = 0;
    public _2017Day19() : base("Day19")
    {
        int row = 0, col;
        for (col = 0; col < Input[0].Length; col++)
            if (Input[row][col] == '|')
                break;

        int dx = 0, dy = 1;
        bool end = false;
        do
        {
            Steps++;
            col += dx;
            row += dy;

            switch (Input[row][col])
            {
                case ' ':
                    end = true;
                    break;

                case '+':
                    (dx, dy) = (dy, dx);
                    if (Input[row + dy][col + dx] == ' ')
                    {
                        dx = -dx;
                        dy = -dy;
                    }
                    break;

                case '-':
                case '|':
                    break;

                default:
                    Letters += Input[row][col];
                    break;
            }
        }
        while (!end);
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The packet will see {Letters} letters in order.");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The path is {Steps} long.");
    }
}
