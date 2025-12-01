using System.Reflection;

namespace AOC2025;

public class PuzzleRegistry
{
    private readonly IReadOnlyList<IPuzzle> _puzzles = new List<IPuzzle>(
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IPuzzle).IsAssignableFrom(t))
            .Select(t => (IPuzzle)Activator.CreateInstance(t)!)
    );
    
    public IPuzzle? GetPuzzle(int day) =>
        _puzzles.FirstOrDefault(p => p.Day == day);
}