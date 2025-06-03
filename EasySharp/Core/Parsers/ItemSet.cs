namespace EasySharp.Core.Parsers;

/// LR(1)项集
public class ItemSet
{
    public int StateNumber { get; set; }
    public HashSet<LRItem> Items { get; }

    public ItemSet(HashSet<LRItem> items)
    {
        Items = items;
        StateNumber = -1;
    }

    public ItemSet(LRItem item)
    {
        Items = new HashSet<LRItem> { item };
        StateNumber = -1;
    }

    public override bool Equals(object? obj)
    {
        return obj is ItemSet other && Items.SetEquals(other.Items);
    }

    public override int GetHashCode()
    {
        return Items.Count;
    }

    public override string ToString()
    {
        return $"I{StateNumber}: {{{string.Join(", ", Items)}}}";
    }
}