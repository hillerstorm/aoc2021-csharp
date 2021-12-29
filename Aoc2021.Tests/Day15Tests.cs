using Xunit;

namespace Aoc2021.Tests;

public class Day15Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/15_1.txt", 40)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day15.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2()
  {
    var assertions = new[]
    {
      ("Inputs/15_1.txt", 315)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day15.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
