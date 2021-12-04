public class Day02 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(IEnumerable<string> input) =>
    Parse(input, Part.One);

  public static int Part2(IEnumerable<string> input) =>
    Parse(input, Part.Two);

  private static int Parse(IEnumerable<string> input, Part part) {
    var depth = 0;
    var pos = 0;
    var aim = 0;
    foreach (var line in input)
      switch (line[0]) {
        case 'f':
          var amount = int.Parse(line[8..]);
          pos += amount;
          depth += aim * amount;
          break;
        case 'd':
          aim += int.Parse(line[5..]);
          break;
        case 'u':
          aim -= int.Parse(line[3..]);
          break;
      }

    return part == Part.One
      ? pos * aim
      : pos * depth;
  }
}
