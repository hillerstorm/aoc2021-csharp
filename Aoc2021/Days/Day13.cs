using System.Text;

namespace Aoc2021;

public class Day13 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) =>
    Run(input, Part.One).Dots;

  public static string Part2(string[] input) =>
    Run(input, Part.Two).Output;

  private static (int Dots, string Output) Run(string[] input, Part part) {
    HashSet<(int X, int Y)> dots = input
      .Where(x => char.IsDigit(x[0]))
      .Select(i => {
        var (x, y, _) = i.Split(",");
        return (int.Parse(x), int.Parse(y));
      })
      .ToHashSet();

    var folds = input
      .Where(x => !char.IsDigit(x[0]))
      .Select(i => {
        var (dir, num, _) = i[11..].Split("=");
        return (dir, int.Parse(num));
      })
      .ToList();

    foreach (var (dir, num) in folds) {
      if (dir == "y")
        dots = dots
          .Where(d => d.Y < num)
          .Concat(dots.Where(d => d.Y > num).Select(d => (d.X, num - (d.Y - num))))
          .ToHashSet();
      else
        dots = dots
          .Where(d => d.X < num)
          .Concat(dots.Where(d => d.X > num).Select(d => (num - (d.X - num), d.Y)))
          .ToHashSet();

      if (part == Part.One)
        return (dots.Count, string.Empty);
    }

    var minX = dots.Min(d => d.X);
    var minY = dots.Min(d => d.Y);
    var maxX = dots.Max(d => d.X);
    var maxY = dots.Max(d => d.Y);
    var sb = new StringBuilder();
    for (var y = minY; y <= maxY; y++) {
      if (y > minY)
        sb.Append('\n');
      for (var x = minX; x <= maxX; x++)
        sb.Append(dots.Contains((x, y)) ? '█' : ' ');
    }

    return (dots.Count, sb.ToString());
  }
}
