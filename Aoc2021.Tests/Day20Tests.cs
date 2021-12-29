using Xunit;

namespace Aoc2021.Tests;

public class Day20Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/20_1.txt", 35)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day20.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/20_1.txt", 3351)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day20.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
