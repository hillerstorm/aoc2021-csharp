public class Day07 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitAsInt(",");
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(int[] input) =>
    Run(input, Part.One);

  public static int Part2(int[] input) =>
    Run(input, Part.Two);

  public static int Run(int[] input, Part part)
  {
    var minFuel = int.MaxValue;
    var min = input.Min();
    var max = input.Max();
    for (var i = min; i <= max; i++)
    {
      var cost = 0;
      foreach (var crab in input)
      {
        var dist = Math.Abs(crab - i);
        if (part == Part.One)
          cost += dist;
        else
          while (dist > 0)
            cost += dist--;
      }
      if (cost < minFuel)
        minFuel = cost;
    }

    return minFuel;
  }
}
