using AdventOfCode._2015.Day16;

namespace AdventOfCode
{
    public class _2015Day16 : _2015Day
    {
        private readonly List<Sue> Sues = [];
        private readonly Sue MFCSAM = new(3, 7, 2, 3, 0, 0, 5, 3, 2, 1);

        public _2015Day16() : base("Day16")
        {
            foreach (string line in Input)
            {
                Sues.Add(new Sue(line));
            }
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"The gift is from Sue number {Sues.IndexOf(Sues.Where(s => s.SimilarTo(MFCSAM)).Single()) + 1}");
        }

        public override void Part2()
        {
            base.Part2();

            Console.WriteLine($"Sue number {Sues.IndexOf(Sues.Where(s => s.SimilarToReal(MFCSAM)).Single()) + 1} in the real Sue");
        }
    }
}
