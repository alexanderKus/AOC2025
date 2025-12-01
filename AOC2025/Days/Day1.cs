namespace AOC2025.Days;

public class Day1 : InputReader, IPuzzle
{
    public int Day { get; } = 1;
    
    public string SolvePart1()
    {
        var combinations = ReadInputLines();
        var password = 0;
        var currentValue = 50;
        foreach (var combination in combinations)
        {
            var direction = combination[0];
            var steps = int.Parse(combination[1..]);
            currentValue = direction switch
            {
                'L' => (currentValue - steps + 100) % 100,
                'R' => (currentValue + steps) % 100,
                _ => throw new Exception("Invalid direction")
            };
            if (currentValue == 0)
            {
                password++;
            }
#if DEBUG
            Console.WriteLine($"The dial is rotated {combination} to point at {currentValue}.");
#endif
        }
        return password.ToString();
    }

    public string SolvePart2()
    {
        var combinations = ReadInputLines();
        var password = 0;
        var currentValue = 50;
        foreach (var combination in combinations)
        {
            var direction = combination[0];
            var steps = int.Parse(combination[1..]);
            for(var i = 0; i < steps; i++)
            {
                currentValue = direction switch
                {
                    'L' => (currentValue - 1 + 100) % 100,
                    'R' => (currentValue + 1) % 100,
                    _ => throw new Exception("Invalid direction")
                };
                if (currentValue == 0)
                {
                    password++;
                }
            }
#if DEBUG
            Console.WriteLine($"The dial is rotated {combination} to point at {currentValue}.");
#endif
        }
        return password.ToString();
    }
    
}