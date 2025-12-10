namespace AOC2025.Days;

public class Day8  : InputReader, IPuzzle
{
    public int Day { get; } = 8;
    public string SolvePart1()
    {
        var lines = ReadInputLines()
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .ToArray();
        var result = 1;
        
        PriorityQueue<((int X, int Y, int Z) P1, (int X1, int Y1, int Z1) P2), double> queue = new();

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = i+1; j < lines.Length; j++)
            {
                var dis = Math.Sqrt(
                    Math.Pow(lines[i][0] - lines[j][0], 2) +
                    Math.Pow(lines[i][1] - lines[j][1], 2) +
                    Math.Pow(lines[i][2] - lines[j][2], 2)
                    );
                queue.Enqueue(((lines[i][0], lines[i][1], lines[i][2]), 
                    (lines[j][0], lines[j][1], lines[j][2])), 
                    dis);
            }
        }

        int c = 0;
        List<HashSet<(int X, int Y, int Z)>> circuits = new();
        while (c < 1000 && queue.Count > 0)
        {
            var (p1, p2) = queue.Dequeue();

            if (circuits.Any(x => x.Contains(p1) && x.Contains(p2)))
            {
                // do nothing
            }
            else if (circuits.Any(x => x.Contains(p1) && !x.Contains(p2))
                && circuits.Any(x => !x.Contains(p1) && x.Contains(p2)))
            {
                var pp1 = circuits.First(x => x.Contains(p1) && !x.Contains(p2));
                var pp2 = circuits.First(x => !x.Contains(p1) && x.Contains(p2));
                pp1.UnionWith(pp2);
                circuits.Remove(pp2);
            }
            else if (circuits.Any(x => x.Contains(p1)) && !circuits.Any(x => x.Contains(p2)))
            {
                circuits.First(x => x.Contains(p1)).Add(p2);
            }
            else if (!circuits.Any(x => x.Contains(p1)) && circuits.Any(x => x.Contains(p2)))
            {
                circuits.First(x => x.Contains(p2)).Add(p1);
            }
            else
            {
                circuits.Add([p1, p2]);
            }
            c++;
        }
        
        circuits.Select(x => x.Count)
            .OrderByDescending(x => x)
            .Take(3)
            .ToList()
            .ForEach(x => result *= x);
        
        return result.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines()
            .Select(x => x.Split(',').Select(long.Parse).ToArray())
            .ToArray();
        long result = 0;
        
        PriorityQueue<((long X, long Y, long Z) P1, (long X1, long Y1, long Z1) P2), double> queue = new();

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = i+1; j < lines.Length; j++)
            {
                var dis = Math.Sqrt(
                    Math.Pow(lines[i][0] - lines[j][0], 2) +
                    Math.Pow(lines[i][1] - lines[j][1], 2) +
                    Math.Pow(lines[i][2] - lines[j][2], 2)
                    );
                queue.Enqueue(((lines[i][0], lines[i][1], lines[i][2]), 
                    (lines[j][0], lines[j][1], lines[j][2])), 
                    dis);
            }
        }
        
        List<HashSet<(long X, long Y, long Z)>> circuits = new();
        while (queue.Count > 0)
        {
            var (p1, p2) = queue.Dequeue();
            var wasConnection = false;
            if (circuits.Any(x => x.Contains(p1) && x.Contains(p2)))
            {
                wasConnection = false;
            }
            else if (circuits.Any(x => x.Contains(p1) && !x.Contains(p2))
                && circuits.Any(x => !x.Contains(p1) && x.Contains(p2)))
            {
                var pp1 = circuits.First(x => x.Contains(p1) && !x.Contains(p2));
                var pp2 = circuits.First(x => !x.Contains(p1) && x.Contains(p2));
                pp1.UnionWith(pp2);
                circuits.Remove(pp2);
                wasConnection = true;
            }
            else if (circuits.Any(x => x.Contains(p1)) && !circuits.Any(x => x.Contains(p2)))
            {
                circuits.First(x => x.Contains(p1)).Add(p2);
                wasConnection = true;
            }
            else if (!circuits.Any(x => x.Contains(p1)) && circuits.Any(x => x.Contains(p2)))
            {
                circuits.First(x => x.Contains(p2)).Add(p1);
                wasConnection = true;
            }
            else
            {
                circuits.Add([p1, p2]);
                wasConnection = true;
            }

            if (circuits.Count == 1 && wasConnection)
            {
                result = p1.X * p2.X1;
            }
        }
        
        return result.ToString();
    }
}