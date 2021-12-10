﻿using Nito.Collections;

namespace Aoc2021;

public static class Extensions {
  public static string[] SplitLines(this string input, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries) =>
    input.Split("\n", options);

  public static int[] SplitAsInt(this string input, string separator = "\n") =>
    input.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

  public static long[] SplitAsLong(this string input, string separator = "\n") =>
    input.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

  public static async Task<(string? Input, string? Error)> GetInput(this int day) {
    var inputPath = $"Inputs/{day:00}.txt";
    if (File.Exists(inputPath))
      return (await File.ReadAllTextAsync(inputPath), null);

    // Download
    Console.WriteLine("Input file not found, downloading...");
    if (!File.Exists(".session"))
      return (null, "No .session file found, save value from Cookie header on your aoc page");

    var session = await File.ReadAllTextAsync(".session");
    if (string.IsNullOrWhiteSpace(session))
      return (null, ".session file found but is empty, save value from Cookie header on your aoc page");

    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("Cookie", $"session={session.Trim()}");
    var result = await client.GetStringAsync($"https://adventofcode.com/2021/day/{day}/input");

    Console.WriteLine("Downloaded input, saving to disk");
    await File.WriteAllTextAsync(inputPath, result);

    return (result, null);
  }

  public static IEnumerable<T> Cyclic<T>(this IEnumerable<T> source) {
    var arr = source.ToArray();
    var i = 0;
    while (true) {
      yield return arr[i];
      i = (i + 1) % arr.Length;
    }
  }

  public static IEnumerable<(int X, int Y)> Square(int x, int y, int width, int height) =>
    x.To(width).Pairs(y.To(height));

  public static IEnumerable<(T A, T B)> TakeTwo<T>(this IReadOnlyList<T> source) {
    if (source.Count < 2)
      yield break;

    for (var i = 1; i < source.Count; i++)
      yield return (source[i - 1], source[i]);
  }

  public static IEnumerable<(T A, T B, T C)> TakeThree<T>(this IReadOnlyList<T> source) {
    if (source.Count < 3)
      yield break;

    for (var i = 2; i < source.Count; i++)
      yield return (source[i - 2], source[i - 1], source[i]);
  }

  public static IEnumerable<(T1 A, T2 B)> Pairs<T1, T2>(this IEnumerable<T1> source, IEnumerable<T2> other) =>
    source.SelectMany(x => other.Select(y => (x, y)));

  public static IEnumerable<(T1 A, T2 B, T3 C)> Triples<T1, T2, T3>(
    this IEnumerable<T1> source,
    IEnumerable<T2> first,
    IEnumerable<T3> second
  ) =>
    source.SelectMany(x => first.SelectMany(y => second.Select(z => (x, y, z))));

  public static IEnumerable<int> To(this int from, int max) =>
    Enumerable.Range(from, max);

  public static void Rotate<T>(this Deque<T> source, long offset) {
    if (offset < 0) {
      offset = Math.Abs(offset);
      for (long i = 0; i < offset; i++)
        source.AddToFront(source.RemoveFromBack());
    } else if (offset > 0) {
      for (long i = 0; i < offset; i++)
        source.AddToBack(source.RemoveFromFront());
    }
  }

  public static IEnumerable<IEnumerable<T1>> ChunkBy<T1>(this IEnumerable<T1> source, int width) {
    var entries = source.LongCount() / width;
    for (var i = 0; i < entries; i++)
      yield return source.Skip(i * width).Take(width);
  }

  public static (IList<T> Matches, IList<T> Other) Partition<T>(
    this IEnumerable<T> source,
    Predicate<T> predicate
  ) {
    var matches = new List<T>();
    var other = new List<T>();

    foreach (var item in source)
      if (predicate(item))
        matches.Add(item);
      else
        other.Add(item);

    return (matches, other);
  }

  public static Dictionary<TKey, int> AddTo<TKey>(this Dictionary<TKey, int> dict, TKey key, int value)
    where TKey : notnull {
    if (dict.ContainsKey(key))
      dict[key] += value;
    else
      dict[key] = value;

    return dict;
  }

  public static Dictionary<TKey, long> AddTo<TKey>(this Dictionary<TKey, long> dict, TKey key, long value)
    where TKey : notnull {
    if (dict.ContainsKey(key))
      dict[key] += value;
    else
      dict[key] = value;

    return dict;
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    rest = source.Skip(1);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    rest = source.Skip(2);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    rest = source.Skip(3);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out T d,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    d = source[3];
    rest = source.Skip(4);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out T d,
    out T e,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    d = source[3];
    e = source[4];
    rest = source.Skip(5);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out T d,
    out T e,
    out T f,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    d = source[3];
    e = source[4];
    f = source[5];
    rest = source.Skip(6);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out T d,
    out T e,
    out T f,
    out T g,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    d = source[3];
    e = source[4];
    f = source[5];
    g = source[6];
    rest = source.Skip(7);
  }

  public static void Deconstruct<T>(
    this IList<T> source,
    out T a,
    out T b,
    out T c,
    out T d,
    out T e,
    out T f,
    out T g,
    out T h,
    out IEnumerable<T> rest
  ) {
    a = source[0];
    b = source[1];
    c = source[2];
    d = source[3];
    e = source[4];
    f = source[5];
    g = source[6];
    h = source[7];
    rest = source.Skip(8);
  }
}
