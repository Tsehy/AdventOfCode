namespace AdventOfCode._2017.Day08;

public class _2017Day08 : _2017Day
{
    private readonly int AllTimeMax;
    private readonly int CurrentMax;

    public _2017Day08() : base("Day08")
    {
        var registers = new Dictionary<string, int>();
        AllTimeMax = int.MinValue;
        CurrentMax = int.MinValue;

        foreach (string line in Input)
        {
            string[] parts = line.Split(' ');

            int conditionLeft = registers.TryGetValue(parts[4], out int val) ? val : 0;
            int conditionRight = int.Parse(parts[6]);
            bool condition = parts[5] switch
            {
                ">" => conditionLeft > conditionRight,
                ">=" => conditionLeft >= conditionRight,
                "<" => conditionLeft < conditionRight,
                "<=" => conditionLeft <= conditionRight,
                "==" => conditionLeft == conditionRight,
                "!=" => conditionLeft != conditionRight,
                _ => throw new ArgumentException("Unknown conditional operator!"),
            };

            if (condition)
            {
                int originalValue = registers.TryGetValue(parts[0], out int orig) ? orig : 0;
                int operationValue = int.Parse(parts[2]);
                registers[parts[0]] = parts[1] switch
                {
                    "inc" => originalValue + operationValue,
                    "dec" => originalValue - operationValue,
                    _ => throw new ArgumentException("Unknown operation!")
                };
            }
             
            if (registers.Count > 0)
                CurrentMax = registers.Values.Max();

            if (CurrentMax > AllTimeMax)
                AllTimeMax = CurrentMax;
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"Largest value: {CurrentMax}");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"Largest value during the process: {AllTimeMax}");
    }
}
