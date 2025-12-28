namespace AdventOfCode._2017.Day09;

public class _2017Day09 : _2017Day
{
    private readonly int Score;
    private readonly int RemovedCount;

    public _2017Day09() : base("Day09")
    {
        Score = 0;
        RemovedCount = 0;
        int level = 0;
        bool garbage = false;

        int index = 0;
        while (index < Input[0].Length)
        {
            char current = Input[0][index];
            if (current == '!') // escape char
            {
                index += 2;
                continue;
            }

            if (!garbage || current == '>') // absorb anything except closing sign
            {
                switch (Input[0][index])
                {
                    case '<':
                        garbage = true;
                        break;

                    case '>':
                        garbage = false;
                        break;

                    case '{':
                        level++;
                        break;

                    case '}':
                        Score += level;
                        level--;
                        break;
                }
            }
            else
            {
                RemovedCount++;
            }

            index++;
        }
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The total score is: {Score}");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"Removed characters: {RemovedCount}");
    }
}
