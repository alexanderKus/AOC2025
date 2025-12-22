using System.Text.RegularExpressions;

namespace AOC2025.Days;

public class Day12 : InputReader, IPuzzle
{
    public int Day { get; } = 12;
    public string SolvePart1()
    {
        var data = ReadInput().Split(Environment.NewLine+Environment.NewLine);
        var trees = data[^1].Split(Environment.NewLine);
        long sum = 0;
        Regex regex = new (@"\d+");
        foreach (var tree in trees)
        {
            var matches = regex.Matches(tree).Select(x => x.Value).Select(int.Parse).ToArray();
            var x = matches[0];
            var y = matches[1];
            var count = matches[2..].Sum();
            if (x/3 * y/3 >= count)
            {
                sum += 1;
            }
        }
        return sum.ToString();
    }

    public string SolvePart2()
    {
        // NOTE: No part 2 for this day.
        return string.Empty;
    }
}