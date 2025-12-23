using System.Text;

namespace AdventOfCode._2016.Day21;

internal class PasswordScrambler(string[] commands)
{
    private readonly string[] Commands = commands;

    public string Scramble(string password)
    {
        var sb = new StringBuilder(password);
        foreach (string command in Commands)
            Execute(command, sb, reverse: false);
        return sb.ToString();
    }

    public string UnScramble(string password)
    {
        var sb = new StringBuilder(password);
        for (int i = Commands.Length - 1; i >= 0; i--)
            Execute(Commands[i], sb, reverse: true);
        return sb.ToString();
    }

    private static void Execute(string command, StringBuilder sb, bool reverse)
    {
        string[] commandParts = command.Split(' ');
        switch (commandParts[0])
        {
            case "swap":
                ExecuteSwap(commandParts, sb);
                break;
            case "rotate":
                ExecuteRotate(commandParts, sb, reverse);
                break;
            case "reverse":
                ExecuteReverse(commandParts, sb);
                break;
            case "move":
                ExecuteMove(commandParts, sb, reverse);
                break;
        }
    }

    private static void ExecuteSwap(string[] commandParts, StringBuilder sb)
    {
        int x, y;
        if (commandParts[1] == "position")
        {
            x = int.Parse(commandParts[2]);
            y = int.Parse(commandParts[5]);
        }
        else
        {
            x = sb.IndexOf(commandParts[2][0]);
            y = sb.IndexOf(commandParts[5][0]);
        }
        (sb[y], sb[x]) = (sb[x], sb[y]);
    }

    private static void ExecuteRotate(string[] commandParts, StringBuilder sb, bool reverse)
    {
        bool toRight;
        int steps;
        if (commandParts.Length == 4)
        {
            toRight = commandParts[1] == "right";
            steps = int.Parse(commandParts[2]);
        }
        else
        {
            toRight = true;
            if (!reverse)
            {
                int pos = sb.IndexOf(commandParts[6][0]);
                steps = 1 + pos + (pos > 3 ? 1 : 0);
            }
            else
            {
                // multiple original states can produce the same result, so we just pick one of the many states :)
                var newPos = sb.IndexOf(commandParts[6][0]);
                newPos--;
                if (newPos % 2 == 1)
                    newPos += sb.Length;
                if (newPos % 2 == 1)
                    newPos--;
                int pos = newPos / 2;
                steps = 1 + pos + (pos > 3 ? 1 : 0);
            }
        }
        toRight = reverse != toRight;

        steps %= sb.Length;
        string rotated = sb.ToString();
        rotated = toRight
            ? rotated[^steps..] + rotated[..^steps]
            : rotated[steps..] + rotated[..steps];

        sb.Clear();
        sb.Append(rotated);
    }

    private static void ExecuteReverse(string[] commandParts, StringBuilder sb)
    {
        int from = int.Parse(commandParts[2]);
        int to = int.Parse(commandParts[4]);

        var rev = new StringBuilder();
        for (int i = 0; i < sb.Length; i++)
        {
            if (i >= from && i <= to)
                rev.Append(sb[from + to - i]);
            else
                rev.Append(sb[i]);
        }

        sb.Clear();
        sb.Append(rev);
    }

    private static void ExecuteMove(string[] commandParts, StringBuilder sb, bool reverse)
    {
        int x = int.Parse(commandParts[2]);
        int y = int.Parse(commandParts[5]);
        if (reverse)
            (x, y) = (y, x);

        char tmp = sb[x];
        sb.Remove(x, 1);
        sb.Insert(y, tmp);
    }
}

public static class StringBuilderExtensions
{
    public static int IndexOf(this StringBuilder sb, char search)
    {
        int index = -1;
        for (int i = 0; i < sb.Length; i++)
        {
            if (sb[i] == search)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
