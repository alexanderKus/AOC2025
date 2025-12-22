using Microsoft.VisualBasic.CompilerServices;

namespace AOC2025.Days;

public class Day6 : InputReader, IPuzzle
{
    public int Day { get; } = 6;
    public string SolvePart1()
    {
        var lines = ReadInputLines();
        var data = lines.Select(x => x.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray()).ToArray();
        var numbers = data[..^1].Select(x => x.Select(long.Parse).ToArray()).ToArray();
        var operations = data[^1];
        var col = numbers.Length;
        var row = numbers[0].Length;
        long sum = 0;
        for (var r = 0; r < row; r++)
        {
            var isAddition = operations[r] == "+";
            long s = isAddition ? 0 : 1;
            for (var c = 0; c < col; c++)
            {
                if (isAddition)
                {
                    s += numbers[c][r];
                }
                else
                {
                    s *= numbers[c][r];
                }
            }
            sum += s;
        }
        return sum.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines();
        long sum = 0;
        for (var i = 0; i < lines[0].Length; i++)
        {
            long subSum = 0;
            var isParsing = true;
            var isAddition = true;
            while (isParsing && i < lines[0].Length)
            {
                var n = string.Empty;
                if (lines[^1][i] != ' ')
                {
                    isAddition = lines[^1][i] == '+';
                    subSum = isAddition ? 0 : 1;
                }
                for (var j = 0; j < lines.Length - 1; j++)
                {
                    var c = lines[j][i];
                    if (char.IsNumber(c)) { n += c; }
                }

                if (string.IsNullOrEmpty(n))
                {
                    isParsing = false;
                }
                else
                {
                    subSum = isAddition ? subSum + long.Parse(n) : subSum * long.Parse(n);
                    i++;
                }
            }

            sum += subSum;
        }
        return sum.ToString();
    }
}