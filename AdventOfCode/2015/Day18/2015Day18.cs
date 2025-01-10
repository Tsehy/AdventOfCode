using AdventOfCode._2015.Day18;

namespace AdventOfCode
{
    public class _2015Day18 : _2015Day
    {
        private readonly List<Point> Grid = [];

        public _2015Day18() : base("Day18")
        {
        }

        #region Init
        private void Init()
        {
            Grid.Clear();

            for (int row = 0; row < Input.Length; row++)
            {
                for (int col = 0; col < Input[row].Length; col++)
                {
                    Grid.Add(new Point(row, col, Input[row][col] == '#'));
                }
            }

            // whire connections
            foreach (Point point in Grid)
            {
                point.Neighbours = Grid
                .Where(p =>
                    p.Row >= point.Row - 1
                    && p.Row <= point.Row + 1
                    && p.Column >= point.Column - 1
                    && p.Column <= point.Column + 1
                    && p != point
                ).ToList();
            }
        }
        #endregion

        public override void Part1()
        {
            base.Part1();

            Init();

            for (int i = 0; i < 100; i++)
            {
                Iterate();
            }

            Console.WriteLine($"{Grid.Count(p => p.State)} lamps are on\n");
        }

        public override void Part2()
        {
            base.Part2();

            Init();

            // fix corners
            foreach (Point point in Grid.Where(p => (p.Row == 0 || p.Row == 99) && (p.Column == 0 || p.Column == 99)))
            {
                point.State = true;
                point.NextState = true;
            }

            for (int i = 0; i < 100; i++)
            {
                Iterate(true);
            }

            Console.WriteLine($"{Grid.Count(p => p.State)} lamps are on (with fixed corners)\n");
        }

        private void Iterate(bool cornerCheck = false)
        {
            foreach (Point point in Grid)
            {
                int neighbours = point.Neighbours.Count(n => n.State);

                if (cornerCheck && (point.Row == 0 || point.Row == 99) && (point.Column == 0 || point.Column == 99))
                {
                    continue;
                }

                point.NextState = point.State
                    ? (neighbours >= 2 && neighbours <= 3) // point is ON  => if the neighbours are between 2 and 3 it stays ON
                    : (neighbours == 3);                   // point is OFF => if the neighbours is 3 it turns ON
            }

            Grid.ForEach(p => { p.State = p.NextState; });
        }
    }
}
