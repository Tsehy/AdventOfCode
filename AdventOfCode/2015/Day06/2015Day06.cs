using AdventOfCode._2015.Day06;

namespace AdventOfCode
{
    public class _2015Day06 : _2015Day
    {
        private readonly List<Instruction> instructions;

        public _2015Day06() : base("Day06")
        {
            instructions = GetInstructions();
        }

        public override void Part1()
        {
            base.Part1();

            bool[,] lamps = new bool[1000, 1000];

            foreach (Instruction instr in instructions)
            {
                ExecuteInstructionPart1(lamps, instr);
            }

            int lampsOn = lamps.Cast<bool>().Count(l => l);

            Console.WriteLine($"{lampsOn} lamps are turned on.");
        }

        public override void Part2()
        {
            base.Part2();

            int[,] lamps = new int[1000, 1000];

            foreach (Instruction instr in instructions)
            {
                ExecuteInstructionPart2(lamps, instr);
            }

            int totalBrightness = lamps.Cast<int>().Sum(l => l);

            Console.WriteLine($"The total brightness is {totalBrightness}.");
        }

        #region Private methods
        private List<Instruction> GetInstructions()
        {
            return Input.Select(i => (Instruction)i).ToList();
        }

        private static void ExecuteInstructionPart1(bool[,] lamps, Instruction instr)
        {
            for (int x = instr.X1; x <= instr.X2; x++)
            {
                for (int y = instr.Y1; y <= instr.Y2; y++)
                {
                    lamps[x, y] = instr.Mode switch
                    {
                        "turn on" => true,
                        "turn off" => false,
                        _ => !lamps[x, y], // toggle
                    };
                }
            }
        }

        private static void ExecuteInstructionPart2(int[,] lamps, Instruction instr)
        {
            for (int x = instr.X1; x <= instr.X2; x++)
            {
                for (int y = instr.Y1; y <= instr.Y2; y++)
                {
                    lamps[x, y] += instr.Mode switch
                    {
                        "turn on" => 1,
                        "turn off" => lamps[x, y] != 0 ? -1 : 0, // value should'n go bellow zero
                        _ => 2, // toggle
                    };
                }
            }
        }
        #endregion
    }
}
