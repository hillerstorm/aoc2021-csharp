namespace Aoc2021;

public class Day09 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) {
    var width = input[0].Length;
    var height = input.Length;
    var grid = string.Concat(input).Select(x => int.Parse(char.ToString(x))).ToArray();
    var sum = 0;
    for (var x = 0; x < width; x++)
    for (var y = 0; y < height; y++) {
      var cell = grid[width * y + x];
      var neighbours = GetNeighbours(grid, width, height, x, y);
      if (neighbours.All(n => n.Height > cell))
        sum += cell + 1;
    }
    return sum;
  }

  public static int Part2(string[] input) {
    var width = input[0].Length;
    var height = input.Length;
    var grid = string.Concat(input).Select(x => int.Parse(char.ToString(x))).ToArray();
    var basins = new List<int>();
    for (var x = 0; x < width; x++)
    for (var y = 0; y < height; y++) {
      var cell = grid[width * y + x];
      var neighbours = GetNeighbours(grid, width, height, x, y).ToArray();
      if (!neighbours.All(n => n.Height > cell)) continue;

      neighbours = neighbours.Where(n => n.Height != 9).ToArray();
      var basin = new List<(int X, int Y, int Height)> { (x, y, cell) };
      basin.AddRange(neighbours);
      while (neighbours.Length > 0) {
        neighbours = neighbours
          .SelectMany(n => GetNeighbours(grid, width, height, n.X, n.Y))
          .Distinct()
          .Where(n => !basin.Any(b => b.X == n.X && b.Y == n.Y) && n.Height != 9)
          .ToArray();
        basin.AddRange(neighbours);
      }

      basins.Add(basin.Count);
    }

    return basins
      .OrderByDescending(x => x)
      .Take(3)
      .Aggregate(1, (a, b) => a * b);
  }

  private static IEnumerable<(int X, int Y, int Height)> GetNeighbours(
    IReadOnlyList<int> grid, int width, int height, int x, int y
  ) {
    if (x > 0)
      yield return (x - 1, y, grid[width * y + x - 1]);
    if (x + 1 < width)
      yield return (x + 1, y, grid[width * y + x + 1]);
    if (y > 0)
      yield return (x, y - 1, grid[width * (y - 1) + x]);
    if (y + 1 < height)
      yield return (x, y + 1, grid[width * (y + 1) + x]);
  }
}
