namespace AdventOfCode
{
    public class _2022Day04 : _2022Day
    {
        private readonly List<Tuple<Tuple<int, int>, Tuple<int, int>>> pairs = new();

        public _2022Day04() : base("Day04")
        {
            ExtractData();
        }

        public override void Part1()
        {
            base.Part1();

            // TODO cleaner solution
            int numberOfFullRangeContainingPairs = pairs.Where(p => p.Item1.Item1 <= p.Item2.Item1 && p.Item1.Item2 >= p.Item2.Item2 || p.Item1.Item1 >= p.Item2.Item1 && p.Item1.Item2 <= p.Item2.Item2).Count();

            Console.WriteLine($"Ranges fully containing the other: {numberOfFullRangeContainingPairs}\n");
        }

        public override void Part2()
        {
            base.Part2();

            // all - not overlapping
            int numberOfOverlappingPairs = pairs.Count - pairs.Where(p => p.Item1.Item2 < p.Item2.Item1 || p.Item1.Item1 > p.Item2.Item2).Count();

            Console.WriteLine($"Panges overlapping: {numberOfOverlappingPairs}\n");
        }

        #region Private methods
        private void ExtractData()
        {
            foreach (string data in Input)
            {
                int[] tmp = Array.ConvertAll(data.Replace('-', ',').Split(','), s => int.Parse(s));
                pairs.Add(Tuple.Create(Tuple.Create(tmp[0], tmp[1]), Tuple.Create(tmp[2], tmp[3])));
            }
        }
        #endregion
    }
}