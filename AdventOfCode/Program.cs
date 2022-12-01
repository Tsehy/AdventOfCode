using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Year: ");
        string? year = Console.ReadLine();
        Console.Write("Day: ");
        string? day = Console.ReadLine();
        Console.WriteLine();

        try
        {
            Day? chosenDay = CreateDay(year, day);

            chosenDay?.Part1();
            chosenDay?.Part2();
        }
        catch (Exception)
        {
            Console.WriteLine("Wrong year or day!\nPlease use format: YYYY DD\n\n(Or the day is not yet compleated)");
        }
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