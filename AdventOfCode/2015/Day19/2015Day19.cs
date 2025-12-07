using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public partial class _2015Day19 : _2015Day
    {
        [GeneratedRegex(@"(\w+) => (\w+)")]
        private static partial Regex ReplacementRegex();

        private readonly List<(string from, string to)> Replacements = [];
        private readonly string MedicineMolecule = "";

        public _2015Day19() : base("Day19")
        {
            foreach (string line in Input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                Match m = ReplacementRegex().Match(line);
                if (m.Success)
                {
                    Replacements.Add((m.Groups[1].Value, m.Groups[2].Value));
                }
                else
                {
                    MedicineMolecule = line;
                }
            }

            // For greedy search in Part2
            Replacements = Replacements.OrderByDescending(v => v.to.Length).ToList();
        }

        public override void Part1()
        {
            base.Part1();

            HashSet<string> created = [];
            foreach ((string from, string to) in Replacements)
            {
                for (int i = 0; i <= MedicineMolecule.Length - from.Length; i++)
                {
                    if (MedicineMolecule.Substring(i, from.Length) == from)
                    {
                        var sb = new StringBuilder(MedicineMolecule);
                        created.Add(sb.Replace(from, to, i, from.Length).ToString());
                    }
                }
            }

            Console.WriteLine($"{created.Count} different molecules can be created.\n");
        }

        private IEnumerable<string> ReducedStates(string molecule)
        {
            foreach ((string from, string to) in Replacements)
            {
                if (!molecule.Contains(to))
                    continue;

                for (int i = 0; i < molecule.Length - to.Length; i++)
                {
                    if (molecule[i..(i + to.Length)] == to)
                    {
                        yield return molecule[..i] + from + molecule[(i + to.Length)..];
                    }
                }
            }
        }

        public override void Part2()
        {
            base.Part2();

            var states = new List<string>() { MedicineMolecule };
            var visited = new HashSet<string>() { MedicineMolecule };
            int steps = 0;

            while (states.Count != 0)
            {
                var nextStates = new List<string>();

                foreach (string state in states)
                {
                    foreach (string newState in ReducedStates(state))
                    {
                        if (visited.Contains(newState))
                            continue;

                        nextStates.Add(newState);
                        visited.Add(newState);
                        break;
                    }
                }

                states = nextStates;
                steps++;
            } 

            Console.WriteLine($"Fewest number of replacements to create the medicine: {steps}\n");
        }
    }
}
