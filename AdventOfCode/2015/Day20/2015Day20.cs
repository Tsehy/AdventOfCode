using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class _2015Day20 : _2015Day
    {
        private readonly int InputNumber;

        public _2015Day20() : base("Day20")
        {
            InputNumber = int.TryParse(Input[0], out int value) ? value : 0;
        }

        public override void Part1()
        {
            base.Part1();

            int[] houses = new int[InputNumber];
            for (int elf = 1; elf < houses.Length; elf++)
            {
                for (int house = elf; house < houses.Length; house += elf)
                {
                    houses[house] += elf * 10;
                }

                if (houses[elf] >= InputNumber)
                {
                    Console.WriteLine($"Lowest house number: {elf}\n");
                    break;
                }
            }
        }

        public override void Part2()
        {
            base.Part2();

            int[] houses = new int[InputNumber];
            for (int elf = 1; elf < houses.Length; elf++)
            {
                int housesRemaining = 50;
                int house = elf;
                while (house < houses.Length && housesRemaining != 0)
                {
                    houses[house] += elf * 11;
                    house += elf;
                    housesRemaining--;
                }

                if (houses[elf] >= InputNumber)
                {
                    Console.WriteLine($"Lowest house number: {elf}\n");
                    break;
                }
            }
        }
    }
}
