namespace AdventOfCode._2015.Day14
{
    public readonly record struct Reindeer(int Speed, int FlyDuration, int RestDuration)
    {
        public static implicit operator (int, int, int)(Reindeer r) => (r.Speed, r.FlyDuration, r.RestDuration);
        public static implicit operator Reindeer((int, int, int) t) => new(t.Item1, t.Item2, t.Item3);
        public int DistanceAt(int time) => (Math.DivRem(time, FlyDuration + RestDuration, out int rem) * FlyDuration + Math.Min(rem, FlyDuration)) * Speed;
    }
}
