using AdventOfCode._2015.Day15;

namespace AdventOfCode
{
    public class _2015Day15 : _2015Day
    {
        private readonly List<Ingredient> Ingredients;

        public _2015Day15() : base("Day15")
        {
            Ingredients = [];
            foreach (string line in Input)
            {
                Ingredients.Add(new Ingredient(line));
            }
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"The score of the best mix: {GetBestMix()}\n");
        }

        public override void Part2()
        {
            base.Part2();

            Console.WriteLine($"The score of the best mix with 500 calories: {GetBestMix(500)}\n");
        }

        private int GetBestMix(int? sumCalorie = null)
        {
            // brute force
            int maxValue = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; i + j < 100; j++)
                {
                    for (int k = 0; i + j + k < 100; k++)
                    {
                        int l = 100 - (i + j + k);
                        Ingredient ingredient = i * Ingredients[0] + j * Ingredients[1] + k * Ingredients[2] + l * Ingredients[3];
                        if ((int)ingredient > maxValue && (sumCalorie == null || ingredient.Calories == sumCalorie))
                        {
                            maxValue = (int)ingredient;
                        }
                    }
                }
            }

            return maxValue;
        }
    }
}
