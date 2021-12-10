using Xunit;

namespace Aoc2021.Tests;

public class Day10Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/10_1.txt", 26397)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day10.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/10_1.txt", 288957L)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day10.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
