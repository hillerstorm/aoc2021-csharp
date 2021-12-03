public class Day03 : IDay
{
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput)
  {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input)
  {
    var gamma = string.Empty;
    var epsilon = string.Empty;

    for (var i = 0; i < input[0].Length; i++)
    {
      var ones = 0;
      var zeros = 0;
      foreach (var line in input)
        if (line[i] == '1')
          ones++;
        else
          zeros++;

      if (ones > zeros)
      {
        gamma += "1";
        epsilon += "0";
      }
      else
      {
        gamma += "0";
        epsilon += "1";
      }
    }

    return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
  }

  public static int Part2(string[] input)
  {
    var oxygenRating = string.Empty;
    var co2Rating = string.Empty;

    IList<string> oxygenInput = input.ToList();
    IList<string> co2Input = input.ToList();
    for (var i = 0; i < input[0].Length; i++)
    {
      if (oxygenInput.Count > 1)
      {
        var (ones, zeros) = oxygenInput.Partition(x => x[i] == '1');

        oxygenInput = ones.Count >= zeros.Count
          ? ones
          : zeros;

        if (oxygenInput.Count == 1)
          oxygenRating = oxygenInput[0];
      }

      if (co2Input.Count > 1)
      {
        var (ones, zeros) = co2Input.Partition(x => x[i] == '1');

        co2Input = ones.Count < zeros.Count
          ? ones
          : zeros;

        if (co2Input.Count == 1)
          co2Rating = co2Input[0];
      }
    }

    return Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(co2Rating, 2);
  }
}
