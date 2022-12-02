public class Day
{
    public string[] input { get; set; }

	public Day(string yearName, string folderName)
	{
        string path = Path.Combine(Directory.GetCurrentDirectory(), $@"..\..\..\{yearName}\{folderName}\input.txt");

        input = File.ReadAllLines(path);
	}

	public virtual void Part1()
	{
        Console.WriteLine("Part 1\n------");
    }
    public virtual void Part2()
	{
        Console.WriteLine("Part 2\n------");
    }
}
