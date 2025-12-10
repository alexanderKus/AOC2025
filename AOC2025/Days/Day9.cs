namespace AOC2025.Days;

public class Day9 : InputReader, IPuzzle
{
    public int Day { get; } = 9;
    public string SolvePart1()
    {
        var lines = ReadInputLines().Select(x => x.Split(',').Select(long.Parse).ToArray()).ToArray();

        long area = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = i + 1; j < lines.Length; j++)
            {
                long width = Math.Abs(lines[i][0] - lines[j][0]) + 1;
                long height = Math.Abs(lines[i][1] - lines[j][1]) + 1;
                area = Math.Max(area, width * height);
            }
        }

        return area.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines().Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
        List<(int x, int y)> points = lines.Select(line => (line[0], line[1])).ToList();

        // xs = sorted unique x coordinates
        var xs = points.Select(p => p.x).Distinct().OrderBy(v => v).ToList();
        var ys = points.Select(p => p.y).Distinct().OrderBy(v => v).ToList();

        int gx = xs.Count * 2 - 1;
        int gy = ys.Count * 2 - 1;

        int[,] grid = new int[gx, gy];

        // Draw lines between consecutive points (wrap around)
        for (int i = 0; i < points.Count; i++)
        {
            var p1 = points[i];
            var p2 = points[(i + 1) % points.Count];

            int cx1 = xs.IndexOf(p1.x) * 2;
            int cy1 = ys.IndexOf(p1.y) * 2;
            int cx2 = xs.IndexOf(p2.x) * 2;
            int cy2 = ys.IndexOf(p2.y) * 2;

            int minCx = Math.Min(cx1, cx2);
            int maxCx = Math.Max(cx1, cx2);
            int minCy = Math.Min(cy1, cy2);
            int maxCy = Math.Max(cy1, cy2);

            for (int cx = minCx; cx <= maxCx; cx++)
            {
                for (int cy = minCy; cy <= maxCy; cy++)
                {
                    grid[cx, cy] = 1;
                }
            }
        }

        // Flood fill from outside
        var outside = new HashSet<(int x, int y)> { (-1, -1) };
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((-1, -1));

        while (queue.Count > 0)
        {
            var (tx, ty) = queue.Dequeue();
            var neighbors = new (int x, int y)[]
            {
                (tx - 1, ty),
                (tx + 1, ty),
                (tx, ty - 1),
                (tx, ty + 1)
            };

            foreach (var (nx, ny) in neighbors)
            {
                if (nx < -1 || ny < -1 || nx > grid.GetLength(0) || ny > grid.GetLength(1))
                    continue;

                if (nx >= 0 && nx < grid.GetLength(0) &&
                    ny >= 0 && ny < grid.GetLength(1) &&
                    grid[nx, ny] == 1)
                    continue;

                if (outside.Contains((nx, ny)))
                    continue;

                outside.Add((nx, ny));
                queue.Enqueue((nx, ny));
            }
        }

        // Mark everything not reachable from outside as 1
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (!outside.Contains((x, y)))
                {
                    grid[x, y] = 1;
                }
            }
        }

        // Build 2D prefix sum array (psa)
        int[,] psa = new int[grid.GetLength(0), grid.GetLength(1)];

        for (var x = 0; x < psa.GetLength(0); x++)
        {
            for (var y = 0; y < psa.GetLength(1); y++)
            {
                int left = x > 0 ? psa[x - 1, y] : 0;
                int top = y > 0 ? psa[x, y - 1] : 0;
                int topleft = (x > 0 && y > 0) ? psa[x - 1, y - 1] : 0;
                psa[x, y] = left + top - topleft + grid[x, y];
            }
        }

        bool Valid(int x1, int y1, int x2, int y2)
        {
            int cx1 = xs.IndexOf(x1) * 2;
            int cy1 = ys.IndexOf(y1) * 2;
            int cx2 = xs.IndexOf(x2) * 2;
            int cy2 = ys.IndexOf(y2) * 2;

            int minCx = Math.Min(cx1, cx2);
            int maxCx = Math.Max(cx1, cx2);
            int minCy = Math.Min(cy1, cy2);
            int maxCy = Math.Max(cy1, cy2);

            int left = minCx > 0 ? psa[minCx - 1, maxCy] : 0;
            int top = minCy > 0 ? psa[maxCx, minCy - 1] : 0;
            int topleft = (minCx > 0 && minCy > 0) ? psa[minCx - 1, minCy - 1] : 0;

            int count = psa[maxCx, maxCy] - left - top + topleft;
            int totalCells = (maxCx - minCx + 1) * (maxCy - minCy + 1);

            return count == totalCells;
        }

        int best = 0;
        for (var i = 0; i < points.Count; i++)
        {
            var p1 = points[i];
            for (var j = 0; j < i; j++)
            {
                var p2 = points[j];
                if (Valid(p1.x, p1.y, p2.x, p2.y))
                {
                    int area = (Math.Abs(p1.x - p2.x) + 1) * (Math.Abs(p1.y - p2.y) + 1);
                    if (area > best)
                    {
                        best = area;
                    }
                }
            }
        }


        return best.ToString();
    }
}