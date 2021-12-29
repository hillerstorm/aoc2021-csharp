namespace Aoc2021;

public class Day11 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) =>
    Run(input, Part.One);

  public static int Part2(string[] input) =>
    Run(input, Part.Two);

  private static int Run(string[] input, Part part) {
    var width = input[0].Length;
    var height = input.Length;
    var grid = string.Concat(input).Select(c => int.Parse(char.ToString(c))).ToArray();
    var flashes = 0;
    for (var step = 0; ; step++) {
      var stepFlashes = 0;
      var visited = new HashSet<int>();
      var toFlash = new Queue<int>();

      for (var i = 0; i < grid.Length; i++) {
        if (grid[i] == 9) {
          grid[i] = 0;
          toFlash.Enqueue(i);
        } else {
          grid[i] += 1;
        }
      }

      while (toFlash.Count > 0) {
        var index = toFlash.Dequeue();
        visited.Add(index);

        stepFlashes++;

        var neighbours = GetNeighbours(width, height, index % width, index / width)
          .Where(n => !visited.Contains(n) && !toFlash.Contains(n));
        foreach (var idx in neighbours) {
          if (grid[idx] == 9) {
            grid[idx] = 0;
            toFlash.Enqueue(idx);
          } else {
            grid[idx] += 1;
          }
        }
      }

      if (part == Part.Two && stepFlashes == grid.Length)
        return step + 1;

      flashes += stepFlashes;

      if (part == Part.One && step == 99)
        return flashes;
    }
  }

  private static IEnumerable<int> GetNeighbours(int width, int height, int x, int y) {
    if (x > 0) {
      yield return width * y + x - 1;
      if (y > 0)
        yield return width * (y - 1) + x - 1;
      if (y + 1 < height)
        yield return width * (y + 1) + x - 1;
    }
    if (x + 1 < width) {
      yield return width * y + x + 1;
      if (y > 0)
        yield return width * (y - 1) + x + 1;
      if (y + 1 < height)
        yield return width * (y + 1) + x + 1;
    }
    if (y > 0)
      yield return width * (y - 1) + x;
    if (y + 1 < height)
      yield return width * (y + 1) + x;
  }
}
