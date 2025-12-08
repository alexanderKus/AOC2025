using System.Collections.Concurrent;

namespace AOC2025.Days;

public class Day7 : InputReader, IPuzzle
{
    public int Day { get; } = 7;

    public string SolvePart1()
    {
        var lines = ReadInputLines().Select(x => x.ToCharArray()).ToArray();
        var start = lines[0].IndexOf('S');
        lines[1][start] = '|';
        var counter = 0;
        for (var i = 2; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i - 1][j] == '|' && lines[i][j] == '.')
                {
                    lines[i][j] = '|';
                }
                else if (lines[i - 1][j] == '|' && lines[i][j] == '^')
                {
                    counter++;
                    if (lines[i][j - 1] == '.')
                    {
                        lines[i][j - 1] = '|';
                    }

                    if (lines[i][j + 1] == '.')
                    {
                        lines[i][j + 1] = '|';
                        j++;
                    }
                }
                
            }
        }
        return counter.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines().Select(x => x.ToCharArray()).ToArray();
        Dictionary<(int r, int c), long> visited = new();

        long Solve(int r, int c)
        {
            if (r >= lines.Length)
            {
                return 1;
            }

            if (visited.ContainsKey((r, c)))
            {
                return visited[(r, c)];
            }
            if (lines[r][c] == '.' || lines[r][c] == 'S')
            {
                var x = Solve(r + 1, c);
                visited[(r, c)] = x;
                return x;
            }
            if (lines[r][c] == '^')
            {
                var x = Solve(r, c - 1) + Solve(r, c + 1);
                visited[(r, c)] = x;
                return x;
            }
            throw new Exception("Invalid state");
        }
        
        var start = lines[0].IndexOf('S');
        return Solve(0, start).ToString();
    }
}