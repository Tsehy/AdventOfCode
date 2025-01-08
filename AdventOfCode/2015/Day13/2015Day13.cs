using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day13 : _2015Day
    {
        private readonly HashSet<string> persons;
        private readonly Dictionary<(string from, string to), int> pairings;
        private readonly string first;

        public _2015Day13() : base("Day13")
        {
            Regex pattern = new Regex(@"(\w+) would (lose|gain) (\d+) happiness units by sitting next to (\w+).");
            persons = [];
            pairings = [];
            foreach (string line in Input)
            {
                Match match = pattern.Match(line);
                string personA = match.Groups[1].Value;
                string personB = match.Groups[4].Value;

                persons.Add(personA);
                persons.Add(personB);

                // simplify the input to get something similar to 2015Day09
                KeyValuePair<(string from, string to), int> k1 = new((personA, personB), match.Groups[2].Value == "lose" ? -int.Parse(match.Groups[3].Value) : int.Parse(match.Groups[3].Value));
                KeyValuePair<(string from, string to), int> k2 = new((personB, personA), k1.Value);
                
                if (pairings.ContainsKey(k1.Key))
                {
                    pairings[k1.Key] += k1.Value;
                }
                else
                {
                    pairings.Add(k1.Key, k1.Value);
                }

                if (pairings.ContainsKey(k2.Key))
                {
                    pairings[k2.Key] += k2.Value;
                }
                else
                {
                    pairings.Add(k2.Key, k2.Value);
                }
            }

            first = persons.First();
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Total change in happines with the best seating: {GetMaxSeating()}\n");
        }

        public override void Part2()
        {
            base.Part2();

            Console.WriteLine($"Total change in happines with the best seating including *me*: {GetMaxSeating(true)}\n");
        }

        // Held-Karp algorithm
        // because the neares neighbour algo from day09 is bad in this situation
        // https://en.wikipedia.org/wiki/Travelling_salesman_problem#cite_note-31
        public int GetMaxSeating(bool withMe = false)
        {
            var personsList = persons.ToList();
            personsList.Remove(first);

            int maxValue = int.MinValue;
            foreach (string last in personsList)
            {
                List<string> seating = GetSeating(personsList, last);
                seating.Insert(0, first);
                maxValue = Math.Max(maxValue, GetValue(seating, withMe));
            }

            return maxValue;
        }

        private List<string> GetSeating(List<string> personsList, string last, bool withMe = false)
        {
            var list = new List<string>(personsList);
            list.Remove(last);

            var seating = new List<string>();
            if (list.Count == 0)
            {
                seating.Add(last);
            }
            else if (list.Count == 1)
            {
                seating.Add(list[0]);
                seating.Add(last);
            }
            else if (list.Count == 2)
            {
                if (pairings[(first, list[0])] + pairings[(list[0], list[1])] + pairings[(list[1], last)] > pairings[(first, list[1])] + pairings[(list[1], list[0])] + pairings[(list[0], last)])
                {
                    seating.Add(list[0]);
                    seating.Add(list[1]);
                }
                else
                {
                    seating.Add(list[1]);
                    seating.Add(list[0]);
                }
                seating.Add(last);
            }
            else
            {
                int maxValue = int.MinValue;
                var maxList = new List<string>();
                foreach (string person in list)
                {
                    List<string> partialSeating = GetSeating(list, person);
                    partialSeating.Add(last);
                    int v = GetValue(partialSeating, withMe);
                    if (v > maxValue)
                    {
                        maxValue = v;
                        maxList = partialSeating;
                    }
                }

                seating.AddRange(maxList);
            }

            return seating;
        }

        private int GetValue(List<string> list, bool withMe = false)
        {
            List<int> usedPairings = [];
            for (int i = 0; i < list.Count - 1; i++)
            {
                usedPairings.Add(pairings[(list[i], list[i + 1])]);
            }
            usedPairings.Add(pairings[(list[^1], list[0])]);
            return usedPairings.OrderByDescending(v => v).Take(usedPairings.Count - (withMe ? 1 : 0)).Sum();
        }
    }
}
