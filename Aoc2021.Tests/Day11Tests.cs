using Xunit;

namespace Aoc2021.Tests;

public class Day11Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/11_1.txt", 1656)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day11.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/11_1.txt", 195)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day11.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
