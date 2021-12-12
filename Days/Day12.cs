namespace Aoc2021;

public class Day12 : IDay {
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
    var neighbours = input
      .Select(x => x.Split("-"))
      .Concat(input.Select(x => x.Split("-").Reverse().ToArray()))
      .ToLookup(x => x[0], x => x[1]);

    var start = ("start", new[] { "start" }, true);
    var queue = new Stack<(string Node, string[] VisitedSmall, bool CanVisitTwice)>();
    queue.Push(start);

    var paths = 0;

    while (queue.Count > 0) {
      var (node, visitedSmall, canVisitTwice) = queue.Pop();
      if (node == "end") {
        paths++;
        continue;
      }

      foreach (var neighbour in neighbours[node]) {
        if (!visitedSmall.Contains(neighbour)) {
          var newVisitedSmall = visitedSmall.AsEnumerable();
          if (neighbour.ToLower() == neighbour)
            newVisitedSmall = newVisitedSmall.Append(neighbour);
          queue.Push((neighbour, newVisitedSmall.ToArray(), canVisitTwice));
        } else if (part == Part.Two && canVisitTwice && neighbour != "start" && neighbour != "end") {
          queue.Push((neighbour, visitedSmall, false));
        }
      }
    }

    return paths;
  }
}
