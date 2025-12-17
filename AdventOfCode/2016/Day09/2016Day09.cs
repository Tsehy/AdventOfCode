namespace AdventOfCode;

public class _2016Day09 : _2016Day
{
    public _2016Day09() : base("Day09")
    {
    }

    private static long GetLength(string input, bool recursive)
    {
        long length = 0;
        for (int index = 0; index < input.Length; index++)
        {
            if (input[index] != '(')
            {
                length++;
            }
            else
            {
                int closingBracket = input.IndexOf(')', index);
                int[] parts = [.. input.Substring(index + 1, closingBracket - index - 1).Split('x').Select(int.Parse)];
                if (recursive)
                    length += GetLength(input.Substring(closingBracket + 1, parts[0]), recursive) * parts[1];
                else
                    length += parts[0] * parts[1];
                index = closingBracket + parts[0];
            }
        }
        return length;
    }

    public override void Part1()
    {
        base.Part1();

        long length = GetLength(Input[0], recursive: false);
        Console.WriteLine($"The lenght after one decompreccion is: {length}\n");
    }

    public override void Part2()
    {
        base.Part2();

        long length = GetLength(Input[0], recursive: true);
        Console.WriteLine($"The lenght after recursive decompreccion is: {length}\n");
    }
}
