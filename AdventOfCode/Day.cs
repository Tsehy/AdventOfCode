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
		throw new NotImplementedException();
	}
	public virtual void Part2()
	{
		throw new NotImplementedException();
	}
}
