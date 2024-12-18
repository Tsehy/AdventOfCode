using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2016Day01 : _2016Day
    {
        private readonly MatchCollection instructions;

        public _2016Day01() : base("Day01")
        {
            instructions = Regex.Matches(Input[0], @"(R|L)(\d+)", RegexOptions.None);
        }

        public override void Part1()
        {
            base.Part1();

            int x = 0, y = 0, direction = 0; // 0-north 1-east 2-south 3-west
            foreach (Match match in instructions)
            {
                ProcessRotation(ref direction, match.Groups[1].Value);

                int dist = int.Parse(match.Groups[2].Value);
                switch (direction)
                {
                    case 0: // north
                        y += dist;
                        break;

                    case 1: // east
                        x += dist;
                        break;

                    case 2: // south
                        y -= dist;
                        break;

                    case 3: // west
                    default:
                        x -= dist;
                        break;
                }
            }

            Console.WriteLine($"Curent location: {x}, {y}\nDistance: {Math.Abs(x) + Math.Abs(y)}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int x = 0, y = 0, direction = 0; // 0-north 1-east 2-south 3-west
            List<string> visitedLocations = new() { "0, 0" };
            foreach (Match match in instructions)
            {
                ProcessRotation(ref direction, match.Groups[1].Value);

                int dist = int.Parse(match.Groups[2].Value);
                for (int i = 0; i < dist; i++)
                {
                    switch (direction)
                    {
                        case 0: // north
                            y++;
                            break;

                        case 1: // east
                            x++;
                            break;

                        case 2: // south
                            y--;
                            break;

                        case 3: // west
                        default:
                            x--;
                            break;
                    }

                    visitedLocations.Add($"{x}, {y}");
                }
            }

            int distance = 0;
            string location = "";
            foreach(string loc in visitedLocations)
            {
                if (visitedLocations.Count(v => v == loc) > 1)
                {
                    location = loc;
                    distance = loc.Split(", ").Select(s => Math.Abs(int.Parse(s))).Sum();
                    break;
                }
            }

            Console.WriteLine($"Visited twice: {location}\nDistance: {distance}\n");

        }

        private static void ProcessRotation(ref int direction, string turn)
        {
            direction += turn == "R" ? 1 : 3; // modulo rotation
            direction %= 4; // "rotate back"
        }
    }
}
