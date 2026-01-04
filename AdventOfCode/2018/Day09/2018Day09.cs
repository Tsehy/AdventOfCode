using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018.Day09;

public class _2018Day09 : _2018Day
{
    private readonly int playerCount;
    private readonly int maxMarble;

    public _2018Day09() : base("Day09")
    {
        string[] parts = Input[0].Split(' ');
        playerCount = int.Parse(parts[0]);
        maxMarble = int.Parse(parts[6]);
    }

    private static long PlayGame(int players, int maxMarble)
    {
        long[] scores = new long[players];
        var marbles = new LinkedList<int>();
        var current = marbles.AddFirst(0);

        int currentplayer = 0;

        for (int m = 1; m <= maxMarble; m++)
        {
            if (m % 23 != 0)
            {
                current = current!.Next ?? marbles.First;
                current = marbles.AddAfter(current!, m);
            }
            else
            {
                for (int i = 0; i < 7; i++)
                    current = current!.Previous ?? marbles.Last;

                scores[currentplayer] += m + current!.Value;
                var toRemove = current;
                current = current!.Next ?? marbles.First;
                marbles.Remove(toRemove);
            }
            currentplayer = (currentplayer + 1) % players;
        }

        return scores.Max();
    }

    public override void Part1()
    {
        base.Part1();

        Console.WriteLine($"The high score is: {PlayGame(playerCount, maxMarble)}");
    }

    public override void Part2()
    {
        base.Part2();

        Console.WriteLine($"The new high score is: {PlayGame(playerCount, maxMarble * 100)}");
    }
}
