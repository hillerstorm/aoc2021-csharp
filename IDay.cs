namespace Aoc2021;

public interface IDay {
  (Func<string> Part1, Func<string> Part2) Parts(string input);
}
