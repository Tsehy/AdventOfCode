namespace AdventOfCode._2018.Day04;

internal enum EventyType
{
    ShiftStart, FallingAsleep, WakingUp,
}

internal readonly struct GuardpostEvent(DateTime time, EventyType type, int? guardId)
{
    public DateTime EventTime { get; } = time;
    public int? GuardId { get; } = guardId;
    public EventyType Type { get; } = type;
}

public class _2018Day04 : _2018Day
{
    private readonly Dictionary<int, int[]> SleepTimes = [];

    public _2018Day04() : base("Day04")
    {
        List<GuardpostEvent> events = [];

        foreach (string line in Input)
        {
            string[] parts = line.Split(' ');
            string timeString = $"{parts[0][1..]} {parts[1][..^1]}";
            DateTime time = DateTime.Parse(timeString);
            switch (parts[2])
            {
                case "Guard":
                    events.Add(new(time, EventyType.ShiftStart, int.Parse(parts[3][1..])));
                    break;

                case "wakes":
                    events.Add(new(time, EventyType.WakingUp, null));
                    break;

                case "falls":
                    events.Add(new(time, EventyType.FallingAsleep, null));
                    break;
            }
        }

        int currentGuard = 0;
        DateTime from = DateTime.Now;
        foreach (var guardpostEvent in events.OrderBy(e => e.EventTime))
        {
            if (!SleepTimes.ContainsKey(currentGuard))
                SleepTimes[currentGuard] = new int[60];

            switch (guardpostEvent.Type)
            {
                case EventyType.ShiftStart:
                    currentGuard = guardpostEvent.GuardId ?? 0;
                    break;

                case EventyType.FallingAsleep:
                    from = guardpostEvent.EventTime;
                    break;

                case EventyType.WakingUp:
                    for (int i = from.Minute; i < guardpostEvent.EventTime.Minute; i++)
                        SleepTimes[currentGuard][i]++;
                    break;
            }
        }
    }

    public override void Part1()
    {
        base.Part1();

        var guard = SleepTimes.MaxBy(vp => vp.Value.Sum());
        int maxMinute = 0;
        int maxValue = guard.Value[maxMinute];

        for (int i = 1; i < guard.Value.Length; i++)
        {
            if (guard.Value[i] > maxValue)
            {
                maxMinute = i;
                maxValue = guard.Value[i];
            }
        }

        Console.WriteLine($"Strategy 1: {guard.Key * maxMinute}");
    }

    public override void Part2()
    {
        base.Part2();

        var guard = SleepTimes.MaxBy(vp => vp.Value.Max());
        int maxMinute = 0;
        int maxValue = guard.Value[maxMinute];

        for (int i = 1; i < guard.Value.Length; i++)
        {
            if (guard.Value[i] > maxValue)
            {
                maxMinute = i;
                maxValue = guard.Value[i];
            }
        }

        Console.WriteLine($"Strategy 2: {guard.Key * maxMinute}");
    }
}
