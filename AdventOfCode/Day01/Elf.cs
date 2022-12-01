public class Elf
{
    public int TotalCalorie { get; set; }

    public Elf()
    {
        TotalCalorie = 0;
    }

    public void Add(int calorie)
    {
        TotalCalorie += calorie;
    }
}
