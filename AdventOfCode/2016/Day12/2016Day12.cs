namespace AdventOfCode._2016.Day12;

public class _2016Day12 : _2016Day
{
    private readonly Dictionary<string, int> Registers = [];
    private readonly List<string[]> Commands;

    public _2016Day12() : base("Day12")
    {
        Registers["a"] = 0;
        Registers["b"] = 0;
        Registers["c"] = 0;
        Registers["d"] = 0;

        Commands = [.. Input.Select(i => i.Split(' '))];
    }

    private void ExecuteCommands()
    {
        int nextCommandIndex = 0;
        while (nextCommandIndex >= 0 && nextCommandIndex < Commands.Count)
        {
            string[] command = Commands[nextCommandIndex];
            switch (Commands[nextCommandIndex][0])
            {
                case "cpy":
                    if (Registers.TryGetValue(command[1], out int registerValue))
                        Registers[command[2]] = registerValue;
                    else
                        Registers[command[2]] = int.Parse(command[1]);
                    break;

                case "inc":
                    Registers[command[1]] = Registers[command[1]] + 1;
                    break;

                case "dec":
                    Registers[command[1]] = Registers[command[1]] - 1;
                    break;

                case "jnz":
                    if (Registers.TryGetValue(command[1], out int jmp))
                    {
                        if (jmp != 0)
                        {
                            nextCommandIndex += int.Parse(command[2]);
                            continue;
                        }
                    }
                    else if (command[1] != "0")
                    {
                        nextCommandIndex += int.Parse(command[2]);
                        continue;
                    }
                    break;
            }
            nextCommandIndex++;
        }
    }

    public override void Part1()
    {
        base.Part1();

        ExecuteCommands();
        Console.WriteLine($"Value in register a: {Registers["a"]}");
    }

    public override void Part2()
    {
        base.Part2();

        Registers["a"] = 0;
        Registers["b"] = 0;
        Registers["c"] = 1;
        Registers["d"] = 0;

        ExecuteCommands();
        Console.WriteLine($"Value in register a: {Registers["a"]}");
    }
}
