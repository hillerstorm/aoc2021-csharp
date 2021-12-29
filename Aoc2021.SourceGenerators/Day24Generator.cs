using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Aoc2021;

[Generator]
public class Day24Generator : ISourceGenerator {
    public void Execute(GeneratorExecutionContext context) {
      var file = context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith("24.txt"));
      if (file == null) return;
      Console.WriteLine(file.Path);

      var blocks = new List<string>();

      var content = file.GetText(context.CancellationToken)?
        .ToString()
        .Split("\n", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
      if (content == null) return;

      var source = new StringBuilder($@"// Auto-generated code
namespace Aoc2021;

public static class Day24Input {{
  public static Func<(long W, long X, long Y, long Z), int, (long W, long X, long Y, long Z)>[] Instructions =
    new[] {{
");
      var indent = 6;
      for (var i = 0; i < content.Length; i++) {
        var line = content[i];
        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        switch (parts[0]) {
          case "inp":
            if (i > 0) {
              source.Append($"{new string(' ', indent)}");
              source.AppendLine("return (w, x, y, z);");
              indent -= 2;
              source.Append($"{new string(' ', indent)}");
              source.AppendLine("}),");
            }

            source.Append($"{new string(' ', indent)}");
            source.AppendLine("(((long W, long X, long Y, long Z) memory, int input) => {");
            indent += 2;
            source.Append($"{new string(' ', indent)}");
            source.AppendLine("long w = input;");
            source.Append($"{new string(' ', indent)}");
            source.AppendLine("var (_, x, y, z) = memory;");

            break;
          case "add":
            source.Append($"{new string(' ', indent)}");
            source.AppendLine($"{parts[1]} += {parts[2]};");
            break;
          case "mul":
            source.Append($"{new string(' ', indent)}");
            source.AppendLine($"{parts[1]} *= {parts[2]};");
            break;
          case "div":
            source.Append($"{new string(' ', indent)}");
            source.AppendLine($"{parts[1]} /= {parts[2]};");
            break;
          case "mod":
            source.Append($"{new string(' ', indent)}");
            source.AppendLine($"{parts[1]} %= {parts[2]};");
            break;
          case "eql":
            source.Append($"{new string(' ', indent)}");
            source.AppendLine($"{parts[1]} = {parts[1]} == {parts[2]} ? 1 : 0;");
            break;
        }
      }

      source.Append($"{new string(' ', indent)}");
      source.AppendLine("return (w, x, y, z);");
      indent -= 2;
      source.Append($@"{new string(' ', indent)}}})
    }};

    private static (long W, long X, long Y, long Z) ResetW((long W, long X, long Y, long Z) memory) =>
      (0, memory.X, memory.Y, memory.Z);

    private static (long W, long X, long Y, long Z) ResetX((long W, long X, long Y, long Z) memory) =>
      (memory.W, 0, memory.Y, memory.Z);

    private static (long W, long X, long Y, long Z) ResetY((long W, long X, long Y, long Z) memory) =>
      (memory.W, memory.X, 0, memory.Z);

    private static (long W, long X, long Y, long Z) ResetZ((long W, long X, long Y, long Z) memory) =>
      (memory.W, memory.X, memory.Y, 0);

    public static Func<(long W, long X, long Y, long Z), (long W, long X, long Y, long Z)>[] Resets =
      new[] {{
");

      indent = 8;
      foreach (var line in content.Where(x => x.StartsWith("inp"))) {
        source.Append($"{new string(' ', indent)}");
        var register = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
        switch (register) {
          case "w":
            source.AppendLine("ResetW,");
            break;
          case "x":
            source.AppendLine("ResetX,");
            break;
          case "y":
            source.AppendLine("ResetY,");
            break;
          default: // case "z":
            source.AppendLine("ResetZ,");
            break;
        }
      }
      indent -= 2;
      source.Append($@"{new string(' ', indent)}}};
}}
");

      context.AddSource("Day24.g.cs", source.ToString());
    }

    public void Initialize(GeneratorInitializationContext context) {
    }
}
