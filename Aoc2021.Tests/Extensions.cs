namespace Aoc2021.Tests;

public static class Extensions {
  public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
    foreach (var item in source)
      action(item);
  }
}
