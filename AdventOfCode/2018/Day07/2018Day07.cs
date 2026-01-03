using System.Text;

namespace AdventOfCode._2018.Day07;

internal class Node
{
    public List<char> Before { get; set; } = [];
    public List<char> After { get; set; } = [];

    public Node Copy() => new() { After = [.. After], Before = [.. Before] };
}

internal class Worker
{
    public char WorkingOn { get; set; }
    public int Timer { get; set; }

    public void Assign(char current)
    {
        WorkingOn = current;
        Timer = current - 'A' + 61;
    }

    public bool Tick()
    {
        if (Timer > 0)
        {
            Timer--;
            if (Timer == 0)
                return true;
        }
        return false;
    }
}

public class _2018Day07 : _2018Day
{
    private readonly Dictionary<char, Node> Nodes = [];

    public _2018Day07() : base("Day07")
    {
        foreach (string line in Input)
        {
            char left = line[5];
            char right = line[36];

            if (!Nodes.TryGetValue(left, out var leftNode))
                Nodes[left] = leftNode = new();
            if (!Nodes.TryGetValue(right, out var rightNode))
                Nodes[right] = rightNode = new();

            leftNode.After.Add(right);
            rightNode.Before.Add(left);
        }
    }

    public override void Part1()
    {
        base.Part1();

        var nodes = Nodes.ToDictionary(x => x.Key, x => x.Value.Copy());

        var stepOrder = new StringBuilder();
        var open = new PriorityQueue<char, char>();
        foreach (var vp in nodes.Where(vp => vp.Value.Before.Count == 0))
            open.Enqueue(vp.Key, vp.Key);

        while (open.Count > 0)
        {
            char current = open.Dequeue();
            var currentNode = nodes[current];
            stepOrder.Append(current);

            foreach (char neighbour in currentNode.After)
            {
                var neighbourNode = nodes[neighbour];
                neighbourNode.Before.Remove(current);

                if (neighbourNode.Before.Count == 0)
                    open.Enqueue(neighbour, neighbour);
            }
        }

        Console.WriteLine($"The steop order is the following: {stepOrder}");
    }

    public override void Part2()
    {
        base.Part2();

        var nodes = Nodes.ToDictionary(x => x.Key, x => x.Value.Copy());
        var workers = new Worker[5];
        for (int i = 0; i < workers.Length; i++)
            workers[i] = new();

        var open = new PriorityQueue<char, char>();
        foreach (var vp in nodes.Where(vp => vp.Value.Before.Count == 0))
            open.Enqueue(vp.Key, vp.Key);

        int timer = 0;
        while (open.Count > 0 || !workers.All(w => w.Timer == 0))
        {
            for (int i = 0; i < workers.Length; i++)
            {
                if (open.Count > 0 && workers[i].Timer == 0)
                {
                    char current = open.Dequeue();
                    workers[i].Assign(current);
                }
            }

            foreach (var worker in workers)
            {
                if (worker.Tick())
                {
                    char finished = worker.WorkingOn;
                    var finishedNode = nodes[finished];

                    foreach (char neighbour in finishedNode.After)
                    {
                        var neighbourNode = nodes[neighbour];
                        neighbourNode.Before.Remove(finished);

                        if (neighbourNode.Before.Count == 0)
                            open.Enqueue(neighbour, neighbour);
                    }
                }
            }

            timer++;
        }

        Console.WriteLine($"It takes {timer} seconds to complete all steps.");
    }
}
