public class Day01 : Day
{
    private List<Elf> elves { get; set; }

    public Day01() : base("Day01")
	{
		elves = new List<Elf>();

		int index = 0;
		Elf elf = new Elf();

		while (index < input.Length)
		{
            if (input[index] != "")
            {
				int calorie = int.Parse(input[index]);
				elf.Add(calorie);
            }
            else
            {
                elves.Add(elf);
                elf = new Elf();
            }

            index++;
		}

        elves = elves.OrderByDescending(e => e.TotalCalorie).ToList();
	}

    public override void Part1()
	{
		Console.WriteLine("Part 1");

        Console.WriteLine($"Maximum amount of calories carried: {elves[0].TotalCalorie}");
	}

    public override void Part2()
	{
        Console.WriteLine("Part 2");

        int totalCalories = elves.Take(3)
            .Select(e => e.TotalCalorie)
            .Sum()
        ;

        Console.WriteLine($"Maximum amount of calories carried by the top 3: {totalCalories}");
    }
}
