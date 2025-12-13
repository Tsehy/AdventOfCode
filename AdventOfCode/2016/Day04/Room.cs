using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016.Day04;

public partial class Room
{
    public string EncryptedParts { get; set; }
    public int SectorId { get; set; }
    public string CheckSum { get; set; }

    public Room(string raw)
    {
        var parts = RoomRegex().Match(raw);
        EncryptedParts = parts.Groups[1].Value;
        SectorId = int.Parse(parts.Groups[2].Value);
        CheckSum = parts.Groups[3].Value;
    }

    [GeneratedRegex(@"([a-z-]+)-(\d+)\[([a-z]{5})\]")]
    private static partial Regex RoomRegex();

    public bool IsValid()
    {
        return string.Join("", EncryptedParts.Where(p => p != '-').GroupBy(p => p).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Take(5).Select(g => g.Key)) == CheckSum;
    }

    public string Decrypt()
    {
        int rotate = SectorId % 26;
        var sb = new StringBuilder();
        foreach (char part in EncryptedParts)
        {
            if (part == '-')
            {
                sb.Append(part);
            }
            else
            {
                int decrypted = part + rotate;
                if (decrypted > 'z')
                    decrypted -= 26;
                sb.Append((char)decrypted);
            }
        }
        return sb.ToString();
    }
}
