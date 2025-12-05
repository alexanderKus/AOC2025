namespace AOC2025.Days;

public class Day5 : InputReader, IPuzzle
{
    public int Day { get; } = 5;
    public string SolvePart1()
    {
        var data = ReadInput().Split(Environment.NewLine + Environment.NewLine);
        var ranges = data[0].Split(Environment.NewLine)
            .Select(r => r.Split('-'))
            .Select(parts => (Start: long.Parse(parts[0]), End: long.Parse(parts[1])))
            .ToArray();
        var ids = data[1].Split(Environment.NewLine).Select(long.Parse).ToArray();
        
        var c = ids.Count(id => ranges.Any(range => id >= range.Start && id <= range.End));
        return c.ToString();
    }

    public string SolvePart2()
    {
        var data = ReadInput().Split(Environment.NewLine + Environment.NewLine);
        var ranges = data[0].Split(Environment.NewLine)
            .Select(r => r.Split('-'))
            .Select(parts => (Start: long.Parse(parts[0]), End: long.Parse(parts[1])))
            .OrderBy(r => r.Start)
            .ToArray();
        List<(long Start, long End)> mergedRanges = [];
        foreach (var range in ranges)
        {
            if (mergedRanges.Count == 0)
            {
                mergedRanges.Add(range);
            }
            else
            {
                if (range.Start <= mergedRanges[^1].End)
                {
                    mergedRanges[^1] = (mergedRanges[^1].Start, Math.Max(mergedRanges[^1].End, range.End));
                }
                else
                {
                    mergedRanges.Add(range);
                }
            }
        }
        
        return mergedRanges.Sum(x => x.End - x.Start + 1).ToString();
    }
}