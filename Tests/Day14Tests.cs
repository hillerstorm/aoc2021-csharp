using Xunit;

namespace Aoc2021.Tests;

public class Day14Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/14_1.txt", 1588)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day14.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2()
  {
    var assertions = new[]
    {
      ("Tests/Inputs/14_1.txt", 2188189693529)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day14.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
