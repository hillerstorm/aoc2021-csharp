using Xunit;

namespace Aoc2021.Tests;

public class Day01Tests {
  [Fact]
  public void TestPart1() {
    var assertions = new[] {
      ("Inputs/01_1.txt", 7)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day01.Part1(File.ReadAllText(x.Input).SplitAsInt())));
  }

  [Fact]
  public void TestPart2() {
    var assertions = new[] {
      ("Inputs/01_1.txt", 5)
    };
    assertions.ForEach(((string Input, int Expected) x) =>
      Assert.Equal(x.Expected, Day01.Part2(File.ReadAllText(x.Input).SplitAsInt())));
  }
}
