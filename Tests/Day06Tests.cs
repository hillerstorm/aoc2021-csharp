using Xunit;

namespace Aoc2021.Tests;

public class Day06Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/06_1.txt", 5934L)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day06.Part1(File.ReadAllText(x.Input).SplitAsInt(","))));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/06_1.txt", 26984457539)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day06.Part2(File.ReadAllText(x.Input).SplitAsInt(","))));
  }
}
