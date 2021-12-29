namespace Aoc2021;

public class Day24 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string _) {
    return (
      () => Part1().ToString(),
      () => Part2().ToString()
    );
  }

  public static long Part1() {
    if (result == default) Run();
    return result.max;
  }

  public static long Part2() {
    if (result == default) Run();
    return result.min;
  }

  private static (long min, long max) result;

  public static void Run() {
    var states = new List<(long min, long max, (long W, long X, long Y, long Z) memory)> {
      (0L, 0L, (0L, 0L, 0L, 0L))
    };

    foreach (var (instruction, reset, i) in Day24Input.Instructions.Zip(Day24Input.Resets, Enumerable.Range(1, Day24Input.Instructions.Length))) {
      Console.WriteLine($"Running block {i}");

      Console.WriteLine($"States before dedupe: {states.Count}");
      states = states
        .Select(x => (x.min, x.max, memory: reset(x.memory)))
        .GroupBy(x => x.memory)
        .Select(x => (min: x.Min(y => y.min), max: x.Max(y => y.max), memory: x.Key))
        .ToHashSet()
        .ToList();
      Console.WriteLine($"States after dedupe: {states.Count}");

      states = Enumerable.Range(1, 9)
        .SelectMany(i => states
          .Select(x =>
            (x.min * 10 + i, x.max * 10 + i, instruction(x.memory, i))
          )
        )
        .ToList();
      Console.WriteLine($"States after expansion: {states.Count}");
    }

    result = states
      .Where(x => x.memory.Z == 0)
      .Select(x => (x.min, x.max))
      .Aggregate((a, b) => (Math.Min(a.min, b.min), Math.Max(a.max, b.max)));
  }
}
