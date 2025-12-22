namespace AdventOfCode._2016.Day19;

// Thank you Numberphile: https://www.youtube.com/watch?v=uCsD3ZGzMgE
public class _2016Day19 : _2016Day
{
    private readonly int NumberOfElves;

    public _2016Day19() : base("Day19")
    {
        NumberOfElves = int.Parse(Input[0]);
    }

    public override void Part1()
    {
        base.Part1();

        // Old code, fast, but not the best
        //List<int> elves = [.. Enumerable.Range(1, NumberOfElves)];
        //int rem = 0;
        //while (elves.Count > 1)
        //{
        //    int newRem = (elves.Count + rem) % 2;
        //    elves = [.. elves.Where((e, i) => i % 2 == rem)];
        //    rem = newRem;
        //}
        //Console.WriteLine($"Elf nr. {elves[0]} wins all presents.");

        int elf = 0;
        for (int i = 1; i <= NumberOfElves; i++)
        {
            if (elf + 2 > i)
            {
                elf = 1;
            }
            else
            {
                elf += 2;
            }
        }
        Console.WriteLine($"Elf nr. {elf} wins all presents.");
    }

    public override void Part2()
    {
        base.Part2();

        int elf = 0;
        int largest = 1;
        for (int i = 1; i <= NumberOfElves; i++)
        {
            if (elf + 2 > i)
            {
                largest = elf;
                elf = 1;
            }
            else
            {
                elf += (elf < largest) ? 1 : 2;
            }
        }
        Console.WriteLine($"Elf nr. {elf} wins all presents.");
    }
}
