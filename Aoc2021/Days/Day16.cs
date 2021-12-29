using System.Text;

namespace Aoc2021;

public class Day16 : IDay {
  public (Func<string> Part1, Func<string> Part2) Parts(string rawInput) {
    return (
      () => Part1(rawInput).ToString(),
      () => Part2(rawInput).ToString()
    );
  }

  public enum OperatorType {
    Sum = 0,
    Product = 1,
    Minimum = 2,
    Maximum = 3,
    GreaterThan = 5,
    LessThan = 6,
    Equal = 7
  }

  public abstract record Packet(int Version);
  public record LiteralPacket(int Version, long Value)
    : Packet(Version);
  public record OperatorPacket(int Version, OperatorType Type, List<Packet> SubPackets)
    : Packet(Version);

  public static int Part1(string input) =>
    SumVersions(ParseFirstPacket(input));

  public static long Part2(string input) =>
    GetValue(ParseFirstPacket(input));

  private static Packet ParseFirstPacket(string input, int index = 0) =>
    ParsePacket(string.Concat(
      input.Trim().Select(x =>
        Convert.ToString(Convert.ToInt32(x.ToString(), 16), 2).PadLeft(4, '0')
      )
    ), ref index);

  private static int SumVersions(Packet packet) =>
    packet.Version + packet switch {
      OperatorPacket op => op.SubPackets.Sum(SumVersions),
      _ => 0
    };

  private static long GetValue(Packet packet) => packet switch {
    LiteralPacket literal =>
      literal.Value,

    OperatorPacket { Type: OperatorType.Sum } op =>
      op.SubPackets.Sum(GetValue),

    OperatorPacket { Type: OperatorType.Product } op =>
      op.SubPackets.Aggregate(1L, (x, p) => x * GetValue(p)),

    OperatorPacket { Type: OperatorType.Minimum } op =>
      op.SubPackets.Min(GetValue),

    OperatorPacket { Type: OperatorType.Maximum } op =>
      op.SubPackets.Max(GetValue),

    OperatorPacket { Type: OperatorType.GreaterThan } op =>
      GetValue(op.SubPackets[0]) > GetValue(op.SubPackets[1]) ? 1 : 0,

    OperatorPacket { Type: OperatorType.LessThan } op =>
      GetValue(op.SubPackets[0]) < GetValue(op.SubPackets[1]) ? 1 : 0,

    OperatorPacket { Type: OperatorType.Equal } op =>
      GetValue(op.SubPackets[0]) == GetValue(op.SubPackets[1]) ? 1 : 0,

    _ => 0
  };

  private static Packet ParsePacket(string bits, ref int index) {
    var packetVersion = Convert.ToInt32(bits[index..(index += 3)], 2);
    var typeId = Convert.ToInt32(bits[index..(index += 3)], 2);

    return typeId == 4
      ? new LiteralPacket(packetVersion, ParseLiteralPacket(bits, ref index))
      : new OperatorPacket(packetVersion, (OperatorType)typeId, ParseOperatorPacket(bits, ref index));
  }

  private static long ParseLiteralPacket(string bits, ref int index) {
    var isEnd = bits[index++];
    var sb = new StringBuilder();

    while (true) {
      sb.Append(bits[index..(index += 4)]);

      if (isEnd == '0')
        break;

      isEnd = bits[index++];
    }

    return Convert.ToInt64(sb.ToString(), 2);
  }

  private static List<Packet> ParseOperatorPacket(string bits, ref int index) {
    var subPackets = new List<Packet>();

    var lengthTypeId = bits[index++];
    if (lengthTypeId == '0') {
      var length = Convert.ToInt32(bits[index..(index += 15)], 2);
      var maxIndex = index + length;

      while (index < maxIndex) {
        subPackets.Add(ParsePacket(bits, ref index));
      }
    } else {
      var numPackets = Convert.ToInt32(bits[index..(index += 11)], 2);

      for (var i = 0; i < numPackets; i++) {
        subPackets.Add(ParsePacket(bits, ref index));
      }
    }

    return subPackets;
  }
}
