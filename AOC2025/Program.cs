using AOC2025;

var puzzleRegistry = new PuzzleRegistry();

var dayNumber = int.Parse(args[0]);
var solver = puzzleRegistry.GetPuzzle(dayNumber)!;
var solution1 = solver.SolvePart1();
var solution2 = solver.SolvePart2();

Console.WriteLine(solution1);
Console.WriteLine(solution2);
