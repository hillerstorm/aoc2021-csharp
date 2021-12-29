namespace Aoc2021;

public class Day01 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitAsInt();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(int[] input) => input
    .TakeTwo()
    .Count(x => x.B > x.A);

  public static int Part2(int[] input) => input
    .TakeThree()
    .Select(x => x.A + x.B + x.C)
    .ToArray()
    .TakeTwo()
    .Count(x => x.B > x.A);
}
