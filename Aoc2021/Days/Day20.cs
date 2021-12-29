using System.Text;

namespace Aoc2021;

public class Day20 : IDay {
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
    var algo = input[0].Select(x => x == '.' ? '0' : '1').ToArray();

    var width = input[1].Length;
    var height = input[1..].Length;

    var image = string.Concat(input[1..])
      .Select(x => x == '.' ? '0' : '1')
      .ToArray()
      .Pad(with: '0', by: 2, width, height);

    width += 4;
    height += 4;

    var swapping = algo[0] == '1';
    var white = swapping;

    var steps = part == Part.One ? 2 : 50;
    for (var s = 0; s < steps; s++) {
      var newWidth = width + 2;
      var newHeight = height + 2;
      var newImage = Enumerable.Repeat(swapping && white ? '1' : '0', newWidth * newHeight).ToArray();

      for (var y = 1; y < height - 1; y++)
      for (var x = 1; x < width - 1; x++)
        newImage[(y + 1) * newWidth + x + 1] = GetNextPixel(algo, image, y * width + x, width);

      white = !white;
      width = newWidth;
      height = newHeight;
      image = newImage;
    }

    return image.Count(x => x == '1');
  }

  private static char GetNextPixel(IReadOnlyList<char> algorithm, char[] image, int index, int width) {
    var sb = new StringBuilder();
    sb.Append(image[(index - width - 1)..(index - width + 2)]);
    sb.Append(image[(index - 1)..(index + 2)]);
    sb.Append(image[(index + width - 1)..(index + width + 2)]);
    return algorithm[Convert.ToInt32(sb.ToString(), 2)];
  }
}
