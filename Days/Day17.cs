namespace Aoc2021;

public class Day17 : IDay
{
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput)
  {
    return (
      () => Part1(rawInput.Trim()).ToString(),
      () => Part2(rawInput.Trim()).ToString()
    );
  }

  public static int Part1(string input) =>
    Run(input, Part.One);

  public static int Part2(string input) =>
    Run(input, Part.Two);

  public static int Run(string input, Part part)
  {
    var (xMin, xMax, _) = input[15..input.IndexOf(",")].Split("..");
    var (yMin, yMax, _) = input[(input.IndexOf("y") + 2)..].Split("..");

    var x1 = int.Parse(xMin);
    var x2 = int.Parse(xMax);
    var y1 = int.Parse(yMin);
    var y2 = int.Parse(yMax);

    var maxHeight = -1;
    var hitCount = 0;

    for (var x = 0; x <= x2; x++)
    {
      for (var y = y1; y < Math.Abs(y1); y++)
      {
        var xVelocity = x;
        var yVelocity = y;
        var pos = (x: 0, y: 0);
        var height = 0;
        var hit = false;

        while (true)
        {
          pos = (pos.x + xVelocity, pos.y + yVelocity);

          if (pos.y > height)
            height = pos.y;

          xVelocity += xVelocity switch
          {
            > 0 => -1,
            < 0 => 1,
            _ => 0
          };
          yVelocity -= 1;

          if (pos.x >= x1 && pos.x <= x2 && pos.y >= y1 && pos.y <= y2)
          {
            hit = true;
            hitCount++;
            break;
          }

          if (pos.x > x2 || pos.y < y1)
            break;
        }

        if (hit && height > maxHeight)
          maxHeight = height;
      }
    }

    return part == Part.One ? maxHeight : hitCount;
  }
}
