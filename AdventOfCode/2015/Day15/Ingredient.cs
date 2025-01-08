using System.Text.RegularExpressions;

namespace AdventOfCode._2015.Day15
{
    public readonly partial record struct Ingredient(int Capacity, int Durability, int Flavor, int Texture, int Calories)
    {
        // First time I'm doing constructors like these
        // It's a fun experiment :)
        public Ingredient(List<int> list) : this(list[0], list[1], list[2], list[3], list[4])
        {
        }

        // This already has the RegexOptions.Compiled
        [GeneratedRegex(@"-?\d+")]
        private static partial Regex NumberPattern();

        public Ingredient(string str) : this(NumberPattern().Matches(str).Select(m => int.Parse(m.Value)).ToList())
        {
        }

        public static explicit operator int(Ingredient i)
            => Math.Max(0, i.Capacity) * Math.Max(0, i.Durability) * Math.Max(0, i.Flavor) * Math.Max(0, i.Texture);

        // Also first time defining custom operators
        public static Ingredient operator +(Ingredient left, Ingredient right)
            => new(left.Capacity + right.Capacity, left.Durability + right.Durability, left.Flavor + right.Flavor, left.Texture + right.Texture, left.Calories + right.Calories);

        public static Ingredient operator *(int n, Ingredient i)
            => new(n * i.Capacity, n * i.Durability, n * i.Flavor, n * i.Texture, n * i.Calories);

    }
}
