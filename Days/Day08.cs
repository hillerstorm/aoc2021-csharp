public class Day08 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(IEnumerable<string> input) => input
    .SelectMany(x =>
      x.Split("|", StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    )
    .Count(x => x.Length is 2 or 3 or 4 or 7);

  private static readonly Dictionary<string, char> RealDigits = new() {
    { "abcefg", '0' },
    { "cf", '1' },
    { "acdeg", '2' },
    { "acdfg", '3' },
    { "bcdf", '4' },
    { "abdfg", '5' },
    { "abdefg", '6' },
    { "acf", '7' },
    { "abcdefg", '8' },
    { "abcdfg", '9' },
  };

  public static int Part2(IEnumerable<string> input) {
    IEnumerable<(string[] Patterns, string[] Message)> entries = input
      .Select(x => {
        var (patterns, message, _) = x.Split("|", StringSplitOptions.RemoveEmptyEntries);
        return (
          patterns.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(pattern => string.Concat(pattern.OrderBy(y => y)))
            .Distinct()
            .ToArray(),
          message.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(digit => string.Concat(digit.OrderBy(y => y)))
            .ToArray()
        );
      });

    var sum = 0;

    foreach (var (patterns, message) in entries) {
      var cf = patterns.Concat(message).First(x => x.Length == 2); // 1
      var acf = patterns.Concat(message).First(x => x.Length == 3); // 7
      var bcdf = patterns.Concat(message).First(x => x.Length == 4); // 4
      var abcdefg = patterns.Concat(message).First(x => x.Length == 7); // 8
      var zeroSixNine = patterns.Concat(message).Where(x => x.Length == 6).Distinct().ToArray();
      var abcdfg = zeroSixNine.First(x => x.Except(bcdf).Count() == 2); // 9
      var abcefg = zeroSixNine.Except(new[] { abcdfg }).First(x => x.Except(cf).Count() == 4); // 0
      var abdefg = zeroSixNine.Except(new[] { abcdfg, abcefg }).First(); // 6
      var a = acf.Except(cf).First();
      var d = abdefg.Except(abcefg).First();
      var b = bcdf.Except(cf).Except(new[] { d }).First();
      var c = abcefg.Except(abdefg).First();
      var e = abcdefg.Except(abcdfg).First();
      var f = cf.Except(new[] { c }).First();

      sum += int.Parse(string.Concat(message
        .Select(x => RealDigits[string.Concat(x
          .Select(y =>
            y == a
              ? 'a'
              : y == b
                ? 'b'
                : y == c
                  ? 'c'
                  : y == d
                    ? 'd'
                    : y == e
                      ? 'e'
                      : y == f
                        ? 'f'
                        : 'g'
          )
          .OrderBy(y => y))]
        )));
    }

    return sum;
  }
}
