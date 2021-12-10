namespace Aoc2021;

public class Day06 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitAsInt(",");
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static long Part1(IEnumerable<int> ages) =>
    Run(ages, 80);

  public static long Part2(IEnumerable<int> ages) =>
    Run(ages, 256);

  public static long Run(IEnumerable<int> ages, int days) {
    var summed = ages.Aggregate(new Dictionary<int, long>(), (a, b) => a.AddTo(b, 1));

    for (var day = 0; day < days; day++) {
      var dailyCount = new Dictionary<int, long>();
      foreach (var (fish, count) in summed) {
        if (fish == 0) {
          dailyCount.AddTo(6, count);
          dailyCount.AddTo(8, count);
        } else {
          dailyCount.AddTo(fish - 1, count);
        }
      }

      summed = dailyCount;
    }

    return summed.Values.Sum();
  }
}
