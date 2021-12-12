using Xunit;

namespace Aoc2021.Tests;

public class Day12Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Tests/Inputs/12_1.txt", 10),
      ("Tests/Inputs/12_2.txt", 19),
      ("Tests/Inputs/12_3.txt", 226)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day12.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Tests/Inputs/12_1.txt", 36),
      ("Tests/Inputs/12_2.txt", 103),
      ("Tests/Inputs/12_3.txt", 3509)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day12.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
