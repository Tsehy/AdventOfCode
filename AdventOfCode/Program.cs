using System.Reflection;

namespace AdventOfCode
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Year: ");
            string? year = Console.ReadLine();
            Console.Write("Day: ");
            string? day = Console.ReadLine();
            Console.WriteLine();

            Day? chosenDay = null;

            try
            {
                chosenDay = CreateDay(year, day);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is FileNotFoundException)
                {
                    Console.WriteLine($"Missing input file for {year}/{day}!");
                }
                else
                {
                    Console.WriteLine("Wrong year or day!\nPlease use format: YYYY DD\n\n(Or the day is not yet compleated)");
                }
            }

            chosenDay?.Part1();
            chosenDay?.Part2();
        }

        #region Private methods
        private static Day? CreateDay(string? sYear, string? sDay)
        {
            return (Day?)CreateObject($"_{sYear}Day{sDay}");
        }

        private static object? CreateObject(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return Activator.CreateInstance(type);
        }
        #endregion
    }
}