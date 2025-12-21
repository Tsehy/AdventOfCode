namespace AdventOfCode
{
    public class _2015Day01 : _2015Day
    {
        private readonly string floorInstruction;

        public _2015Day01() : base("Day01")
        {
            floorInstruction = Input[0];
        }

        public override void Part1()
        {
            base.Part1();

            int finalFloor = floorInstruction.Sum(c => c == '(' ? 1 : -1);

            Console.WriteLine($"Final floor: {finalFloor}");
        }

        public override void Part2()
        {
            base.Part2();

            int currentFloor = 0,
                currentPosition = 1;

            foreach (char c in floorInstruction)
            {
                currentFloor += c == '(' ? 1 : -1;
                if (currentFloor == -1)
                {
                    break;
                }
                currentPosition++;
            }

            Console.WriteLine($"First time entering basement: {currentPosition}. position");
        }
    }
}