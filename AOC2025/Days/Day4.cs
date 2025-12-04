namespace AOC2025.Days;

public class Day4 : InputReader, IPuzzle
{
    public int Day { get; } = 4;
    public string SolvePart1()
    {
        var lines = ReadInputLines();
        var count = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            var len = lines[0].Length;
            for (var j = 0; j < len; j++)
            {
                if (lines[i][j] == '.')
                {
                    continue;
                }
                var n = 0;
                for (var k = -1; k <= 1; k++)
                {
                    for (var l = -1; l <= 1; l++)
                    {
                        if (k == 0 && l == 0)
                        {
                            continue;
                        }
                        if (i + k >= 0 && i + k < lines.Length && j + l >= 0 && j + l < len && lines[i + k][j + l] == '@')
                        {
                            n++;
                        }
                    }
                }

                if (n < 4)
                {
                    count++;
                }
            }
        }
        return count.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines().Select(x => x.ToCharArray()).ToArray();
        var count = 0;
        var hasRemoved = true;
#if DEBUG
        var iter = 1;
#endif
        while (hasRemoved)
        {
            hasRemoved = false;
            for (var i = 0; i < lines.Length; i++)
            {
                var len = lines[0].Length;
                for (var j = 0; j < len; j++)
                {
                    if (lines[i][j] == '.')
                    {
                        continue;
                    }
                    var n = 0;
                    for (var k = -1; k <= 1; k++)
                    {
                        for (var l = -1; l <= 1; l++)
                        {
                            if (k == 0 && l == 0)
                            {
                                continue;
                            }
                            if (i + k >= 0 && i + k < lines.Length && j + l >= 0 && j + l < len && lines[i + k][j + l] == '@')
                            {
                                n++;
                            }
                        }
                    }

                    if (n < 4)
                    {
                        count++;
                        hasRemoved = true;
                        lines[i][j] = '.';
                    }
                }
            }
#if DEBUG
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.BufferWidth * Console.BufferHeight));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Iteration {iter}: {count} removed.");
            foreach (var line in lines)
            {
                Console.WriteLine(new string(line));
            }
            iter++;
            Thread.Sleep(200);
#endif
        }
        return count.ToString();
    }
}