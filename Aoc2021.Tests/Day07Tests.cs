using Xunit;

namespace Aoc2021.Tests;

public class Day07Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/07_1.txt", 37)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day07.Part1(File.ReadAllText(x.Input).SplitAsInt(","))));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/07_1.txt", 168)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day07.Part2(File.ReadAllText(x.Input).SplitAsInt(","))));
  }
}
