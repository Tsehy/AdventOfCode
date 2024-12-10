namespace AdventOfCode
{
    public class _2015Day02 : _2015Day
    {
        private readonly List<(int length, int width, int height)> boxes;

        public _2015Day02() : base("Day02")
        {
            boxes = new List<(int length, int width, int height)>();

            ExtractData();
        }

        public override void Part1()
        {
            base.Part1();

            int totalWrapping = 0;
            foreach (var (length, width, height) in boxes)
            {
                int side1 = length * width,
                    side2 = length * height,
                    side3 = width * height;

                totalWrapping += 2 * (side1 + side2 + side3) + Math.Min(Math.Min(side1, side2), side3);
            }

            Console.WriteLine($"Total area of wrapping paper: {totalWrapping} square feet\n");
        }

        public override void Part2()
        {
            base.Part2();

            int totalRibbon = 0;
            foreach (var (length, width, height) in boxes)
            {
                int ribbonForBox = 2 * (length + width + height - Math.Max(Math.Max(length, width), height)),
                    ribbonForBow = length * width * height;

                totalRibbon += ribbonForBow + ribbonForBox;
            }

            Console.WriteLine($"Total length of the ribbon: {totalRibbon} feet\n");
        }

        #region Private methods
        private void ExtractData()
        {
            foreach (string data in Input)
            {
                var dataParts = data.Split('x');

                boxes.Add((
                    int.Parse(dataParts[0]),
                    int.Parse(dataParts[1]),
                    int.Parse(dataParts[2])
                ));
            }
        }
        #endregion
    }
}