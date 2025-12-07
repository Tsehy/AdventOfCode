using AdventOfCode._2015.Day22;

namespace AdventOfCode
{
    public class _2015Day22 : _2015Day
    {
        private readonly int BossHealth;
        private readonly int BossAttack;

        public _2015Day22() : base("Day22")
        {
            BossHealth = int.Parse(Input[0].Split(": ")[1]);
            BossAttack = int.Parse(Input[1].Split(": ")[1]);
        }

        private int MinimumMana(bool isHardMode = false)
        {
            var open = new PriorityQueue<GameState, int>();
            open.Enqueue(new GameState(BossHealth, BossAttack, 50, 500), 0);
            int minimumMana = int.MaxValue;

            while (open.Count > 0)
            {
                var currentState = open.Dequeue();
                foreach (Spell spell in currentState.AvailableSpells())
                {
                    var nextState = currentState.CreateNextState(spell, isHardMode);
                    if (nextState.GameEnded && nextState.PlayerWon && nextState.TotalManaSpent < minimumMana)
                        minimumMana = nextState.TotalManaSpent;

                    if (nextState.OutOfMana || nextState.GameEnded || nextState.TotalManaSpent > minimumMana)
                        continue;

                    open.Enqueue(nextState, nextState.TotalManaSpent);
                }
            }

            return minimumMana;
        }

        public override void Part1()
        {
            base.Part1();

            int minMana = MinimumMana();
            Console.WriteLine($"Minimum mana needed to kill the boss: {minMana}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int minMana = MinimumMana(true);
            Console.WriteLine($"Minimum mana needed to kill the boss on hard mode: {minMana}\n");
        }
    }
}
