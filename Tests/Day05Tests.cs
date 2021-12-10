using Xunit;

namespace Aoc2021.Tests;

public class Day05Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/05_1.txt", 5)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day05.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/05_1.txt", 12)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day05.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
