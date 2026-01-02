namespace AdventOfCode._2017.Day22;

public readonly record struct Point(int X, int Y)
{
    public Point TurnLeft() => new(Y, -X);
    public Point TurnRight() => new(-Y, X);

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
}

public class _2017Day22 : _2017Day
{
    private readonly Dictionary<Point, char> Infected = [];

    public _2017Day22() : base("Day22")
    {
        int size = Input.Length;
        int offset = size / 2;
        // x increases to right
        // y increases to down
        for (int row = 0; row < size; row++)
            for (int col = 0; col < size; col++)
                Infected[new(col - offset, row - offset)] = Input[row][col];
    }

    public override void Part1()
    {
        base.Part1();

        var deepCopy = Infected.ToDictionary(i => i.Key, i => i.Value);
        Point position = new(0, 0);
        Point facing = new(0, -1);

        int infectionCaused = 0;
        for (int i = 0; i < 10_000; i++)
        {
            if (!deepCopy.TryGetValue(position, out char state))
                state = '.';

            switch (state)
            {
                case '.':
                    deepCopy[position] = '#';
                    infectionCaused++;
                    facing = facing.TurnLeft();
                    break;

                case '#':
                    deepCopy[position] = '.';
                    facing = facing.TurnRight();
                    break;
            }

            position += facing;
        }

        Console.WriteLine($"After 10_000 bursts {infectionCaused} bursts caused infection");
    }

    public override void Part2()
    {
        base.Part2();

        var deepCopy = Infected.ToDictionary(i => i.Key, i => i.Value);
        Point position = new(0, 0);
        Point facing = new(0, -1);

        int infectionCaused = 0;
        for (int i = 0; i < 10_000_000; i++)
        {
            if (!deepCopy.TryGetValue(position, out char state))
                state = '.';

            switch (state)
            {
                case '.':
                    deepCopy[position] = 'W';
                    facing = facing.TurnLeft();
                    break;

                case 'W':
                    deepCopy[position] = '#';
                    infectionCaused++;
                    break;

                case '#':
                    deepCopy[position] = 'F';
                    facing = facing.TurnRight();
                    break;

                case 'F':
                    deepCopy[position] = '.';
                    facing = facing.TurnRight().TurnRight();
                    break;
            }

            position += facing;
        }

        Console.WriteLine($"After 10_000_000 bursts {infectionCaused} bursts caused infection.");
    }
}
