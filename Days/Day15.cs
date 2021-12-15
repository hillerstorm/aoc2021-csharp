namespace Aoc2021;

public class Day15 : IDay {
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

  private static long Run(IReadOnlyList<string> input, Part part) {
    var width = input[0].Length;
    var height = input.Count;
    var grid = input.SelectMany(x => x.Select(y => y - '0')).ToArray();

    if (part == Part.Two) {
      grid = Expand(grid, width, height);
      width *= 5;
      height *= 5;
    }

    var path = PathFinder.FindPath(
      0,
      grid.Length - 1,
      i => GetNeighbours(width, height, i),
      (goalIndex, nextIndex) => Distance(width, goalIndex, nextIndex),
      (_, i) => grid[i]
    );

    return path[1..].Sum(x => grid[x]);
  }

  private static int[] Expand(int[] input, int width, int height) {
    var list = new List<int>();
    for (var y = 0; y < height; y++)
    for (var i = 0; i < 5; i++)
      list.AddRange(
        input[(y * width)..(y * width + width)]
          .Select(n => i == 0 ? n : n + i == 9 ? 9 : (n + i) % 9)
      );

    input = list.ToArray();

    for (var i = 1; i < 5; i++)
      list.AddRange(
        input
          .Select(n => n + i == 9 ? 9 : (n + i) % 9)
      );

    return list.ToArray();
  }

  private static float Distance(int width, int goalIndex, int nextIndex) =>
    Math.Abs(goalIndex % width - goalIndex / width) + Math.Abs(nextIndex % width - nextIndex / width);

  private static IEnumerable<int> GetNeighbours(int width, int height, int index) {
    if (index % width > 0)
      yield return index - 1;
    if (index % width < width - 1)
      yield return index + 1;
    if (index >= width)
      yield return index - width;
    if (index / width < height - 1)
      yield return index + width;
  }
}
