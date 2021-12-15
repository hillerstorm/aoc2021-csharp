namespace Aoc2021;

public class HeapNode<TValue> : IComparable<HeapNode<TValue>> {
  public HeapNode(TValue value, float priority, long insertOrder) {
    Value = value;
    Priority = priority;
    InsertOrder = insertOrder;
  }

  public TValue Value { get; }
  private float Priority { get; }
  private long InsertOrder { get; }

  public int CompareTo(HeapNode<TValue>? other) {
    if (other == null)
      return 1;

    var diff = Priority.CompareTo(other.Priority);
    return diff == 0
      ? InsertOrder.CompareTo(other.InsertOrder)
      : diff;
  }
}
