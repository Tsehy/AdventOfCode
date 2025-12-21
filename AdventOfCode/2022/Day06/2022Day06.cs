namespace AdventOfCode
{
    internal class _2022Day06 : _2022Day
    {
        private readonly string message;

        public _2022Day06() : base("Day06")
        {
            message = Input[0];
        }

        public override void Part1()
        {
            base.Part1();

            int marker = GetMarkerIndex(4);

            Console.WriteLine($"The short marker appears after {marker} characters.");
        }

        public override void Part2()
        {
            base.Part2();

            int marker = GetMarkerIndex(14);

            Console.WriteLine($"The long marker appears after {marker} characters.");

        }

        #region Private methods
        private int GetMarkerIndex(int markerLength)
        {
            for (int i = 0; i < message.Length - markerLength + 1; i++)
            {
                string tmp = message.Substring(i, markerLength);
                string tmpDistinct = new(tmp.Distinct().ToArray());
                if (tmp == tmpDistinct)
                {
                    return i + markerLength; //shift to the end
                }
            }

            return -1;
        }
        #endregion
    }
}