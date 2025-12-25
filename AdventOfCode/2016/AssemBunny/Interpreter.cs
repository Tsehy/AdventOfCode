namespace AdventOfCode._2016.AssemBunny;

internal enum CommandType
{
    inc, dec, cpy, jnz, tgl, mlt, nop
}

internal readonly record struct Command(CommandType Type, int? ValueParam1, string? KeyParam1, int? ValueParam2, string? KeyParam2, int? ValueParam3, string? Keyparam3);

internal static class CommandParser
{
    public static List<Command> Parse(string[] input)
    {
        return [.. input.Select(i => {
            string[] parts = i.Split(' ');
            var type = (CommandType)Enum.Parse(typeof(CommandType), parts[0]);

            int? value1 = null, value2 = null, value3 = null;
            string? key1 = null, key2 = null, key3 = null;

            if (parts.Length > 1)
            {
                if (int.TryParse(parts[1], out int val1))
                    value1 = val1;
                else
                    key1 = parts[1];
            }

            if (parts.Length > 2)
            {
                if (int.TryParse(parts[2], out int val2))
                    value2 = val2;
                else
                    key2 = parts[2];
            }

            if (parts.Length > 3){
                if (int.TryParse(parts[3], out int val3))
                    value3 = val3;
                else
                    key3 = parts[3];
            }

            return new Command(type, value1, key1, value2, key2, value3, key3);
        })];
    }
}

internal class Interpreter
{
    public List<Command> Commands { get; set; }
    public Dictionary<string, int> Registers { get; set; } = [];

    public Interpreter(List<Command> commands)
    {
        Commands = commands;
        Registers["a"] = 0;
        Registers["b"] = 0;
        Registers["c"] = 0;
        Registers["d"] = 0;
    }

    public void SimplifyMultiplication()
    {
        for (int i = 0; i < Commands.Count - 4; i++)
        {
            if (Commands[i].Type is not (CommandType.inc or CommandType.dec))
                continue;

            if (Commands[i + 1].Type is not (CommandType.inc or CommandType.dec) || Commands[i].Type == Commands[i+1].Type)
                continue;

            string? dec1Param = (Commands[i].Type == CommandType.dec) ? Commands[i].KeyParam1 : Commands[i + 1].KeyParam1;

            if (!(Commands[i + 2].Type == CommandType.jnz && Commands[i + 2].KeyParam1 == dec1Param && Commands[i + 2].ValueParam2 == -2))
                continue;

            if (Commands[i + 3].Type != CommandType.dec)
                continue;

            string? dec2Param = Commands[i + 3].KeyParam1;

            if (!(Commands[i + 4].Type == CommandType.jnz && Commands[i + 4].KeyParam1 == dec2Param && Commands[i + 4].ValueParam2 == -5))
                continue;

            string? incParam = (Commands[i].Type == CommandType.inc) ? Commands[i].KeyParam1 : Commands[i + 1].KeyParam1;

            Commands[i] = new Command(CommandType.nop, null, null, null, null, null, null);
            Commands[i + 1] = new Command(CommandType.nop, null, null, null, null, null, null);
            Commands[i + 2] = new Command(CommandType.nop, null, null, null, null, null, null);
            Commands[i + 3] = new Command(CommandType.nop, null, null, null, null, null, null);
            Commands[i + 4] = new Command(CommandType.mlt, null, dec1Param, null, dec2Param, null, incParam);
            i += 4;
        }
    }

    public void ExecuteCommands()
    {
        List<Command> deepCopy = [.. Commands];

        int nextCommandIndex = 0;
        while (nextCommandIndex >= 0 && nextCommandIndex < deepCopy.Count)
        {
            Command command = deepCopy[nextCommandIndex];
            switch (command.Type)
            {
                case CommandType.cpy:
                    if (command.KeyParam2 == null)
                        break;

                    Registers[command.KeyParam2] = (command.KeyParam1 != null) ? Registers[command.KeyParam1] : (command.ValueParam1 ?? 0);
                    break;

                case CommandType.inc:
                    if (command.KeyParam1 != null)
                        Registers[command.KeyParam1] = Registers[command.KeyParam1] + 1;
                    break;

                case CommandType.dec:
                    if (command.KeyParam1 != null)
                        Registers[command.KeyParam1] = Registers[command.KeyParam1] - 1;
                    break;

                case CommandType.jnz:
                    int param1 = (command.KeyParam1 != null) ? Registers[command.KeyParam1] : (command.ValueParam1 ?? 0);
                    int param2 = (command.KeyParam2 != null) ? Registers[command.KeyParam2] : (command.ValueParam2 ?? 0);
                    if (param1 != 0)
                    {
                        nextCommandIndex += param2;
                        continue;
                    }
                    break;

                case CommandType.tgl:
                    int targetIndex = nextCommandIndex + ((command.KeyParam1 != null) ? Registers[command.KeyParam1] : (command.ValueParam1 ?? 0));
                    if (targetIndex < deepCopy.Count && targetIndex >= 0)
                    {
                        var targetcommand = deepCopy[targetIndex];
                        var newType = targetcommand.Type switch
                        {
                            CommandType.jnz => CommandType.cpy,
                            CommandType.cpy => CommandType.jnz,
                            CommandType.inc => CommandType.dec,
                            CommandType.mlt => CommandType.mlt, // No other 3param operator
                            CommandType.nop => CommandType.nop, // No other paramless operator
                            _ => CommandType.inc,
                        };
                        deepCopy[targetIndex] = targetcommand with { Type = newType };
                    }
                    break;

                // mlt (a: register or number) (b: register or number) (c: register)
                // a <- 0
                // b <- 0
                // c <- c + a * b
                case CommandType.mlt:
                    if (command.Keyparam3 == null)
                        break;

                    int mult1 = (command.KeyParam1 != null) ? Registers[command.KeyParam1] : (command.ValueParam1 ?? 0);
                    if (command.KeyParam1 != null)
                        Registers[command.KeyParam1] = 0;

                    int mult2 = (command.KeyParam2 != null) ? Registers[command.KeyParam2] : (command.ValueParam2 ?? 0);
                    if (command.KeyParam2 != null)
                        Registers[command.KeyParam2] = 0;

                    Registers[command.Keyparam3] += mult1 * mult2;
                    break;

                case CommandType.nop:
                default:
                    break;
            }
            nextCommandIndex++;
        }
    }
}
