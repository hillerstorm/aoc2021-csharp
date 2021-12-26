using System.Text.RegularExpressions;

namespace Aoc2021;

public class Day22 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static long Part1(IEnumerable<string> input) {
    const int offset = 50;
    const int size = offset + offset + 1;

    var grid = Enumerable.Repeat(false, size * size * size).ToArray();

    foreach (var step in ParseSteps(input, Part.One))
      for (var z = step.ZMin + offset; z <= step.ZMax + offset; z++)
      for (var y = step.YMin + offset; y <= step.YMax + offset; y++)
      for (var x = step.XMin + offset; x <= step.XMax + offset; x++)
        grid[z * size * size + y * size + x] = step.On;

    return grid.Count(on => on);
  }

  public static long Part2(IEnumerable<string> input) {
    var steps = ParseSteps(input, Part.Two);

    var cuboids = new List<Cuboid>();

    foreach (var cuboid in steps) {
      cuboids.AddRange(cuboids
        .Select(x => GetIntersection(cuboid, x))
        .Where(x => x.XMin <= x.XMax && x.YMin <= x.YMax && x.ZMin <= x.ZMax)
        .ToList()
      );

      if (cuboid.On)
        cuboids.Add(cuboid);
    }

    return cuboids.Sum(GetCount);
  }

  private static long GetCount(Cuboid cuboid) =>
    (cuboid.XMax - cuboid.XMin + 1L) *
    (cuboid.YMax - cuboid.YMin + 1) *
    (cuboid.ZMax - cuboid.ZMin + 1) *
    (cuboid.On ? 1 : -1);

  private static Cuboid GetIntersection(
    Cuboid first,
    Cuboid second
  ) => new(
    !second.On,
    Math.Max(first.XMin, second.XMin),
    Math.Min(first.XMax, second.XMax),
    Math.Max(first.YMin, second.YMin),
    Math.Min(first.YMax, second.YMax),
    Math.Max(first.ZMin, second.ZMin),
    Math.Min(first.ZMax, second.ZMax)
  );

  private static readonly Regex ParseRegex = new(
    @"^(?<onoff>on|off) x=(?<xmin>[\-]{0,1}\d+)\.\.(?<xmax>[\-]{0,1}\d+),y=(?<ymin>[\-]{0,1}\d+)\.\.(?<ymax>[\-]{0,1}\d+),z=(?<zmin>[\-]{0,1}\d+)\.\.(?<zmax>[\-]{0,1}\d+)$"
  );

  private static IEnumerable<Cuboid> ParseSteps(
    IEnumerable<string> input,
    Part part
  ) =>
    input
      .Select(x => ParseRegex.Match(x).Groups)
      .Select(x => new Cuboid(
        x["onoff"].Value == "on",
        int.Parse(x["xmin"].Value),
        int.Parse(x["xmax"].Value),
        int.Parse(x["ymin"].Value),
        int.Parse(x["ymax"].Value),
        int.Parse(x["zmin"].Value),
        int.Parse(x["zmax"].Value)
      ))
      .Where(x =>
        part == Part.Two ||
        x.XMin >= -50 && x.XMax <= 50 && x.YMin >= -50 && x.YMax <= 50 && x.ZMin >= -50 && x.ZMax <= 50
      );

  private record struct Cuboid(bool On, int XMin, int XMax, int YMin, int YMax, int ZMin, int ZMax);
}
