using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

public class _2022Day02 : _2022Day
{
    private List<string> opponent = new List<string>();
    private List<string> guide = new List<string>();

    public _2022Day02() : base("Day02")
    {
        ExtractData();
    }

    public override void Part1()
    {
        //base.Part1();

        Console.WriteLine($"The score you would get following the incorrect guide: {CalculateGames1(opponent, guide)}\n");
    }

    public override void Part2()
    {
        //base.Part2();

        Console.WriteLine($"The score you would get following the correct guide: {CalculateGames2(opponent, guide)}\n");
    }

    #region Private methods
    private void ExtractData()
    {
        foreach (string data in input)
        {
            string[] tmp = data.Split(' ');
            opponent.Add(tmp[0]);
            guide.Add(tmp[1]);
        }
    }

    private int CalculateGames1(List<string> opponent, List<string> guide)
    {
        /* A, X - Rock
         * B, Y - Paper
         * C, Z - Scissors
         */
        int totalScore = 0;

        for (int i = 0; i < opponent.Count; i++)
        {
            totalScore += CalculateRound1(opponent[i], guide[i]);
        }

        return totalScore;
    }

    private int CalculateRound1(string opponent, string guide)
    {
        int score = "-XYZ".IndexOf(guide);

        string battle = $"{opponent}{guide}";

        // win
        if ("AYBZCX".Contains(battle))
        {
            score += 6;
        }

        // tie
        if ("AXBYCZ".Contains(battle))
        {
            score += 3;
        }

        return score;
    }

    private int CalculateGames2(List<string> opponent, List<string> guide)
    {
        /* A - Rock
         * B - Paper
         * C - Scissors
         * X - Need to lose
         * Y - Need to tie
         * Z - Need to win
         */
        int totalScore = 0;

        for (int i = 0; i < opponent.Count; i++)
        {
            totalScore += CalculateRound2(opponent[i], guide[i]);
        }

        return totalScore;
    }

    private int CalculateRound2(string opponent, string guide)
    {
        int score = 0;
        int opponentId = "-ABC".IndexOf(opponent);

        switch (guide)
        {
            // lose
            case "X":
                if (opponentId == 1)
                {
                    opponentId = 4;
                }
                score += opponentId - 1; // lose: 0pt + one bellow the opponent
                break; 
            
            // tie
            case "Y":
                score += opponentId + 3;
                break;

            // win
            case "Z":
                if (opponentId == 3)
                {
                    opponentId = 0;
                }
                score += opponentId + 7; // win: 6tp + one above the opponent
                break;
        }

        return score;
    }
    #endregion
}
