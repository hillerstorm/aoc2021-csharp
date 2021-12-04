public class Day04 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) =>
    Run(input, Part.One);

  public static int Part2(string[] input) =>
    Run(input, Part.Two);

  private static int Run(string[] input, Part part) {
    var (numbers, boards) = ParseInput(input);
    var winningBoards = new HashSet<int>();
    for (var n = 0; n < numbers.Length; n++) {
      var num = numbers[n];
      for (var b = 0; b < boards.Length; b++) {
        boards[b] = boards[b].Select(x => x == num ? "#" : x).ToArray();
        if (CheckBoard(boards[b])) {
          if (part == Part.One)
            return CalculateScore(boards[b], num);

          winningBoards.Add(b);
          if (winningBoards.Count == boards.Length)
            return CalculateScore(boards[b], num);
        }
      }
    }

    return -1;
  }

  private static (string[] Numbers, string[][] Boards) ParseInput(string[] input) =>
    (
      input[0].Split(",", StringSplitOptions.RemoveEmptyEntries),
      input[1..]
        .ChunkBy(5)
        .Select(x =>
          string.Join(" ", x)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        )
        .ToArray()
    );

  private static int CalculateScore(string[] board, string num) =>
    int.Parse(num) * board.Where(x => x != "#").Sum(x => int.Parse(x));

  private static bool CheckBoard(string[] board) {
    var rows = board.ChunkBy(5).Select(x => x.ToArray()).ToArray();
    if (rows.Any(r => string.Join("", r) == "#####"))
      return true;

    for (var x = 0; x < 5; x++) {
      var win = true;
      for (var y = 0; y < 5; y++)
        if (rows[y][x] != "#") {
          win = false;
          break;
        }
      if (win)
        return true;
    }

    return false;
  }
}
