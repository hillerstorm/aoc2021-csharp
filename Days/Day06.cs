public class Day06 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitAsInt(",");
    return (
      () => Part1(input.ToList()).ToString(),
      () => Part2(input.ToList()).ToString()
    );
  }

  public static int Part1(List<int> ages) {
    var toAdd = 0;
    for (var days = 0; days < 80; days++) {
      var fishes = ages.Count;
      if (toAdd > 0) {
        ages.AddRange(Enumerable.Repeat(8, toAdd));
        toAdd = 0;
      }
      for (var i = 0; i < fishes; i++) {
        var fish = ages[i];
        if (fish == 0) {
          ages[i] = 6;
        } else {
          ages[i] = fish - 1;
          if (ages[i] == 0) {
            toAdd++;
          }
        }
      }
    }

    return ages.Count;
  }

  public static int Part2(List<int> ages) =>
    -1;
}
