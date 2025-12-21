using AdventOfCode._2015.Day21;

namespace AdventOfCode
{
    public class _2015Day21 : _2015Day
    {
        private readonly int LowestCost = int.MaxValue;
        private readonly int HighestCost = int.MinValue;

        public _2015Day21() : base("Day21")
        {
            int bossHealth = int.Parse(Input[0].Split(": ")[1]);
            int bossDamage = int.Parse(Input[1].Split(": ")[1]);
            int bossArmor = int.Parse(Input[2].Split(": ")[1]);

            List<Equipment> Weapons = [new Equipment(8, 4, 0), new Equipment(10, 5, 0), new Equipment(25, 6, 0), new Equipment(40, 7, 0), new Equipment(74, 8, 0)];
            List<Equipment?> Armors = [null, new Equipment(13, 0, 1), new Equipment(31, 0, 2), new Equipment(53, 0, 3), new Equipment(75, 0, 4), new Equipment(102, 0, 5)];
            List<Equipment?> Rings = [null, null, new Equipment(25, 1, 0), new Equipment(50, 2, 0), new Equipment(100, 3, 0), new Equipment(20, 0, 1), new Equipment(40, 0, 2), new Equipment(80, 0, 3)];

            var setup = new EquipmentSetup();
            foreach (Equipment weapon in Weapons)
            {
                setup.Weapon = weapon;
                foreach (Equipment? armor in Armors)
                {
                    setup.Armor = armor;
                    for (int i = 0; i < Rings.Count - 1; i++)
                    {
                        setup.LeftRing = Rings[i];
                        for (int j = i + 1; j < Rings.Count; j++)
                        {
                            setup.RightRing = Rings[j];
                            if (setup.CanWinFight(bossHealth, bossDamage, bossArmor) && LowestCost > setup.TotalPrice)
                                LowestCost = setup.TotalPrice;

                            if (!setup.CanWinFight(bossHealth, bossDamage, bossArmor) && HighestCost < setup.TotalPrice)
                                HighestCost = setup.TotalPrice;
                        }
                    }
                }
            }
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Lowest cost equipment that beats the boss: {LowestCost}");
        }

        public override void Part2()
        {
            base.Part2();

            Console.WriteLine($"Highest cost equipment that does not beats the boss: {HighestCost}");
        }
    }
}
