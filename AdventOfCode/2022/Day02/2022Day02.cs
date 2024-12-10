namespace AdventOfCode
{
    public class _2022Day02 : _2022Day
    {
        private readonly List<Tuple<char, char>> rounds = new(); // 1st opponent, 2nd guide

        public _2022Day02() : base("Day02")
        {
            ExtractData();
        }

        public override void Part1()
        {
            base.Part1();

            int totalScore = rounds.Select(r => WithWrongGuide(r)).Sum();

            Console.WriteLine($"The score you would get following the incorrect guide: {totalScore}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int totalScore = rounds.Select(r => WithCorrectGuide(r)).Sum();

            Console.WriteLine($"The score you would get following the correct guide: {totalScore}\n");
        }

        #region Private methods
        private void ExtractData()
        {
            foreach (string data in Input)
            {
                string[] tmp = data.Split(' ');
                rounds.Add(new Tuple<char, char>(tmp[0][0], tmp[1][0]));
            }
        }

        private int WithWrongGuide(Tuple<char, char> round)
        {
            int score = "-XYZ".IndexOf(round.Item1);

            string battle = $"{round.Item1}{round.Item2}";

            if ("AYBZCX".Contains(battle))
            {   // win
                score += 6;
            }

            if ("AXBYCZ".Contains(battle))
            {   // tie
                score += 3;
            }

            return score;
        }

        private int WithCorrectGuide(Tuple<char, char> round)
        {
            int score = 0;
            int opponentId = "-ABC".IndexOf(round.Item1);

            switch (round.Item2)
            {
                // lose
                case 'X':
                    if (opponentId == 1)
                    {
                        opponentId = 4;
                    }
                    score += opponentId - 1; // lose: 0pt + one bellow the opponent
                    break;

                // tie
                case 'Y':
                    score += opponentId + 3;
                    break;

                // win
                case 'Z':
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
}