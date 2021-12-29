using Microsoft.CodeAnalysis;

namespace Aoc2021;

[Generator]
public class Day24Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
      // Build up the source code
      string source = $@"// Auto-generated code
namespace Aoc2021;

public partial class Day24 : IDay
{{
  static partial string[] Instructions() =>
    new[] { "awd" };
}}
";
      // Add the source code to the compilation
      context.AddSource("Day24.g.cs", source);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }
}
