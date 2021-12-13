using Xunit;

namespace Aoc2021.Tests;

public class Day13Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/13_1.txt", 17)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day13.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2()
  {
    var assertions = new[]
    {
      ("Tests/Inputs/13_1.txt", @"█████
█   █
█   █
█   █
█████")
    };
    assertions.ForEach(((string Input, string Expected) x) =>
      Assert.Equal(x.Expected, Day13.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
