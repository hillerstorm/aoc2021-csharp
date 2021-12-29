using Xunit;

namespace Aoc2021.Tests;

public class Day22Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/22_1.txt", 39),
      ("Inputs/22_2.txt", 590784),
      ("Inputs/22_3.txt", 474140)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day22.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/22_1.txt", 39),
      ("Inputs/22_2.txt", 39769202357779),
      ("Inputs/22_3.txt", 2758514936282235)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day22.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
