using AdventOfCode._2016.Day04;

namespace AdventOfCode;

public class _2016Day04 : _2016Day
{
    private readonly List<Room> Rooms;

    public _2016Day04() : base("Day04")
    {
        Rooms = [.. Input.Select(i => new Room(i))];
    }

    public override void Part1()
    {
        base.Part1();

        int realRooms = Rooms.Where(r => r.IsValid()).Sum(r => r.SectorId);

        Console.WriteLine($"Sum of the real rooms sector ids: {realRooms}");
    }

    public override void Part2()
    {
        base.Part2();

        int sectorId = Rooms.FirstOrDefault(r => r.IsValid() && r.Decrypt() == "northpole-object-storage")?.SectorId ?? 0;

        Console.WriteLine($"Sector id of the north pole objects: {sectorId}");
    }
}
