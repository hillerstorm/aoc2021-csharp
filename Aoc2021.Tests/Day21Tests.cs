using Xunit;

namespace Aoc2021.Tests;

public class Day21Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/21_1.txt", 739785)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day21.Part1(File.ReadAllText(x.Input).SplitLines())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/21_1.txt", 444356092776315)
    };
    assertions.ForEach(((string Input, long Expected) x) =>
      Assert.Equal(x.Expected, Day21.Part2(File.ReadAllText(x.Input).SplitLines())));
  }
}
