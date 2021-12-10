public class Day10 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(IEnumerable<string> input) =>
    Run(input).SyntaxScore;

  public static long Part2(IEnumerable<string> input) =>
    Run(input).AutoCompleteScore;

  private static (int SyntaxScore, long AutoCompleteScore) Run(IEnumerable<string> lines) {
    var syntaxScore = 0;
    var autoCompleteScores = new List<long>();

    foreach (var line in lines) {
      var expectedClosers = new Stack<char>();
      var isCorrupt = false;
      for (var i = 0; i < line.Length; i++) {
        var chr = line[i];
        switch (chr) {
          case '(':
            expectedClosers.Push(')');
            break;
          case '[':
            expectedClosers.Push(']');
            break;
          case '{':
            expectedClosers.Push('}');
            break;
          case '<':
            expectedClosers.Push('>');
            break;
          case ')':
            isCorrupt = CheckStack(expectedClosers, chr, ref syntaxScore);
            break;
          case ']':
            isCorrupt = CheckStack(expectedClosers, chr, ref syntaxScore);
            break;
          case '}':
            isCorrupt = CheckStack(expectedClosers, chr, ref syntaxScore);
            break;
          case '>':
            isCorrupt = CheckStack(expectedClosers, chr, ref syntaxScore);
            break;
        }

        if (isCorrupt)
          break;

        if (i == line.Length - 1 && expectedClosers.Count > 0)
          autoCompleteScores.Add(expectedClosers.Aggregate(0L, GetAutoCompleteScore));
      }
    }

    autoCompleteScores.Sort();

    return (syntaxScore, autoCompleteScores[(int)Math.Floor(autoCompleteScores.Count / 2m)]);
  }

  private static long GetAutoCompleteScore(long score, char chr) =>
    score * 5 + chr switch {
      ')' => 1,
      ']' => 2,
      '}' => 3,
      '>' => 4,
      _ => 0
    };

  private static bool CheckStack(Stack<char> expectedClosers, char chr, ref int score) {
    if (expectedClosers.TryPop(out var c) && c == chr)
      return false;

    score += chr switch {
      ')' => 3,
      ']' => 57,
      '}' => 1197,
      '>' => 25137,
      _ => 0
    };

    return true;
  }
}
