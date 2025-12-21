namespace AdventOfCode;

public class _2016Day02 : _2016Day
{
    public _2016Day02() : base("Day02")
    {
    }

    private static void Move(ref int x, ref int y, char direction)
    {
        switch (direction)
        {
            case 'L':
                if (x > 0) x--;
                break;

            case 'U':
                if (y > 0) y--;
                break;

            case 'R':
                if (x < 2) x++;
                break;

            case 'D':
                if (y < 2) y++;
                break;
        }
    }

    public override void Part1()
    {
        base.Part1();

        int x = 1, y = 1;
        string password = string.Empty;
        foreach (string code in Input)
        {
            foreach (char c in code)
                Move(ref x, ref y, c);
            int num = 1 + x + 3 * y;
            password += num.ToString();
        }

        Console.WriteLine($"The bathroom code is: {password}");
    }

    public static void Move2(ref int x, ref int y, char direction)
    {
        int newX = x, newY = y;
        switch (direction)
        {
            case 'L':
                newX--;
                break;

            case 'U':
                newY--;
                break;

            case 'R':
                newX++;
                break;

            case 'D':
                newY++;
                break;
        }
        if (Math.Abs(newX) + Math.Abs(newY) != 3)
        {
            x = newX;
            y = newY;
        }
    }

    public override void Part2()
    {
        base.Part2();

        // 7 is (0,0)
        string magic = "..1...234.56789.ABC...D..";
        int x = -2, y = 0;
        string password = string.Empty;
        foreach (string code in Input)
        {
            foreach (char c in code)
                Move2(ref x, ref y, c);
            password += magic[(x + 2) + 5 * (y + 2)];
        }

        Console.WriteLine($"The correct bathroom code is: {password}");
    }
}
