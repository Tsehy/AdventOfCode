using System.Numerics;

namespace AdventOfCode._2015.Day24
{
    public class _2015Day24 : _2015Day
    {
        private readonly List<int> Weights;

        public _2015Day24() : base("Day24")
        {
            Weights = [.. Input.Select(int.Parse)];
        }

        #region Reddit moment
        // Thank you u/JeffJankowski, you saved me from my pain
        // https://www.reddit.com/r/adventofcode/comments/3y1s7f/comment/cy9ufm6/
        private static IEnumerable<IEnumerable<T>> Combinations<T>(int k, IEnumerable<T> items)
        {
            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            if (!items.Any())
                yield break;

            var head = items.First();
            var tail = items.Skip(1);

            foreach (var combination in Combinations(k - 1, tail))
            {
                yield return new[] { head }.Concat(combination);
            }

            foreach (var combination in Combinations(k, tail))
            {
                yield return combination;
            }
        }

        private long? MinQE(List<int>nums, int groups)
        {
            int target = nums.Sum() / groups;
            return Enumerable.Range(2, nums.Count / groups - 2)
                .SelectMany(n => Combinations(n, nums))
                .Where(cmb => cmb.Sum() == target)
                .GroupBy(cmb => cmb.Count())
                .MinBy(g => g.Key)
                ?.Min(cmb => cmb.Select(i => (long)i).Aggregate((i, p) => i * p));
        }
        #endregion

        public override void Part1()
        {
            base.Part1();

            long? minQE = MinQE(Weights, 3);
            //var minQE = MyMinQE(Weights, 3);
            Console.WriteLine($"The quantum entanglement of the first group of packages: {minQE}\n");
        }

        #region Horror
        // In the end I made my solution
        // But at what cost...
        // It was a *really* bad idea to iterate over it
        // Note to myself: Maybe I should learn a bit more of functional programming
        private BigInteger? MyMinQE(List<int> weights, int groups)
        {
            int target = weights.Sum() / groups;
            int size, sum = 0;
            for (size = 0; sum < target; sum += Weights[size])
                size++;

            int minCount = int.MaxValue;
            BigInteger? minQE = long.MaxValue;
            foreach (var solution in Subsets(Weights, size))
            {
                if (solution.Count == 0)
                    continue;

                if (solution.Sum() == target && solution.Count <= minCount)
                {
                    minCount = solution.Count;
                    BigInteger qe = solution.Select(i => (BigInteger)i).Aggregate((i, p) => i * p);
                    if (qe < minQE)
                        minQE = qe;
                }
            }

            return minQE;
        }

        private static IEnumerable<List<int>> Subsets(List<int> fullSet, int size)
        {
            var subset = new List<int>();
            return GenerateSubsets(fullSet, 0, size, subset);
        }

        private static IEnumerable<List<int>> GenerateSubsets(List<int> fullSet, int index, int size, List<int> subset)
        {
            if (subset.Count != 0)
                yield return subset;

            if (subset.Count == size)
                yield break;

            for (int i = index;  i < fullSet.Count; i++)
            {
                subset.Add(fullSet[i]);

                foreach (var result in GenerateSubsets(fullSet, i + 1, size, subset))
                    yield return result;

                subset.RemoveAt(subset.Count - 1);
            }
        }
        #endregion

        public override void Part2()
        {
            base.Part2();

            long? minQE = MinQE(Weights, 4);
            Console.WriteLine($"The quantum entanglement of the first group of packages: {minQE}\n");
        }
    }
}
