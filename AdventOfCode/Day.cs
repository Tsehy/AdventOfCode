namespace AdventOfCode
{
    public class Day
    {
        protected string[] Input { get; set; }

        public Day(string yearName, string folderName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"..\..\..\..\..\AdventOfCodeInputs\{yearName}\{folderName}\input.txt");

            Input = File.ReadAllLines(path);
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
}