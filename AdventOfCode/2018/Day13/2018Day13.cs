namespace AdventOfCode._2018.Day13;

internal enum TurnType
{
    Left, Straight, Right,
}

internal struct Point(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public Point(char facing) : this(0, 0)
    {
        switch (facing)
        {
            case '<':
                X = -1;
                break;
            case '>':
                X = 1;
                break;
            case '^':
                Y = -1;
                break;
            case 'v':
                Y = 1;
                break;
        }
    }

    public readonly Point Turn(TurnType turn) => turn switch
    {
        TurnType.Left => new(Y, -X),
        TurnType.Right => new(-Y, X),
        _ => this
    };

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);
    public override readonly string ToString() => $"{X},{Y}";
}

internal class Cart(int x, int y, char facing)
{
    public Point Position { get; set; } = new(x, y);
    public Point Direction { get; set; } = new(facing);
    public TurnType NextTurn { get; set; } = TurnType.Left;
    public bool Crashed { get; set; } = false;

    internal void TurnCurve(char current)
    {
        var horizontal = TurnType.Left;
        var vertical = TurnType.Right;
        if (current == '\\')
            (horizontal, vertical) = (vertical, horizontal);

        if (Direction.X == 0)
            Direction = Direction.Turn(vertical);
        else
            Direction = Direction.Turn(horizontal);
    }
}

public class _2018Day13 : _2018Day
{
    private readonly char[,] Grid;
    private List<Cart> Carts = [];

    public _2018Day13() : base("Day13")
    {
        int width = Input[0].Length;
        int height = Input.Length;
        Grid = new char[width, height];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                char p = Input[row][col];
                Grid[col, row] = p;
                if (p is '<' or '>' or '^' or 'v')
                    Carts.Add(new(col, row, p));
            }
        }
    }

    private void MoveAllCarts()
    {
        var cartPositions = Carts.Select(c => c.Position).ToHashSet();
        var crashes = new HashSet<Point>();
        foreach (var cart in Carts)
        {
            // did another cart crash into this one?
            if (crashes.Contains(cart.Position))
            {
                cart.Crashed = true;
                continue;
            }

            // the cart moves away
            cartPositions.Remove(cart.Position);
            cart.Position += cart.Direction;

            // turn if necessary
            char current = Grid[cart.Position.X, cart.Position.Y];
            switch (current)
            {
                case '+':
                    cart.Direction = cart.Direction.Turn(cart.NextTurn);
                    cart.NextTurn = (TurnType)(((int)cart.NextTurn + 1) % 3);
                    break;

                case '/':
                case '\\':
                    cart.TurnCurve(current);
                    break;
            }

            // did we crash into another cart?
            if (!cartPositions.Add(cart.Position))
            {
                cart.Crashed = true;
                crashes.Add(cart.Position);
            }
        }

        // the cart moved, and after that a cart crashed into it
        foreach (var cart in Carts.Where(c => crashes.Contains(c.Position)))
            cart.Crashed = true;

        // always itertate from top to bottom, left to right
        Carts = [.. Carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X)];
    }

    public override void Part1()
    {
        base.Part1();

        Point? crash = null;
        do
        {
            MoveAllCarts();
            crash = Carts.FirstOrDefault(c => c.Crashed)?.Position;
        }
        while (crash == null);

        Console.WriteLine($"The first crash is at: {crash}");
    }

    public override void Part2()
    {
        base.Part2();

        do
        {
            Carts.RemoveAll(c => c.Crashed);
            MoveAllCarts();
        }
        while (Carts.Count > 1);

        Console.WriteLine($"The last remaining cart is at: {Carts[0].Position}");
    }
}
