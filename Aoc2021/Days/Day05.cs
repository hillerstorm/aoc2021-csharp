using System.Text.RegularExpressions;

namespace Aoc2021;

public class Day05 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(IEnumerable<string> input) =>
    Run(input, Part.One);

  public static int Part2(IEnumerable<string> input) =>
    Run(input, Part.Two);

  private static int Run(IEnumerable<string> input, Part part) {
    var segments = part == Part.One
      ? ParseLineSegments(input).Where(s => s.IsStraightLine)
      : ParseLineSegments(input);

    var points = new Dictionary<(int X, int Y), int>();
    foreach (var segment in segments) {
      if (segment.Start.X == segment.End.X) {
        if (segment.Start.Y <= segment.End.Y) {
          var x = segment.Start.X;
          for (var y = segment.Start.Y; y <= segment.End.Y; y++) {
            points.AddTo((x, y), 1);
          }
        } else {
          var x = segment.Start.X;
          for (var y = segment.Start.Y; y >= segment.End.Y; y--) {
            points.AddTo((x, y), 1);
          }
        }
      } else if (segment.Start.Y == segment.End.Y) {
        var y = segment.Start.Y;
        for (var x = segment.Start.X; x <= segment.End.X; x++) {
          points.AddTo((x, y), 1);
        }
      } else {
        if (segment.Start.Y <= segment.End.Y) {
          for (int x = segment.Start.X, y = segment.Start.Y; x <= segment.End.X && y <= segment.End.Y; x++, y++) {
            points.AddTo((x, y), 1);
          }
        } else {
          for (int x = segment.Start.X, y = segment.Start.Y; x <= segment.End.X && y >= segment.End.Y; x++, y--) {
            points.AddTo((x, y), 1);
          }
        }
      }
    }

    return points.Values.Count(x => x >= 2);
  }

  public class LineSegment {
    public LineSegment((int X, int Y) start, (int X, int Y) end) {
      if (start.X <= end.X) {
        Start = start;
        End = end;
      } else {
        Start = end;
        End = start;
      }
    }
    public (int X, int Y) Start { get; init; }
    public (int X, int Y) End { get; init; }
    public bool IsStraightLine => Start.X == End.X || Start.Y == End.Y;
  }

  private static readonly Regex SegmentRexp = new("^(?<x1>\\d+),(?<y1>\\d+) \\-\\> (?<x2>\\d+),(?<y2>\\d+)$");
  private static IEnumerable<LineSegment> ParseLineSegments(IEnumerable<string> input) =>
    input.Select(line => {
      var matches = SegmentRexp.Match(line);
      return new LineSegment(
        (int.Parse(matches.Groups["x1"].Value), int.Parse(matches.Groups["y1"].Value)),
        (int.Parse(matches.Groups["x2"].Value), int.Parse(matches.Groups["y2"].Value))
      );
    });
}
