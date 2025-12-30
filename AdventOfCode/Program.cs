using System.Diagnostics;
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
            var swTotal = Stopwatch.StartNew();

            try
            {
                chosenDay = CreateDay(year, day);
            }
            catch (Exception ex) when (ex.InnerException is FileNotFoundException or DirectoryNotFoundException)
            {
                Console.WriteLine($"Missing input file for {year}/{day}!");
                return;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Wrong year or day! Please use format: YYYY DD\n(Or the day is not yet compleated)");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var sw = Stopwatch.StartNew();
            chosenDay?.Part1();
            Console.WriteLine($"Enlapsed time: {sw.Elapsed}\n");
            sw = Stopwatch.StartNew();
            chosenDay?.Part2();
            Console.WriteLine($"Enlapsed time: {sw.Elapsed}\n");

            Console.WriteLine($"\nTotal time: {swTotal.Elapsed}\n----------------------------");
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