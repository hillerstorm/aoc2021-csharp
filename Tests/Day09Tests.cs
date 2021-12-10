using Xunit;

namespace Aoc2021.Tests;

public class Day09Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/09_1.txt", 15)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day09.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/09_1.txt", 1134)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day09.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
