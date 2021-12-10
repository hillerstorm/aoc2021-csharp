using Xunit;

namespace Aoc2021.Tests;

public class Day08Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/08_1.txt", 0),
      ("Tests/Inputs/08_2.txt", 26)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day08.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/08_1.txt", 5353),
      ("Tests/Inputs/08_2.txt", 61229)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day08.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
