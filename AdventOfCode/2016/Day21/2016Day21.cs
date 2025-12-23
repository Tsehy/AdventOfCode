namespace AdventOfCode._2016.Day21;

public class _2016Day21 : _2016Day
{
    private readonly PasswordScrambler Scrambler;
    public _2016Day21() : base("Day21")
    {
        Scrambler = new PasswordScrambler(Input);
    }

    public override void Part1()
    {
        base.Part1();

        string scrambled = Scrambler.Scramble("abcdefgh");
        Console.WriteLine($"The scrambled password is: {scrambled}");
    }

    public override void Part2()
    {
        base.Part2();

        string unScrambled = Scrambler.UnScramble("fbgdceah");
        Console.WriteLine($"The un-scrambled password is: {unScrambled}");
    }
}
