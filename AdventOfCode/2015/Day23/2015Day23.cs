using System.Numerics;

namespace AdventOfCode
{
    public class _2015Day23 : _2015Day
    {
        public _2015Day23() : base("Day23")
        {
        }

        private void ExecuteInstructions(Dictionary<string, BigInteger> registers)
        {
            int head = 0;
            while (head >= 0 && head < Input.Length)
            {
                string[] instruction = Input[head].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
                switch (instruction[0])
                {
                    case "hlf":
                        registers[instruction[1]] /= 2;
                        head++;
                        break;

                    case "tpl":
                        registers[instruction[1]] *= 3;
                        head++;
                        break;

                    case "inc":
                        registers[instruction[1]]++;
                        head++;
                        break;

                    case "jmp":
                        head += int.Parse(instruction[1]);
                        break;

                    case "jie":
                        if (registers[instruction[1]] % 2 == 0)
                            head += int.Parse(instruction[2]);
                        else
                            head++;
                        break;

                    case "jio":
                        if (registers[instruction[1]] == 1)
                            head += int.Parse(instruction[2]);
                        else
                            head++;
                        break;
                }
            }
        }

        public override void Part1()
        {
            base.Part1();

            var registers = new Dictionary<string, BigInteger> { { "a", 0 }, { "b", 0 } };
            ExecuteInstructions(registers);

            Console.WriteLine($"Value in register 'b': {registers["b"]}");
        }

        public override void Part2()
        {
            base.Part2();

            var registers = new Dictionary<string, BigInteger> { { "a", 1 }, { "b", 0 } };
            ExecuteInstructions(registers);

            Console.WriteLine($"Value in register 'b': {registers["b"]}");
        }
    }
}
