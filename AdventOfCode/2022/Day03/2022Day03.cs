namespace AdventOfCode
{
    public class _2022Day03 : _2022Day
    {
        private readonly List<Tuple<string, string>> bagpacks = new();
        private readonly List<Tuple<string, string, string>> groups = new();

        public _2022Day03() : base("Day03")
        {
            ExtractData();
            ExtractDataForPart2();
        }

        public override void Part1()
        {
            base.Part1();

            int sumPriority = bagpacks.Select(b => b.Item1.Where(c => b.Item2.Contains(c)).Select(c => GetPriority(c)).First()).Sum();

            Console.WriteLine($"The totaly priority of the elements that appear in both compartments: {sumPriority}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int sumGroupPriority = groups.Select(g => g.Item1.Where(c => g.Item2.Contains(c) && g.Item3.Contains(c)).Select(c => GetPriority(c)).First()).Sum();

            Console.WriteLine($"The totaly priority of the groups: {sumGroupPriority}\n");
        }

        #region Private methods
        private void ExtractData()
        {
            foreach (string data in Input)
            {
                bagpacks.Add(Tuple.Create(
                    data.Substring(0, data.Length / 2),
                    data.Substring(data.Length / 2, data.Length / 2)
                ));
            }
        }

        private void ExtractDataForPart2()
        {
            for (int i = 0; i < Input.Length; i += 3)
            {
                groups.Add(Tuple.Create(Input[i], Input[i + 1], Input[i + 2]));
            }
        }

        private static int GetPriority(char item)
        {
            int result = item;

            if (result > 90)
            {
                result -= 96;
            }
            else
            {
                result -= 38;
            }

            return result;
        }
        #endregion
    }
}