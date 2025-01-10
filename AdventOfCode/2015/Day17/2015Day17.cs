using AdventOfCode._2015.Day17;

namespace AdventOfCode
{
    public class _2015Day17 : _2015Day
    {
        private readonly List<Container> Containers = [];
        private readonly List<string> FoundSolutions = [];
        private string Solutions => string.Join(' ', Containers.Select((c, i) => (c, i)).Where(g => g.c.Used).Select(g => g.i));

        public _2015Day17() : base("Day17")
        {
            foreach (string line in Input.OrderBy(c => c))
            {
                Containers.Add(new (int.Parse(line)));
            }
            Reduce(150);
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Total combinations of storing the eggnog: {FoundSolutions.Count}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int min = FoundSolutions.Min(s => s.Count(c => c == ' '));
            Console.WriteLine($"Total combinations of storing the eggnog using the least amount of containers: {FoundSolutions.Count(s => s.Count(c => c == ' ') == min)}\n");
        }

        private void Reduce(int value)
        {
            if (value == 0 && !FoundSolutions.Contains(Solutions))
            { 
                FoundSolutions.Add(Solutions);
                return;
            }
            foreach (Container container in Containers.Where(c => !c.Used && c.Size <= value))
            {
                container.Used = true;
                Reduce(value - container.Size);
                container.Used = false;
            }
        }
    }
}
