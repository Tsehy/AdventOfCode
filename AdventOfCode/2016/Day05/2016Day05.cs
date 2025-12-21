using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode;

public class _2016Day05 : _2016Day
{
    private readonly string DoorId;

    public _2016Day05() : base("Day05")
    {
        DoorId = Input[0];
    }

    public override void Part1()
    {
        base.Part1();

        int index = 0;
        var pass = new StringBuilder();
        while (pass.Length < 8)
        {
            byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(DoorId + index.ToString()));
            if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] < 16)
                pass.Append(bytes[2].ToString("x"));
            index++;
        }
        Console.WriteLine($"The password to the first door is: {pass}");
    }

    public override void Part2()
    {
        base.Part2();
        char[] pass = new char[8];
        int index = 0;
        while (pass.Any(c => c == '\0'))
        {
            byte[] bytes = MD5.HashData(Encoding.ASCII.GetBytes(DoorId + index.ToString()));
            if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] < pass.Length)
            {
                int i = int.Parse(bytes[2].ToString("x"));
                if (pass[i] == '\0')
                    pass[i] = bytes[3].ToString("x2")[0];
            }
            index++;
        }
        Console.WriteLine($"The password to the second door is: {string.Join("", pass)}");
    }
}
