public class _2022Day01 : _2022Day
{
    private List<int> elves { get; set; }

    public _2022Day01() : base("Day01")
    {
        elves = new List<int>();

        ExtractData();

        elves = elves.OrderByDescending(e => e).ToList();
    }

    public override void Part1()
	{
        base.Part1();

        Console.WriteLine($"Maximum amount of calories carried: {elves[0]}\n");
	}

    public override void Part2()
	{
        base.Part2();

        int totalCalories = elves.Take(3).Sum();

        Console.WriteLine($"Maximum amount of calories carried by the top 3: {totalCalories}\n");
    }

    #region Private methods
    private void ExtractData()
    {
        int index = 0;
        elves.Add(index);

        foreach (string calorie in input)
        {
            if (calorie != "")
            {
                elves[index] += int.Parse(calorie);
            }
            else
            {
                elves.Add(0);
                index++;
            }
        }
    }
    #endregion
}
