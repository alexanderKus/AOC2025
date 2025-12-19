namespace AOC2025.Days;

public class Day11 : InputReader, IPuzzle
{
    public int Day { get; } = 11;
    public string SolvePart1()
    {
        var lines = ReadInputLines();
        long sum = 0;
        Dictionary<string,List<string>> graph = [];
        
        foreach(var line in lines)
        {
            var parts = line.Split(' ');
            var inNode = parts[0][..^1];
            var outNodes = parts[1..].ToList();
            graph[inNode] = outNodes;
        }
        
        Queue<List<string>> queue = new();
        queue.Enqueue(graph["you"]);
        while (queue.Count > 0)
        {
            var outNodes = queue.Dequeue();
            foreach (var outNode in outNodes)
            {
                if (outNode == "out")
                {
                    sum++;
                }
                else
                {
                    queue.Enqueue(graph[outNode]);
                }
            }
        }
        
        return sum.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines();
        long sum = 0;
        Dictionary<string,List<string>> graph = [];
        
        foreach(var line in lines)
        {
            var parts = line.Split(' ');
            var inNode = parts[0][..^1];
            var outNodes = parts[1..].ToList();
            graph[inNode] = outNodes;
        }

        Dictionary<string, Dictionary<string, long>> memoPerTarget = [];

        long Count(string from, string to)
        {
            if (!memoPerTarget.TryGetValue(to, out var memo))
            {
                memo = new Dictionary<string, long>();
                memoPerTarget[to] = memo;
            }
            
            long Dfs(string node)
            {
                if (node == to) return 1;
                if (memo.TryGetValue(node, out var cached)) return cached;
                long total = 0;
                if (graph.TryGetValue(node, out var neighbors))
                {
                    foreach (var n in neighbors)
                    {
                        total += Dfs(n);
                    }
                }
                memo[node] = total;
                return total;
            }

            long result = 0;
            if (graph.TryGetValue(from, out var startNeighbors))
            {
                foreach (var n in startNeighbors)
                {
                    result += Dfs(n);
                }
            }
            return result;
        }

        sum = Count("svr", "fft") * Count("fft", "dac") * Count("dac", "out") 
            + Count("svr", "dac") * Count("dac", "fft") * Count("fft", "out");
        
        return sum.ToString();
    }
}