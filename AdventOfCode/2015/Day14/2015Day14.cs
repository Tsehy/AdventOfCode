using AdventOfCode._2015.Day14;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day14 : _2015Day
    {
        private readonly List<Reindeer> Reindeers;

        public _2015Day14() : base("Day14")
        {
            Reindeers = [];
            var a = new Regex(@"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.");

            foreach (string line in Input)
            {
                Match m = a.Match(line);
                Reindeers.Add((int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)));
            }
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Distance dome by the winning reindeer: {GetMaxDistanceAt(2503)}km\n");
        }

        public override void Part2()
        {
            base.Part2();

            int[] scores = new int[Reindeers.Count];
            for (int time = 1; time < 2503; time++)
            {
                foreach (int leader in LeadingReindeerIndexes(time))
                {
                    scores[leader]++;
                }
            }

            Console.WriteLine($"Winning reinder's score: {scores.Max()}\n");
        }

        private int GetMaxDistanceAt(int time) => Reindeers.Select(r => r.DistanceAt(time)).Max();

        private IEnumerable<int> LeadingReindeerIndexes(int time) => Reindeers.Select((r, index) => (distance: r.DistanceAt(time), index)).Where(g => g.distance == GetMaxDistanceAt(time)).Select(g => g.index);
    }
}
