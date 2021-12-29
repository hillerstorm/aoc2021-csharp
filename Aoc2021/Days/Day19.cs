using System.Numerics;

namespace Aoc2021;

public class Day19 : IDay {
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

  private static int Run(IReadOnlyList<string> input, Part part) {
    var parsedScanners = ParseScanners(input);

    var beacons = CalculateDistances(
      Enumerable.Empty<(Vector3, (Vector3, Vector3, float)[])>(),
      parsedScanners[0]
    );

    var scanners = new List<Vector3> { Vector3.Zero };

    var queue = new Queue<List<Vector3>>(parsedScanners.Skip(1));

    while (queue.Count > 0) {
      var scanner = queue.Dequeue();
      var found = false;

      foreach (var bcn in scanner) {
        var otherDistances = GetDistances(scanner, bcn);

        foreach (var (_, distances) in beacons) {
          var intersection = otherDistances
            .IntersectBy(distances.Select(x => x.Distance), x => x.Distance)
            .ToArray();

          if (intersection.Length < 11) continue;

          found = true;

          var (firstBeacon, firstDistances, firstDistance) = intersection[0];
          var (matchedBeacon, matchedDistances, _) = distances.First(x => x.Distance == firstDistance);

          var transformX = GetXTransform(matchedDistances.X, firstDistances);
          var transformY = GetYTransform(matchedDistances.Y, firstDistances);
          var transformZ = GetZTransform(matchedDistances.Z, firstDistances);

          var scannerPos = matchedBeacon - new Vector3(
            transformX(firstBeacon),
            transformY(firstBeacon),
            transformZ(firstBeacon)
          );

          scanners.Add(scannerPos);

          beacons = CalculateDistances(beacons, scanner
            .Except(intersection.Select(x => x.Beacon).Concat(new[] { bcn }))
            .Select(x => scannerPos + new Vector3(transformX(x), transformY(x), transformZ(x)))
          );

          break;
        }

        if (found)
          break;
      }

      if (!found)
        queue.Enqueue(scanner);
    }

    return part == Part.One
      ? beacons.Length
      : (int)scanners.Max(x => scanners.Max(y => ManhattanDistance(x, y)));
  }

  private static float ManhattanDistance(Vector3 first, Vector3 second) {
    var abs = Vector3.Abs(first - second);
    return abs.X + abs.Y + abs.Z;
  }

  private static (Vector3 Origin, (Vector3 Beacon, Vector3 Distances, float Distance)[] Distances)[]
    CalculateDistances(
      IEnumerable<(Vector3 Origin, (Vector3, Vector3, float)[])> current,
      IEnumerable<Vector3> toAdd
    ) {
    var beacons = current
      .Select(x => x.Origin)
      .Concat(toAdd)
      .Distinct()
      .ToArray();

    return beacons
      .Select(b => (
        Beacon: b,
        Distances: GetDistances(beacons, b)
      ))
      .ToArray();
  }

  private static (Vector3 Beacon, Vector3 Distances, float Distance)[] GetDistances(
    IEnumerable<Vector3> beacons,
    Vector3 origin
  ) =>
    beacons
      .Where(x => x != origin)
      .Select(x => (
        Beacon: x,
        Distances: x - origin,
        Distance: Vector3.Distance(x, origin)
      ))
      .ToArray();

  private static Func<Vector3, float> GetZTransform(
    float zDistance,
    Vector3 other
  ) =>
    zDistance == other.Z
      ? x => x.Z
      : zDistance == -other.Z
        ? x => -x.Z
        : zDistance == other.X
          ? x => x.X
          : zDistance == -other.X
            ? x => -x.X
            : zDistance == other.Y
              ? x => x.Y
              : x => -x.Y;

  private static Func<Vector3, float> GetYTransform(
    float yDistance,
    Vector3 other
  ) =>
    yDistance == other.Y
      ? x => x.Y
      : yDistance == -other.Y
        ? x => -x.Y
        : yDistance == other.X
          ? x => x.X
          : yDistance == -other.X
            ? x => -x.X
            : yDistance == other.Z
              ? x => x.Z
              : x => -x.Z;

  private static Func<Vector3, float> GetXTransform(
    float xDistance,
    Vector3 other
  ) =>
    xDistance == other.X
      ? x => x.X
      : xDistance == -other.X
        ? x => -x.X
        : xDistance == other.Y
          ? x => x.Y
          : xDistance == -other.Y
            ? x => -x.Y
            : xDistance == other.Z
              ? x => x.Z
              : x => -x.Z;

  private static List<List<Vector3>> ParseScanners(IReadOnlyList<string> input) {
    var scanners = new List<List<Vector3>>();

    var scanner = new List<Vector3>();
    for (var i = 1; i < input.Count; i++) {
      var line = input[i];
      if (line[..2] == "--") {
        scanners.Add(scanner);
        scanner = new List<Vector3>();
        continue;
      }

      var (x, y, z, _) = line.SplitAsInt(",");
      scanner.Add(new Vector3(x, y, z));
    }

    scanners.Add(scanner);

    return scanners;
  }
}
