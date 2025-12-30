using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017.Day15;

public class Generator(long factor, int firstValue)
{
    private readonly long Factor = factor;
    public int Value { get; private set; } = firstValue;
    public int Last16Bits => (Value << 16) >>> 16; // "erase" the first 16 bits0

    public void NextValue() => Value = (int)(Value * Factor % int.MaxValue);
    public void NextDivisibleBy(int divider)
    {
        do
        {
            NextValue();
        }
        while (Value % divider != 0);
    }
}

public class _2017Day15 : _2017Day
{
    private readonly int GenASeed;
    private readonly int GenBSeed;

    public _2017Day15() : base("Day15")
    {
        GenASeed = int.Parse(Input[0].Split(' ')[4]);
        GenBSeed = int.Parse(Input[1].Split(' ')[4]);
    }

    public override void Part1()
    {
        base.Part1();

        var a = new Generator(16_807, GenASeed);
        var b = new Generator(48_271, GenBSeed);

        int count = 0;
        for (int i = 0; i < 40_000_000; i++)
        {
            a.NextValue();
            b.NextValue();
            if (a.Last16Bits == b.Last16Bits)
                count++;
        }

        Console.WriteLine($"The judge's final coutn is: {count}");
    }

    public override void Part2()
    {
        base.Part2();

        var a = new Generator(16_807, GenASeed);
        var b = new Generator(48_271, GenBSeed);

        int count = 0;
        for (int i = 0; i < 5_000_000; i++)
        {
            a.NextDivisibleBy(4);
            b.NextDivisibleBy(8);
            if (a.Last16Bits == b.Last16Bits)
                count++;
        }

        Console.WriteLine($"The judge's final coutn is: {count}");
    }
}
