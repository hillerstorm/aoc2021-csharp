namespace Aoc2021;

public class Day14 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static long Part1(string[] input) =>
    Run(input, Part.One);

  public static long Part2(string[] input) =>
    Run(input, Part.Two);

  private static long Run(string[] input, Part part) {
    var rules = input[1..]
      .Select(x => {
        var (pattern, chr, _) = x.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        return ((pattern[0], pattern[1]), chr[0]);
      })
      .ToDictionary(x => x.Item1, x => x.Item2);

    var counts = input[0]
      .Aggregate(
        new Dictionary<char, long>(),
        (dict, elem) =>
          dict.AddTo(elem, 1)
      );

    var steps = part == Part.One ? 10 : 40;
    var pairs = input[0].ToCharArray().TakeTwo();
    var cache = new Dictionary<((char Left, char Right) Pair, int Step), Dictionary<char, long>>();
    foreach (var pair in pairs)
      CountElements(cache, counts, rules, pair, 1, steps);

    var sorted = counts.Values.OrderBy(x => x).ToArray();
    return sorted[^1] - sorted[0];
  }

  private static void CountElements(
    IDictionary<((char Left, char Right) Pair, int Step), Dictionary<char, long>> cache,
    Dictionary<char, long> counts,
    IReadOnlyDictionary<(char Left, char Right), char> rules,
    (char Left, char Right) pair,
    int step,
    int steps
  ) {
    if (cache.TryGetValue((pair, step), out var cached)) {
      foreach (var (chr, count) in cached)
        counts.AddTo(chr, count);

      return;
    }

    var insert = rules[pair];
    var dict = new Dictionary<char, long> { { insert, 1 } };

    if (step < steps) {
      CountElements(cache, dict, rules, (pair.Left, insert), step + 1, steps);
      CountElements(cache, dict, rules, (insert, pair.Right), step + 1, steps);
    }

    cache.Add((pair, step), dict);

    foreach (var (chr, count) in dict)
      counts.AddTo(chr, count);
  }
}
