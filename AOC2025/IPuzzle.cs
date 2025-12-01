namespace AOC2025;

public interface IPuzzle
{
    int Day { get; }
    string SolvePart1();
    string SolvePart2();
}