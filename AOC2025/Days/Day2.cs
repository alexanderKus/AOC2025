using System.Diagnostics;

namespace AOC2025.Days;

public class Day2 : InputReader, IPuzzle
{
    public int Day { get; } = 2;
    public string SolvePart1()
    {
        long sum = 0;
        var data = ReadInput();
        var ranges = data.Split(',').Select(x => x.Split('-').Select(long.Parse).ToArray()).ToArray();
        foreach (var range in ranges)
        {
            long start = range[0];
            long end = range[1];
            for (long i = start; i <= end; i++)
            {
                var number = i.ToString();
                if (number.Length % 2 != 0)
                {
                    continue;
                }
                int middle = number.Length / 2;
                if (number[..middle] == number[middle..])
                {
                    sum += i; 
#if DEBUG
                    Console.WriteLine($"{start}-{end} has one invalid ID, {i}.");
#endif
                }
            }
        }

        return sum.ToString();
    }

    public string SolvePart2()
    {
        long sum = 0;
        var data = ReadInput();
        var ranges = data.Split(',').Select(x => x.Split('-').Select(long.Parse).ToArray()).ToArray();
        foreach (var range in ranges)
        {
            long start = range[0];
            long end = range[1];
            for (long i = start; i <= end; i++)
            {
                var number = i.ToString();
                for (var j = 1; j < number.Length; j++)
                {
                    if (number.Length % j != 0)
                    {
                        continue;
                    }
                    List<string> parts = [];
                    for(var k = 0; k+j <= number.Length; k+=j)
                    {
                        parts.Add(number[k..(k+j)]);
                    }
                    
                    var valid = true;
                    for (var k = 1; k < parts.Count; k++)
                    {
                        if (!parts[0].Equals(parts[k]))
                        {
                            valid = false;
                            break;
                        }
                    }
                    
                    if (valid)
                    {
                        sum += i;
#if DEBUG
                        Console.WriteLine($"{start}-{end} has one invalid ID, {i}.");
#endif
                        break;
                    }
                }
            }
        }

        return sum.ToString();
    }
}