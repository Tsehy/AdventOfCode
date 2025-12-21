namespace AdventOfCode
{
    public class _2022Day01 : _2022Day
    {
        private readonly List<int> Elves;

        public _2022Day01() : base("Day01")
        {
            Elves = new List<int>();

            ExtractData();

            Elves = Elves.OrderByDescending(e => e).ToList();
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Maximum amount of calories carried: {Elves[0]}");
        }

        public override void Part2()
        {
            base.Part2();

            int totalCalories = Elves.Take(3).Sum();

            Console.WriteLine($"Maximum amount of calories carried by the top 3: {totalCalories}");
        }

        #region Private methods
        private void ExtractData()
        {
            int index = 0;
            Elves.Add(index);

            foreach (string calorie in Input)
            {
                if (calorie != "")
                {
                    Elves[index] += int.Parse(calorie);
                }
                else
                {
                    Elves.Add(0);
                    index++;
                }
            }
        }
        #endregion
    }
}