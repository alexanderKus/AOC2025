namespace AOC2025.Days;

public class Day3 : InputReader, IPuzzle
{
    public int Day { get; } = 3;
    public string SolvePart1()
    {
        var lines = ReadInputLines();
        long sum = 0;
        foreach (var line in lines)
        {
            long number = 0;
            for (var i = 0; i < line.Length - 1; i++)
            {
                for (var j = i + 1; j < line.Length; j++)
                {
                    number = Math.Max(number, long.Parse($"{line[i]}{line[j]}"));
                }
            }
#if DEBUG
            Console.WriteLine($"For {line} the largest joltage you can produce is {number}");
#endif
            sum += number;
        }

        return sum.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines();
        long sum = 0;
        foreach (var line in lines)
        {
            var number = string.Empty;
            var l = 0;
            for (var r = line.Length - 11; r <= line.Length; r++)
            {
                var value = '0';
                var index = 0;
                for (var i = l; i < r; i++)
                {
                    if (line[i] > value)
                    {
                        value = line[i];
                        index = i;
                    }
                }
                number += value;
                l = index + 1;
            }
#if DEBUG
            Console.WriteLine($"For {line} the largest joltage you can produce is {number}");
#endif
            sum += long.Parse(number);
        }

        return sum.ToString();
    }
}