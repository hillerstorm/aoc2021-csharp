using Xunit;

namespace Aoc2021.Tests;

public class Day17Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("target area: x=20..30, y=-10..-5", 45)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day17.Part1(x.Input)));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("target area: x=20..30, y=-10..-5", 112)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day17.Part2(x.Input)));
  }
}
