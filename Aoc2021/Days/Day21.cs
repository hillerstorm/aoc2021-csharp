namespace Aoc2021;

public class Day21 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) {
    var player1Position = int.Parse(input[0][28..]);
    var player2Position = int.Parse(input[1][28..]);

    var player1Score = 0;
    var player2Score = 0;

    var die = 1;
    var totalRolls = 0;

    while (true) {
      totalRolls += 3;
      player1Position += die * 3 + 3;
      while (player1Position > 10)
        player1Position -= 10;

      player1Score += player1Position;

      die += 3;
      if (die > 100)
        die -= 100;

      if (player1Score >= 1000)
        return player2Score * totalRolls;

      totalRolls += 3;
      player2Position += die * 3 + 3;
      while (player2Position > 10)
        player2Position -= 10;

      player2Score += player2Position;

      die += 3;
      if (die > 100)
        die -= 100;

      if (player2Score >= 1000)
        return player1Score * totalRolls;
    }
  }

  public static long Part2(string[] input) {
    var player1Start = int.Parse(input[0][28..]);
    var player2Start = int.Parse(input[1][28..]);

    var player1Wins = 0L;
    var player2Wins = 0L;

    var queue = new Stack<(bool Player1, int P1Pos, int P1Score, int P2Pos, int P2Score, long Times)>();
    queue.Push((true, player1Start, 0, player2Start, 0, 1L));

    while (queue.Count > 0) {
      var step = queue.Pop();
      for (var roll = 3; roll <= 9; roll++) {
        var pos = (step.Player1 ? step.P1Pos : step.P2Pos) + roll;
        while (pos > 10)
          pos -= 10;
        var score = (step.Player1 ? step.P1Score : step.P2Score) + pos;
        var times = step.Times * roll switch {
          3 => 1, // 1,1,1
          4 => 3, // 1,1,2 1,2,1 2,1,1
          5 => 6, // 1,2,2 2,1,2 2,2,1 1,1,3 1,3,1 3,1,1
          6 => 7, // 3,2,1 3,1,2 2,3,1 2,1,3 1,2,3 1,3,2 2,2,2
          7 => 6, // 3,3,1 3,1,3 1,3,3 3,2,2 2,3,2 2,2,3
          8 => 3, // 3,3,2 3,2,3 2,3,3
          9 => 1, // 3,3,3
          _ => 0
        };

        if (score >= 21) {
          if (step.Player1)
            player1Wins += times;
          else
            player2Wins += times;
          continue;
        }

        queue.Push(step.Player1
          ? (false, pos, score, step.P2Pos, step.P2Score, times)
          : (true, step.P1Pos, step.P1Score, pos, score, times)
        );
      }
    }

    return Math.Max(player1Wins, player2Wins);
  }
}
