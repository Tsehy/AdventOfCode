namespace AdventOfCode._2016.Day23;

internal enum CommandType
{
    inc, dec, cpy, jnz, tgl
}

internal readonly record struct Command(CommandType Type, int? ValueParam1, string? KeyParam1, int? ValueParam2, string? KeyParam2);
// I saved a whole minute by refactoring the whole code to this form
// I know I could try try to implement the multiplication, but I have no idea how ...
// So I'll be happy with my 02:49.47 runtime

public class _2016Day23 : _2016Day
{
    private readonly Dictionary<string, int> Registers = [];
    private readonly List<Command> Commands;

    public _2016Day23() : base("Day23")
    {
        Commands = [.. Input.Select(i => {
            string[] parts = i.Split(' ');
            var type = (CommandType)Enum.Parse(typeof(CommandType), parts[0]);
            int? value1 = null, value2 = null;
            string? key1 = null, key2 = null;
            if (int.TryParse(parts[1], out int val1))
                value1 = val1;
            else
                key1 = parts[1];

            if (parts.Length > 2)
            {
                if (int.TryParse(parts[2], out int val2))
                    value2 = val2;
                else
                    key2 = parts[2];
            }
            return new Command(type, value1, key1, value2, key2);
        })];
    }

    private void ExecuteCommands(List<Command> commands)
    {
        int nextCommandIndex = 0;
        while (nextCommandIndex >= 0 && nextCommandIndex < commands.Count)
        {
            Command command = commands[nextCommandIndex];
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
                    if (targetIndex < commands.Count && targetIndex >= 0)
                    {
                        var targetcommand = commands[targetIndex];
                        var newType = targetcommand.Type switch
                        {
                            CommandType.jnz => CommandType.cpy,
                            CommandType.cpy => CommandType.jnz,
                            CommandType.inc => CommandType.dec,
                            _ => CommandType.inc,
                        };
                        commands[targetIndex] = targetcommand with { Type = newType };
                    }
                    break;
            }
            nextCommandIndex++;
        }
    }

    public override void Part1()
    {
        base.Part1();

        Registers["a"] = 7;
        Registers["b"] = 0;
        Registers["c"] = 0;
        Registers["d"] = 0;

        var deepCopy = new List<Command>(Commands);
        ExecuteCommands(deepCopy);
        Console.WriteLine($"The password is: {Registers["a"]}");
    }

    public override void Part2()
    {
        base.Part2();

        Registers["a"] = 12;
        Registers["b"] = 0;
        Registers["c"] = 0;
        Registers["d"] = 0;

        var deepCopy = new List<Command>(Commands);
        ExecuteCommands(deepCopy);
        Console.WriteLine($"The password is: {Registers["a"]}");
    }
}
