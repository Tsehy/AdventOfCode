namespace AdventOfCode
{
    internal class _2015Day09 : _2015Day
    {
        private readonly HashSet<string> cities;
        private readonly Dictionary<(string from, string to), int> distances;

        public _2015Day09() : base("Day09")
        {
            cities = [];
            distances = [];
            foreach (string line in Input)
            {
                string[] parts = line.Split(' ');
                cities.Add(parts[0]);
                cities.Add(parts[2]);
                distances.Add((parts[0], parts[2]), int.Parse(parts[4]));
                distances.Add((parts[2], parts[0]), int.Parse(parts[4]));
            }
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Length of the shortest route: {FindRoute(true)}\n");
        }

        public override void Part2()
        {
            base.Part2();

            Console.WriteLine($"Length of the longest route: {FindRoute(false)}\n");
        }

        private int FindRoute(bool cheapest)
        {
            HashSet<int> routes = [];

            // Find the full-cycles in the graph starting with from city
            // Depending on the starting city we can get different results
            foreach (string city in cities)
            {
                //defaults
                string fromCity = city;
                List<string> visitedCities = [city];
                List<int> usedDistances = [];

                while (usedDistances.Count < cities.Count - 1)
                {
                    // walk on the cheapest edge to the next vertex
                    string toCity = cheapest
                        ? distances.Where(d => d.Key.from == fromCity && !visitedCities.Contains(d.Key.to)).OrderBy(d => d.Value).First().Key.to
                        : distances.Where(d => d.Key.from == fromCity && !visitedCities.Contains(d.Key.to)).OrderByDescending(d => d.Value).First().Key.to;

                    visitedCities.Add(toCity);
                    usedDistances.Add(distances[(fromCity, toCity)]);
                    fromCity = toCity;
                }

                // finish the cycle
                usedDistances.Add(distances[(visitedCities.Last(), visitedCities.First())]);

                // remove the most expensive/cheapest edge
                routes.Add(
                    cheapest
                    ? usedDistances.OrderBy(u => u).Take(usedDistances.Count - 1).Sum()
                    : usedDistances.OrderByDescending(u => u).Take(usedDistances.Count - 1).Sum()
                );
            }

            return cheapest ? routes.Min() : routes.Max();
        }
    }
}