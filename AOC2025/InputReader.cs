namespace AOC2025;

public abstract class InputReader
{
    private const string _inputPath = "./input.txt";
    
    public string ReadInput() =>
        File.ReadAllText(_inputPath);
    
    public string[] ReadInputLines() =>
        File.ReadAllLines(_inputPath);
}
