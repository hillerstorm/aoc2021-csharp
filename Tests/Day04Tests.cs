using Xunit;

namespace Aoc2021.Tests;

public class Day04Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/04_1.txt", 4512)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day04.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/04_1.txt", 1924)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day04.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
