using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day03 : _2015Day
    {
        public readonly string instructions;

        public _2015Day03() : base("Day03")
        {
            instructions = Input[0];
        }

        public override void Part1()
        {
            base.Part1();

            HashSet<string> visitedLocations = new();
            int currentX = 0, currentY = 0;

            foreach (char instruction in instructions)
            {
                MoveSanta(ref currentX, ref currentY, instruction);

                visitedLocations.Add($"{currentX}-{currentY}");
            }

            Console.WriteLine($"{visitedLocations.Count} number of houses were visited");
        }

        public override void Part2()
        {
            base.Part2();

            // matvhes an empty string that has the last match folloved by two characters
            var instructionPairs = Regex.Split(instructions, @"(?<=\G..)");

            HashSet<string> visitedLocations = new();
            int santaX = 0, santaY = 0, robotX = 0, robotY = 0;

            foreach (string instructionPair in instructionPairs)
            {
                if (instructionPair == "")
                {
                    break; // last pair is an empty string
                }

                MoveSanta(ref santaX, ref santaY, instructionPair[0]);
                MoveSanta(ref robotX, ref robotY, instructionPair[1]);

                visitedLocations.Add($"{santaX}-{santaY}");
                visitedLocations.Add($"{robotX}-{robotY}");
            }

            Console.WriteLine($"{visitedLocations.Count} number of houses were visited");
        }

        #region Private methods
        private static void MoveSanta(ref int currentX, ref int currentY, char instruction)
        {
            switch (instruction)
            {
                case '^':
                    currentY++;
                    break;
                case 'v':
                    currentY--;
                    break;
                case '>':
                    currentX++;
                    break;
                default:
                    currentX--;
                    break;
            }
        }
        #endregion
    }
}