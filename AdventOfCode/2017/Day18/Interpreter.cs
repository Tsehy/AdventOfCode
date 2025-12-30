namespace AdventOfCode._2017.Day18;

internal enum CommandType
{
    snd, set, add, mul, mod, rcv, jgz
}

internal struct Command
{
    public CommandType Type;
    public int X;
    public int Y;
    public bool XIsRegister;
    public bool YIsRegister;

    public Command(CommandType type, string reg1, string reg2)
    {
        Type = type;
        
        if (int.TryParse(reg1, out int x))
        {
            X = x;
            XIsRegister = false;
        }
        else
        {
            X = reg1[0] - 'a';
            XIsRegister = true;
        }

        if (int.TryParse(reg2, out int y))
        {
            Y = y;
            YIsRegister = false;
        }
        else
        {
            Y = reg2[0] - 'a';
            YIsRegister = true;
        }
    }
}

internal class Interpreter
{
    private readonly List<Command> Commands;
    private readonly long[] Reg = new long[26];

    private int CommandPointer = 0;

    public Queue<long> Incoming { get; set; } = [];
    public Queue<long> Outgoing { get; private set; } = [];

    public int SendCount { get; private set; } = 0;
    public bool IsBlocked { get; private set; }

    public Interpreter(List<Command> commands, int id = 0)
    {
        Commands = commands;
        Reg['p' - 'a'] = id;
    }

    public bool Step()
    {
        if (0 > CommandPointer || CommandPointer >= Commands.Count)
        {
            IsBlocked = true;
            return false;
        }

        var cmd = Commands[CommandPointer];
        switch (cmd.Type)
        {
            case CommandType.snd:
                Outgoing.Enqueue(Reg[cmd.X]);
                SendCount++;
                CommandPointer++;
                break;

            case CommandType.set:
                Reg[cmd.X] = ResolveY(cmd);
                CommandPointer++;
                break;

            case CommandType.add:
                Reg[cmd.X] += ResolveY(cmd);
                CommandPointer++;
                break;

            case CommandType.mul:
                Reg[cmd.X] *= ResolveY(cmd);
                CommandPointer++;
                break;

            case CommandType.mod:
                Reg[cmd.X] %= ResolveY(cmd);
                CommandPointer++;
                break;

            case CommandType.rcv:
                if (Incoming.Count == 0)
                {
                    IsBlocked = true;
                    return false;
                }
                Reg[cmd.X] = Incoming.Dequeue();
                CommandPointer++;
                break;

            case CommandType.jgz:
                if ((cmd.XIsRegister ? Reg[cmd.X] : cmd.X) > 0)
                    CommandPointer += (int)ResolveY(cmd);
                else
                    CommandPointer++;
                break;
        }

        IsBlocked = false;
        return true;
    }

    private long ResolveY(Command cmd) => cmd.YIsRegister ? Reg[cmd.Y] : cmd.Y;
}
