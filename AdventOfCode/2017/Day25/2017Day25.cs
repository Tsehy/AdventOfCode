namespace AdventOfCode._2017.Day25;

public struct Operation(char writeValue, string direction, char nextState)
{
    public int WriteValue { get; set; } = writeValue - '0';
    public int Direction { get; set; } = direction == "right." ? 1 : -1;
    public char NextState { get; set; } = nextState;
}

public class _2017Day25 : _2017Day
{
    private readonly Dictionary<char, Operation[]> Logic = [];
    private readonly char StartState;
    private readonly int Iterations;

    public _2017Day25() : base("Day25")
    {
        StartState = Input[0][^2];
        Iterations = int.Parse(Input[1].Split(' ')[^2]);

        for (int i = 3; i < Input.Length; i += 10)
        {
            char state = Input[i][^2];
            var caseZero = new Operation(Input[i + 2][^2], Input[i + 3].Split(' ')[^1], Input[i + 4][^2]);
            var caseOne = new Operation(Input[i + 6][^2], Input[i + 7].Split(' ')[^1], Input[i + 8][^2]);
            Logic[state] = [caseZero, caseOne];
        }
    }

    public override void Part1()
    {
        base.Part1();

        char currentState = StartState;
        int cursor = 0;
        var tape = new Dictionary<int, int>();
        for (int i = 0; i < Iterations; i++)
        {
            if (!tape.TryGetValue(cursor, out int value))
                tape[cursor] = value = 0;

            var operation = Logic[currentState][value];

            tape[cursor] = operation.WriteValue;
            cursor += operation.Direction;
            currentState = operation.NextState;
        }

        Console.WriteLine(tape.Values.Sum());
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine("I'll take a bonus star any time *");
    }
}
