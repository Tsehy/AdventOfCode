namespace AdventOfCode._2017.Day23;

internal enum CommandType
{
    set, sub, mul, jnz
}

internal struct Command
{
    public CommandType Type;
    public int X;
    public int Y;
    public bool XIsRegister;
    public bool YIsRegister;

    public Command(string type, string reg1, string reg2)
    {
        Type = (CommandType)Enum.Parse(typeof(CommandType), type);

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

    public override string ToString() => $"{Type} {X} {Y}";
}

internal class Interpreter
{
    private readonly List<Command> Commands;
    public int[] Reg { get; } = new int[8];

    public int MulCount { get; private set; } = 0;

    public Interpreter(List<Command> commands, bool debugMode)
    {
        Commands = commands;
        Reg[0] = debugMode ? 0 : 1;
    }

    public void Process(bool onlySetup = false)
    {
        int CommandPointer = 0;
        int max = onlySetup ? 8 : Commands.Count; // extracted from input

        while (0 <= CommandPointer && CommandPointer < max)
        {
            var cmd = Commands[CommandPointer];
            switch (cmd.Type)
            {
                case CommandType.set:
                    Reg[cmd.X] = ResolveY(cmd);
                    CommandPointer++;
                    break;

                case CommandType.sub:
                    Reg[cmd.X] -= ResolveY(cmd);
                    CommandPointer++;
                    break;

                case CommandType.mul:
                    Reg[cmd.X] *= ResolveY(cmd);
                    CommandPointer++;
                    MulCount++;
                    break;

                case CommandType.jnz:
                    if ((cmd.XIsRegister ? Reg[cmd.X] : cmd.X) != 0)
                        CommandPointer += ResolveY(cmd);
                    else
                        CommandPointer++;
                    break;
            }
        }
    }

    private int ResolveY(Command cmd) => cmd.YIsRegister ? Reg[cmd.Y] : cmd.Y;
}
