namespace AdventOfCode._2017.Day16;

public class _2017Day16 : _2017Day
{
    private readonly List<string> Positions;

    public _2017Day16() : base("Day16")
    {
        string[] DanceMoves = Input[0].Split(',');
        List<char> programs = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'];

        string position = string.Join("", programs);
        var hList = new HashSet<string> { position };
        do
        {
            foreach (string danceMove in DanceMoves)
            {
                switch (danceMove[0])
                {
                    case 's': // Spin
                        int rotation = int.Parse(danceMove[1..]);
                        programs = [.. programs.Select((p, i) => (ch: p, pos: (i + rotation) % 16)).OrderBy(g => g.pos).Select(g => g.ch)];
                        break;

                    case 'x': // Exchange
                        int[] v = [.. danceMove[1..].Split('/').Select(int.Parse)];
                        (programs[v[0]], programs[v[1]]) = (programs[v[1]], programs[v[0]]);
                        break;

                    case 'p': // Partner
                        int pos1 = programs.IndexOf(danceMove[1]);
                        int pos2 = programs.IndexOf(danceMove[3]);
                        (programs[pos1], programs[pos2]) = (programs[pos2], programs[pos1]);
                        break;
                }
            }

            position = string.Join("", programs);
        }
        while (hList.Add(position));

        Positions = [.. hList];
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The order is {Positions[1]} after one cycle.");
    }

    public override void Part2()
    {
        base.Part2();

        int index = 1_000_000_000;
        int realIndex = index % Positions.Count;
        Console.WriteLine($"The order is {Positions[realIndex]} after one billion cycles.");
    }
}
