namespace AdventOfCode
{
    internal class _2022Day05 : _2022Day
    {
        private List<List<char>> crates = new();
        private readonly List<Tuple<int, int, int>> procedure = new(); // how many - from - to

        public _2022Day05() : base("Day05")
        {
            ExtractData();
        }

        public override void Part1()
        {
            base.Part1();

            // save original data, decause the algorithm ruines it
            List<List<char>> originalData = DeepCopy(crates);

            procedure.ForEach(p => { ExecuteProcedure9000(p); });
            string cratesOnTop = string.Concat(crates.Select(c => c.Last()).ToArray());

            crates = DeepCopy(originalData);

            Console.WriteLine($"The crates on top with 9000: {cratesOnTop}");
        }

        public override void Part2()
        {
            base.Part2();

            // save original data, decause the algorithm ruines it
            List<List<char>> originalData = DeepCopy(crates);

            procedure.ForEach(p => { ExecuteProcedure9001(p); });

            string cratesOnTop = string.Concat(crates.Select(c => c.Last()).ToArray());

            crates = DeepCopy(originalData);

            Console.WriteLine($"The crates on top with 9001: {cratesOnTop}");

        }

        #region Private methods
        private List<List<char>> DeepCopy(List<List<char>> data)
        {
            List<List<char>> result = new();

            foreach (var item in data)
            {
                result.Add(new List<char>(item));
            }

            return result;
        }

        private void ExtractData()
        {
            // find the separator empty line
            int end = 0;
            while (Input[end] != "")
            {
                end++;
            }

            // extract creates
            string[] tmpCrates = Input.Take(end).ToArray();

            for (int i = 0; i < tmpCrates[0].Length; i++)
            {
                // data is only in certain columns
                if (tmpCrates[end - 1][i] == ' ')
                {
                    continue;
                }

                List<char> pile = new();

                for (int height = end - 2; height >= 0; height--)
                {
                    if (tmpCrates[height][i] != ' ')
                    {
                        pile.Add(tmpCrates[height][i]);
                    }
                }

                crates.Add(pile);
            }

            // extract rearranging procedure
            for (int i = end + 1; i < Input.Length; i++)
            {
                int[] tmp = Array.ConvertAll(Input[i].Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(' '), s => int.Parse(s));
                procedure.Add(Tuple.Create(tmp[0], tmp[1], tmp[2]));
            }
        }

        private void ExecuteProcedure9000(Tuple<int, int, int> p)
        {
            int quantity = p.Item1,
                from = p.Item2 - 1,
                to = p.Item3 - 1;

            // get grates - reverse is needed for correct order
            List<char> cratesToMove = crates[from].TakeLast(quantity).Reverse().ToList();

            // delete from old position
            crates[from].RemoveRange(crates[from].Count - quantity, quantity);

            // move to new position
            crates[to].AddRange(cratesToMove);
        }

        private void ExecuteProcedure9001(Tuple<int, int, int> p)
        {
            int quantity = p.Item1,
                from = p.Item2 - 1,
                to = p.Item3 - 1;

            // get grates - reverse is NOT needed for correct order
            List<char> cratesToMove = crates[from].TakeLast(quantity).ToList();

            // delete from old position
            crates[from].RemoveRange(crates[from].Count - quantity, quantity);

            // move to new position
            crates[to].AddRange(cratesToMove);
        }
        #endregion
    }
}