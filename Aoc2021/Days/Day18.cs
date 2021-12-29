namespace Aoc2021;

public class Day18 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    var input = rawInput.SplitLines();
    return (
      () => Part1(input).ToString(),
      () => Part2(input).ToString()
    );
  }

  public static int Part1(string[] input) {
    var index = 0;
    return GetValue(input
      .Skip(1)
      .Aggregate(
        ParsePair(input[0], ref index),
        (left, line) => {
          var idx = 0;
          var right = ParsePair(line, ref idx);
          var pair = Add(left, right);
          Reduce(pair);

          return pair;
        }));
  }

  public static int Part2(string[] input) {
    var pairs = input.Pairs(input).Distinct();
    var maxMagnitude = -1;

    foreach (var (a, b) in pairs) {
      var index = 0;
      var left = ParsePair(a, ref index);
      index = 0;
      var right = ParsePair(b, ref index);

      var pair = Add(left, right);
      Reduce(pair);

      var magnitude = GetValue(pair);
      if (magnitude > maxMagnitude)
        maxMagnitude = magnitude;
    }

    return maxMagnitude;
  }

  public static int GetValue(Fish? fish) =>
    fish switch {
      Pair pair => Magnitude(pair),
      Number number => number.Value,
      _ => 0
    };

  private static int Magnitude(Pair pair) =>
    3 * GetValue(pair.Left) + 2 * GetValue(pair.Right);

  public static Pair Add(Fish left, Fish right, Pair? parent = null) {
    var pair = new Pair(left, right, parent);
    left.Parent = pair;
    right.Parent = pair;
    return pair;
  }

  public static void Reduce(Fish root) {
    while (true) {
      while (ExplodeAny(root)) { }

      if (!SplitFirst(root))
        return;
    }
  }

  private static bool ExplodeAny(Fish fish) {
    var exploded = false;

    switch (fish) {
      case Pair { Level: >= 4, Left: Number, Right: Number } pair:
        exploded = Explode(pair) || exploded;
        break;
      case Pair pair: {
        if (pair.Left is Pair leftPair)
          exploded = ExplodeAny(leftPair) || exploded;
        if (pair.Right is Pair rightPair)
          exploded = ExplodeAny(rightPair) || exploded;

        break;
      }
    }

    return exploded;
  }

  private static bool SplitFirst(Fish fish) {
    var split = false;

    if (fish is Number { Value: > 9 } number) {
      split = Split(number);
    } else if (fish is Pair pair) {
      if (pair.Left is Number { Value: > 9 } left)
        split = Split(left);
      else
        split = SplitFirst(pair.Left);

      if (!split && pair.Right is Number { Value: > 9 } right)
        split = Split(right);
      else if (!split)
        split = SplitFirst(pair.Right);
    }

    return split;
  }

  private static bool Explode(Pair pair) {
    if (pair.Parent == null || pair.Level < 4 || pair.Left is not Number left || pair.Right is not Number right)
      return false;

    var parent = pair.Parent;
    var currentPair = pair;
    while (parent != null) {
      if (parent.Left == currentPair) {
        currentPair = parent;
        parent = parent.Parent;
      } else {
        var leftNum = parent.Left;
        while (leftNum is Pair _pair)
          leftNum = _pair.Right;

        if (leftNum is not Number number) continue;

        parent = null;
        number.Value += left.Value;
      }
    }

    parent = pair.Parent;
    currentPair = pair;
    while (parent != null) {
      if (parent.Right == currentPair) {
        currentPair = parent;
        parent = parent.Parent;
      } else {
        var rightNum = parent.Right;
        while (rightNum is Pair _pair)
          rightNum = _pair.Left;

        if (rightNum is not Number number) continue;

        parent = null;
        number.Value += right.Value;
      }
    }

    if (pair.Parent.Left == pair)
      pair.Parent.Left = new Number(0, pair.Parent);
    else if (pair.Parent.Right == pair)
      pair.Parent.Right = new Number(0, pair.Parent);

    return true;
  }

  public static bool Split(Number number) {
    if (number.Value < 10)
      return false;

    var left = number.Value / 2;
    var right = number.Value - left;
    var pair = Add(new Number(left), new Number(right), number.Parent);

    if (number.Parent == null) {
      number.Parent = pair;
      return true;
    }

    if (number.Parent.Left == number)
      number.Parent.Left = pair;
    else
      number.Parent.Right = pair;

    return true;
  }

  public static Fish ParsePair(string content, ref int index, Pair? parent = null) {
    var left = content[++index] == '['
      ? ParsePair(content, ref index)
      : ParseNumber(content, ref index);

    var right = content[++index] == '['
      ? ParsePair(content, ref index)
      : ParseNumber(content, ref index);

    index++;

    return Add(left, right, parent);
  }

  public static Number ParseNumber(string content, ref int index) {
    var chr = content[index];
    var num = string.Empty;
    while (char.IsDigit(chr)) {
      num += chr;

      if (index == content.Length - 1)
        break;

      chr = content[++index];
    }

    return new Number(int.Parse(num));
  }

  public abstract class Fish {
    protected Fish(Pair? parent = null) {
      Parent = parent;
    }

    public Pair? Parent { get; set; }

    public int Level {
      get {
        var level = 0;
        var parent = Parent;
        while (parent != null) {
          level++;
          parent = parent.Parent;
        }
        return level;
      }
    }
  }

  public class Pair : Fish {
    public Pair(Fish left, Fish right, Pair? parent = null)
      : base(parent) {
      Left = left;
      Right = right;
    }

    public Fish Left { get; set; }
    public Fish Right { get; set; }

    public override string ToString() => $"[{Left},{Right}]";
  }

  public class Number : Fish {
    public Number(int value, Pair? parent = null)
      : base(parent) {
      Value = value;
    }

    public int Value { get; set; }

    public override string ToString() => Value.ToString();
  }
}
