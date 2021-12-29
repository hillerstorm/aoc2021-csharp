using Xunit;

namespace Aoc2021.Tests;

public class Day03Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/03_1.txt", 198)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day03.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/03_1.txt", 230)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day03.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
