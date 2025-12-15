using System.Runtime.CompilerServices;
using Microsoft.Z3;

namespace AOC2025.Days;

public class Day10 : InputReader, IPuzzle
{
    public int Day { get; } = 10;
    public string SolvePart1()
    {
        var lines = ReadInputLines();
        long sum = 0;
        foreach (var line in lines)
        {
            var split = line.Split(' ');
            var target = split[0][1..^1].Select((x, i) => x == '#' ? i : -1).Where(x => x>-1).ToHashSet();
            var buttons = split[1..^1].Select(x => x[1..^1].Split(',').Select(int.Parse).ToHashSet()).ToList();
            var joltages = split[^1];
            foreach (var combo in GenerateSubsets(buttons).OrderBy(x => x.Count()))
            {
                HashSet<int> light = new();
                foreach (var button in combo)
                {
                    light.SymmetricExceptWith(button);
                }

                if (light.SetEquals(target))
                {
                    sum += combo.Count();
                    break;
                }
            }
        }
        return sum.ToString();
    }

    public string SolvePart2()
    {
        var lines = ReadInputLines();
        long sum = 0;
        using var ctx = new Context();
        foreach (var line in lines)
        {
            var split = line.Split(' ');
            var target = split[0][1..^1].Select((x, i) => x == '#' ? i : -1).Where(x => x>-1).ToHashSet();
            var buttons = split[1..^1].Select(x => x[1..^1].Split(',').Select(int.Parse).ToHashSet()).ToList();
            var joltages = split[^1][1..^1].Split(',').Select(int.Parse).ToList();
            
            var opt = ctx.MkOptimize();
            var vars = new IntExpr[buttons.Count];
            
            for (var i = 0; i < buttons.Count; i++)
            {
                vars[i] = (IntExpr)ctx.MkConst($"n{i}", ctx.IntSort);
                opt.Add(ctx.MkGe(vars[i], ctx.MkInt(0)));
            }
            
            for (var i = 0; i < joltages.Count; i++)
            {
                var terms = new List<ArithExpr>();
                for (var b = 0; b < buttons.Count; b++)
                {
                    if (buttons[b].Contains(i))
                        terms.Add(vars[b]);
                }
                ArithExpr lhs = terms.Count == 0 ? ctx.MkInt(0) : ctx.MkAdd(terms.ToArray());
                opt.Add(ctx.MkEq(lhs, ctx.MkInt(joltages[i])));
            }

            ArithExpr sumVars = vars.Length == 0 ? ctx.MkInt(0) : ctx.MkAdd(vars.Cast<ArithExpr>().ToArray());
            opt.MkMinimize(sumVars);

            if (opt.Check() == Status.SATISFIABLE)
            {
                var model = opt.Model;
                var valStr = model.Evaluate(sumVars).ToString();
                if (long.TryParse(valStr, out var val))
                    sum += val;
            }
            
        }
        return sum.ToString();
    }

    public static IEnumerable<IEnumerable<T>> GenerateSubsets<T>(IList<T> items)
    {
        return GenerateSubsetsRecursive(items, 0);
    }

    private static IEnumerable<IEnumerable<T>> GenerateSubsetsRecursive<T>(IList<T> items, int index)
    {
        if (index == items.Count)
        {
            yield return Enumerable.Empty<T>();
            yield break;
        }

        foreach (var subset in GenerateSubsetsRecursive(items, index + 1))
        {
            yield return subset;
            yield return new[] { items[index] }.Concat(subset);
        }
    }

}